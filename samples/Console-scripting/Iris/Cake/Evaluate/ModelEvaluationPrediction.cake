
    public class ModelEvaluationPrediction
    {
        public static void EvaluateSingleTestPrediction(IrisData input)
        {
            MLContext contex = new MLContext(seed: 0);

            // https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/save-load-machine-learning-models-ml-net
            // https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/machine-learning-model-predictions-ml-net

            // prediction result for IrisData
            //  Predicted Label - row Id of the CSV
            //  Score           - float distanc from the cluster 
            ClusterPrediction prediction = null;

            // Model is ITransformer
            ITransformer model = null;

            // Prediction Engine loads trained model from zip
            PredictionEngine<IrisData, ClusterPrediction> engine_prediction = null;

            //Define DataViewSchema for data preparation pipeline and trained model
            DataViewSchema model_schema = null;

            using 
                (
                    FileStream fs = new FileStream
                                                (
                                                    path_model, 
                                                    FileMode.Open, 
                                                    FileAccess.Read, 
                                                    FileShare.Read
                                                )
                )
            {
                // Load trained model
                model = contex.Model.Load(path_model, out model_schema);
            }

            engine_prediction = contex.Model.CreatePredictionEngine<IrisData, ClusterPrediction>
                                                                        (
                                                                            model
                                                                            //, model_schema
                                                                        );

            prediction = engine_prediction.Predict(input);

            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");

            string iris_class = "N/A";

            if (prediction.PredictedClusterId == 1)
            {
                iris_class = "Setosa";
            }
            else if (prediction.PredictedClusterId == 2)
            {
                iris_class = "Versicolor";
            }
            else if (prediction.PredictedClusterId == 3)
            {
                iris_class = "Virginica";
            }
            else
            {
                throw new System.Exception("WTF?????");
            }
            Console.WriteLine($"Cluster (if): {iris_class}");

            switch(prediction.PredictedClusterId)
            {
                case 1:
                    iris_class = "Setosa";
                    break;
                case 2:
                    iris_class = "Versicolor";
                    break;
                case 3:
                    iris_class = "Virginica";
                    break;
                default:
                    throw new System.Exception("WTF?????");
            }
            Console.WriteLine($"Cluster (switch/case): {iris_class}");


            return;
        }
    }  
