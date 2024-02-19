using System;

using Ph4ct3x.App.Somatotype.MachineLearningDotNet;
using Ph4ct3x.Somatotype.BusinessLogic;

namespace Ph4ct3x.App.Somatotype.Train.ML.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();

            ModelTrainingLearning.DoTrainingLearning(Data.File);

            ModelTrainingLearning.Metrics();

            return;
        }
    }
}
