
    public class ModelEvaluationPrediction
    {
        static ModelEvaluationPrediction()
        {
            Data.Context = new MLContext(seed: 0);

            return;
        }

        public static void Metrics()
        {
            //Define DataViewSchema for data preparation pipeline and trained model
            DataViewSchema schema = Data.DataViewSchema;

            // Load trained model
            ITransformer trainedModel = Data.Context.Model.Load
                                                                (
                                                                    Data.ModelPathEndomorphic, 
                                                                    out schema
                                                                );

            Data.Transformer = trainedModel;
            IDataView predictions = Data.Transformer.Transform(Data.DataViewTesting);

            Microsoft.ML.Data.RegressionMetrics metrics_endomorphic = null;
            Microsoft.ML.Data.RegressionMetrics metrics_mesomorphic = null;
            Microsoft.ML.Data.RegressionMetrics metrics_ectomorphic = null;

            metrics_endomorphic = Data.Context.Regression.Evaluate(predictions, "Label", "EndomorphicComponent");
            metrics_mesomorphic = Data.Context.Regression.Evaluate(predictions, "Label", "MesomorphicComponent");
            metrics_ectomorphic = Data.Context.Regression.Evaluate(predictions, "Label", "EctomorphicComponent");

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*   EndomorphicComponent");
            Console.WriteLine($"*       RSquared Score:             {metrics_endomorphic.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:    {metrics_endomorphic.RootMeanSquaredError:#.##}");
            //Console.WriteLine($"*   MesomorphicComponent");
            //Console.WriteLine($"*       RSquared Score:             {metrics_endomorphic.RSquared:0.##}");
            //Console.WriteLine($"*       Root Mean Squared Error:    {metrics_endomorphic.RootMeanSquaredError:#.##}");
            //Console.WriteLine($"*   EctomorphicComponent");
            //Console.WriteLine($"*       RSquared Score:             {metrics_endomorphic.RSquared:0.##}");
            //Console.WriteLine($"*       Root Mean Squared Error:    {metrics_endomorphic.RootMeanSquaredError:#.##}");

            return;
        }

        public static void EvaluateSingleTestPrediction(SomatotypeInputData input)
        {
            PredictionEngine<SomatotypeInputData, SomatotypeOutputData> prediction_engine = null;

            prediction_engine = Data.Context
                                    .Model
                                    .CreatePredictionEngine<SomatotypeInputData, SomatotypeOutputData>(Data.Transformer);


            SomatotypeOutputData prediction = prediction_engine.Predict(input);

            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted Endomorphic: {prediction.EndomorphicComponent:0.####}, actual fare: XXXXXXXXXX");
            Console.WriteLine($"**********************************************************************");

            return;
        }
    }
    