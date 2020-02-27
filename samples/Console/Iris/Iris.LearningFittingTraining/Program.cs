using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.ML.Data;

namespace Iris.netcoreapp30
{
    public class IrisData
    {
        [LoadColumn(0)]
        public float SepalLength;

        [LoadColumn(1)]
        public float SepalWidth;

        [LoadColumn(2)]
        public float PetalLength;

        [LoadColumn(3)]
        public float PetalWidth;
    }

    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

        [ColumnName("Score")]
        public float[] Distances;
    }


    class Program
    {
        static readonly string path_model = Path.Combine
                                                    (
                                                        Environment.CurrentDirectory,
                                                        "Model",
                                                        "IrisClusteringModel.zip"
                                                    );
        static readonly string path_data = Path.Combine(Environment.CurrentDirectory, "Data", "iris.data");

        //static async Task Main(string[] args)
        static void Main(string[] args)
        {

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

            //Task.WaitAll();

            return;
        }
    }
}
