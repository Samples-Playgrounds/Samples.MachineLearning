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
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "iris.data");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "IrisClusteringModel.zip");

        //static async Task Main(string[] args)
        static void Main(string[] args)
        {

            Console.WriteLine("Learning model");
            //string file = "iris.data.csv";
            //CharacterSeparatedValues csv = new CharacterSeparatedValues();
            //string content =
            //                await csv.LoadAsync(file)
            //                csv.LoadAsync(file).Result
            //                ;

            //var mapping = csv
            //                .ParseTemporaryImplementation()
            //                .ToList()
            //                ;
            //foreach (string[] row in mapping)
            //{
            //    foreach (string c in row)
            //    {
            //        Console.Write($"csv = {c}    ");
            //    }
            //    Console.WriteLine($"");
            //}

            MLContext mlContext = new MLContext(seed: 0);

            TextLoader textLoader = null;

            Console.WriteLine(" Reading data");
            textLoader = mlContext.Data.CreateTextLoader<IrisData>(hasHeader: false, separatorChar: ',');
            IDataView dataView = textLoader.Read(_dataPath);


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
                        mlContext.Clustering.Trainers.KMeans(featuresColumnName, clustersCount: 3)
                    );

            Console.WriteLine(" Fitting/Learning data");
            var model = pipeline.Fit(dataView);


            using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, fileStream);
            }

            var predictor = model.CreatePredictionEngine<IrisData, ClusterPrediction>(mlContext);

            var prediction = predictor.Predict(TestIrisData.Setosa);
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");



             prediction = predictor.Predict
                                        (
                                            new IrisData
                                            {
                                                SepalLength = 7.6f,
                                                SepalWidth = 2.5f,
                                                PetalLength = 6.9f,
                                                PetalWidth = 2.4f
                                            }
                                        );
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");

            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine(new string('-', 90));
                Console.Write("Enter CSV Data (SepalLenght,SepalWidth, PetalLength, PetalWidth):");
                string line = Console.ReadLine();

                if(string.IsNullOrEmpty(line))
                {
                    repeat = false;
                }
                else
                {
                    string[] parts = line.Split(new char[] { ',' });
                    if(parts.Length != 4)
                    {
                        throw new System.ArgumentException("4 numbers needed");
                    }
                    prediction = predictor.Predict
                                               (
                                                   new IrisData
                                                   {
                                                       SepalLength = float.Parse(parts[0]),
                                                       SepalWidth = float.Parse(parts[1]),
                                                       PetalLength = float.Parse(parts[2]),
                                                       PetalWidth = float.Parse(parts[3])
                                                   }
                                                );
                    Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
                    Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");

                }
            }
            //Task.WaitAll();

        }
    }
}
