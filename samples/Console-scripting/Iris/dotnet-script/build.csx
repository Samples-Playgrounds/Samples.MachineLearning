#!/usr/bin/env dotnet-script
#r "nuget:Microsoft.ML,1.4.0"
#r "nuget:System.Collections.Immutable,1.7.0"
#r "nuget:System.Runtime.CompilerServices.Unsafe,4.7.0"
#r "nuget:Microsoft.ML.CpuMath,1.4.0"


#load   "./Common/Data.cs"
#load   "./TrainLearnFitModel/TrainLearnFitModel.cs"
#load   "./Evaluate/ModelEvaluationPrediction.cs"

using System;
using Microsoft.ML;
using Microsoft.ML.Transforms;
using Microsoft.ML.Data;

Console.WriteLine("Scientific Playground - Scripting - Machine Learning");

static readonly string path_model = Path.Combine
                                            (
                                                Environment.CurrentDirectory,
                                                "Model",
                                                "IrisClusteringModel.zip"
                                            );
static readonly string path_data = Path.Combine
                                            (
                                                Environment.CurrentDirectory, 
                                                "Common", 
                                                "iris.data"
                                            );


public void Learn ()
{
    Console.WriteLine("   Learning");

            MLContext mlContext = new MLContext(seed: 0);

            TextLoader textLoader = null;
            
            textLoader = mlContext.Data.CreateTextLoader<IrisData>(hasHeader: false, separatorChar: ',');
            Microsoft.ML.IDataView dataView = textLoader.Load(path_data);


            string featuresColumnName = "Features";
            var pipeline = mlContext.Transforms
                                        .Concatenate
                                            (
                                                featuresColumnName,
                                                "SepalLength",
                                                "SepalWidth",
                                                "PetalLength",
                                                "PetalWidth"
                                            )
                                        .Append
                                            (
                                                mlContext.Clustering.Trainers.KMeans
                                                                                (
                                                                                    featuresColumnName,
                                                                                    null,
                                                                                    numberOfClusters: 3
                                                                                )
                                            );

            var model = pipeline.Fit(dataView);


            using (var fileStream = new FileStream(path_model, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, textLoader, fileStream);
            }

}


public void Evaluate()
{
    Console.WriteLine("   Evaluating");

            MLContext mlContext = new MLContext(seed: 0);

            // https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/save-load-machine-learning-models-ml-net
            // https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/machine-learning-model-predictions-ml-net

            ITransformer model = null;
            PredictionEngine<IrisData, ClusterPrediction> engine_prediction = null;
            ClusterPrediction prediction = null;

            //Define DataViewSchema for data preparation pipeline and trained model
            DataViewSchema model_schema = null;

            using (FileStream fs = new FileStream(path_model, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                // Load trained model
                model = mlContext.Model.Load(path_model, out model_schema);
            }

            engine_prediction = mlContext.Model.CreatePredictionEngine<IrisData, ClusterPrediction>
                                                                (
                                                                    model
                                                                    //, model_schema
                                                                );

            prediction = engine_prediction.Predict
                                        (
                                            new IrisData
                                            {
                                                SepalLength = 5.1f,
                                                SepalWidth = 3.5f,
                                                PetalLength = 1.4f,
                                                PetalWidth = 0.2f
                                            }
                                        );

            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");

}

//Learn();

Evaluate();

