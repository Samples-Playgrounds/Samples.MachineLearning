using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Ph4ct3x.App.Somatotype.MachineLearningDotNet;

namespace Ph4ct3x.App.Somatotype
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Deterministic Calculation Start");
            // LocalDeterministic();
            // Console.WriteLine("Deterministic Calculation Stop");

            // Console.ReadLine(); // Enter Key
            Console.ReadKey(); // Any Key

            Console.WriteLine("Azure Machine Learning Studio Calculation Start");
            CallAzureMachineLearningService();
            Console.WriteLine("Azure Machine Learning Studio Calculation Start");

            Console.ReadKey(); // Any Key

            Console.WriteLine("Machine Learning Calculation Start");
            ModelTrainingLearning.ExecuteMachineLearningDotNetTrainingLearning();
            Console.WriteLine("Machine Learning Calculation Stop");

            ModelTrainingLearning.Metrics();

            return;
        }

    }
}
