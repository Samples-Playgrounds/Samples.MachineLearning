using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Microsoft.ML;

using HolisticWare.Ph4ct3x.DiagnosticTests.Morphological.SomatoTypes.ML.Evaluation.MachineLearningDotNet;
using Ph4ct3x.Somatotype.BusinessLogic;

namespace Ph4ct3x.App.Somatotype
{
    partial class Program
    {
        static MLContext mlContext = null;
        static SomatotypeInputData sample_ml = null;

        static Microsoft.ML.ITransformer model = null;
        static Microsoft.ML.IDataView trainData = null;
        static Microsoft.ML.IDataView testData = null;

        static readonly string _trainDataPath = Data.File.Replace(".csv", ".results.csv");
        static readonly string _modelPath = Path.Combine
                                                (
                                                    new string[]
                                                    {
                                                        Environment.CurrentDirectory,
                                                        "CSV",
                                                        "Ph4ct3x",
                                                        "DiagonsticTests",
                                                        "Morphological",
                                                        "SomatoType",
                                                        "Model.zip"
                                                    }
                                                );

        private static void CallMachineLearningDotNet()
        {
            sample_ml = new SomatotypeInputData()
            {
                Height = 191.7,
                Mass = 82.0,
                BreadthHumerus = 7.3,
                BreadthFemur = 10.1,
                GirthArmUpper = 33.2,
                GirthCalfStanding = 36,
                SkinfoldTriceps = 7,
                SkinfoldSubscapular = 6,
                SkinfoldMedialCalf = 4,
                SkinfoldSupraspinale = 9
            };

            mlContext = new MLContext(seed: 0);

            // Train/learn
            model = TrainLearnMLdotnet(mlContext, _trainDataPath);
            EvaluateTestMLdotnet(mlContext, model);
            EvaluateTestSinglePredictionMLdotnet(mlContext, model);
        }

        public static ITransformer TrainLearnMLdotnet(MLContext mlContext, string path)
        {
            IDataView dataView = null;

            dataView = mlContext
                        .Data
                        .LoadFromTextFile<SomatotypeInputData>
                                    (
                                        path,
                                        hasHeader: true,
                                        separatorChar: ','
                                    );

            DataOperationsCatalog.TrainTestData dataSplit;

            dataSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.25);
            trainData = dataSplit.TrainSet;
            testData = dataSplit.TestSet;

            IEnumerable<SomatotypeInputData> list = null;

            list = mlContext
                        .Data
                        .CreateEnumerable<SomatotypeInputData>(dataView, reuseRowObject: false)
                        .ToList();

            for(int i = 0; i < list.Count(); i++)
            {
                Console.WriteLine($" Id = {list.ElementAt(i).Id}");

                Console.WriteLine($"        EndomorphicComponent = {list.ElementAt(i).EndomorphicComponent}");
                Console.WriteLine($"        MesomorphicComponent = {list.ElementAt(i).MesomorphicComponent}");
                Console.WriteLine($"        EctomorphicComponent = {list.ElementAt(i).EctomorphicComponent}");
            }

            Microsoft.ML.Transforms.ColumnCopyingEstimator pipeline = null;

            pipeline = mlContext.Transforms.CopyColumns
                                                (
                                                    outputColumnName: "Label",
                                                    inputColumnName: "EndomorphicComponent"
                                                );

            pipeline.Append
                (
                    mlContext.Transforms.Concatenate
                    (
                        "Features",
                        "Height",
                        "Mass",
                        "BreadthHumerus",
                        "BreadthFemur",
                        "GirthArmUpper",
                        "GirthCalfStanding",
                        "SkinfoldSubscapular",
                        "SkinfoldTriceps",
                        "SkinfoldSupraspinale",
                        "SkinfoldMedialCalf"
                    )
                );
            pipeline.Append(mlContext.Regression.Trainers.FastTree());

            Microsoft.ML.Transforms.ColumnCopyingTransformer model_1 = pipeline.Fit(trainData);

            return model = model_1;
        }

        private static void EvaluateTestMLdotnet(MLContext mlc, ITransformer m)
        {
            IDataView predictions = m.Transform(testData);

            Microsoft.ML.Data.RegressionMetrics metrics = null;

            metrics = mlc.Regression.Evaluate(predictions, "Label", "EndomorphicComponent");

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");

            return;
        }

        private static void EvaluateTestSinglePredictionMLdotnet(MLContext mlc, ITransformer m)
        {
            var predictionFunction = mlc
                                        .Model
                                        .CreatePredictionEngine<SomatotypeInputData, SomatotypeOutputData>(m);


            SomatotypeOutputData prediction = predictionFunction.Predict(sample_ml);

            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.EndomorphicComponent:0.####}, actual fare: XXXXXXXXXX");
            Console.WriteLine($"**********************************************************************");

            return;
        }
    }
}
