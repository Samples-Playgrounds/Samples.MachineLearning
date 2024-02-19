using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.Data.DataView;
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

        static void Main(string[] args)
        {
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

            return;
        }
    }
}
