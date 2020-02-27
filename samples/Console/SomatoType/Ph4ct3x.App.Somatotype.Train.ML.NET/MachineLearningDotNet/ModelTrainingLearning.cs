using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.ML;

using HolisticWare.Ph4ct3x.DiagnosticTests.Morphological.SomatoTypes.ML.Evaluation.MachineLearningDotNet;
using Ph4ct3x.Somatotype.BusinessLogic;

namespace Ph4ct3x.App.Somatotype.MachineLearningDotNet
{
    public class ModelTrainingLearning
    {
        static MLContext ml_dotnet_context = null;
        public static void ExecuteMachineLearningDotNetTrainingLearning()
        {
            ml_dotnet_context = new MLContext(seed: 0);

            // Train/learn
            MachineLearningDotNet.ModelTrainingLearning.DoTrainingLearning
                                                                (
                                                                    Data.TrainDataPath
                                                                );

            return;
        }

        public static void DoTrainingLearning(string path)
        {
            Data.TransformPipeline.Append
                                        (
                                            ml_dotnet_context.Regression.Trainers
                                                                                .FastForest()
                                                                                //.FastTree()                                                    
                                                                                //.FastTreeTweedie()
                                                                                //.Gam()
                                                                                //.LbfgsPoissonRegression()
                                                                                //.OnlineGradientDescent()                                                    
                                                                                //.Sdca()
                                        );
            Data.TransformPipeline.Fit(Data.DataViewTraining);

            Microsoft.ML.Transforms.ColumnCopyingTransformer model_endomorphic = null;
            using
                (
                    FileStream file_stream = new FileStream
                                                    (
                                                        Data.ModelPathEndomorphic,
                                                        FileMode.Open,
                                                        FileAccess.Read,
                                                        FileShare.Read
                                                    )
                )
            {
                ml_dotnet_context.Model.Save //<SomatotypeInputData>
                                                    (
                                                        model_endomorphic,
                                                        Data.DataViewSchema,
                                                        file_stream
                                                    );
            }

            //DumpData(data_view);

            return;
        }

        public static void Metrics()
        {
            IDataView predictions = Data.Transformer.Transform(Data.DataViewTesting);

            Microsoft.ML.Data.RegressionMetrics metrics_endomorphic = null;
            Microsoft.ML.Data.RegressionMetrics metrics_mesomorphic = null;
            Microsoft.ML.Data.RegressionMetrics metrics_ectomorphic = null;

            metrics_endomorphic = ml_dotnet_context.Regression.Evaluate(predictions, "Label", "EndomorphicComponent");
            metrics_mesomorphic = ml_dotnet_context.Regression.Evaluate(predictions, "Label", "MesomorphicComponent");
            metrics_ectomorphic = ml_dotnet_context.Regression.Evaluate(predictions, "Label", "EctomorphicComponent");

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*   EndomorphicComponent");
            Console.WriteLine($"*       RSquared Score:             {metrics_endomorphic.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:    {metrics_endomorphic.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*   MesomorphicComponent");
            Console.WriteLine($"*       RSquared Score:             {metrics_endomorphic.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:    {metrics_endomorphic.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*   EctomorphicComponent");
            Console.WriteLine($"*       RSquared Score:             {metrics_endomorphic.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:    {metrics_endomorphic.RootMeanSquaredError:#.##}");

            return;
        }

        private static void DumpData(IDataView data_view)
        {
            IEnumerable<SomatotypeInputData> list = null;

            list = ml_dotnet_context
                                .Data
                                .CreateEnumerable<SomatotypeInputData>(data_view, reuseRowObject: false)
                                .ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                Console.WriteLine($" Id = {list.ElementAt(i).Id}");

                Console.WriteLine($"        EndomorphicComponent = {list.ElementAt(i).EndomorphicComponent}");
                Console.WriteLine($"        MesomorphicComponent = {list.ElementAt(i).MesomorphicComponent}");
                Console.WriteLine($"        EctomorphicComponent = {list.ElementAt(i).EctomorphicComponent}");
            }

            return;
        }

    }
}
