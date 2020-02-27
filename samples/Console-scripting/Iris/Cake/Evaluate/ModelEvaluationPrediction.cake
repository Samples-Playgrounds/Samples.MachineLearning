
    public class ModelEvaluationPrediction
    {
        public static void EvaluateSingleTestPrediction(IrisData input)
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

            prediction = engine_prediction.Predict(input);

            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");

            return;
        }
    }
    