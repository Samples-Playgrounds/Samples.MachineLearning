using System;

using Ph4ct3x.Somatotype.BusinessLogic;

using Ph4ct3x.App.Somatotype.MachineLearningDotNet;

namespace Ph4ct3x.App.Somatotype.Evaluate.ML.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();

            ModelEvaluationPrediction.EvaluateSingleTestPrediction(Data.SampleTest);

            ModelEvaluationPrediction.EvaluateSingleTestPrediction
                                            (
                                                new HolisticWare.Ph4ct3x.DiagnosticTests.Morphological.SomatoTypes.ML.Evaluation.MachineLearningDotNet.
                                                    SomatotypeInputData()
                                                {
                                                    Height = 90.2,
                                                    Mass = 100.2,
                                                }
                                            );

            return;
        }
    }
}
