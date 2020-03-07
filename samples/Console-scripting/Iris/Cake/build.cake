using System;

Information("Scientific Playground - Scripting - Machine Learning");
Console.WriteLine("     Cake Build System");

string TARGET = Argument ("t", Argument ("target", "Evaluate"));

// tools (needed for code and Cake addins)
//#tool nuget:?package=Microsoft.ML&version=1.4.0

// Cake Addins
// Cake specific 
#addin nuget:?package=Microsoft.ML&version=1.4.0&loaddependencies=true

#load   "./Common/Data.cake"
#load   "./Evaluate/ModelEvaluationPrediction.cake"

        static readonly string path_model = System.IO.Path.Combine
                                                    (
                                                        Environment.CurrentDirectory,
                                                        "Models",
                                                        "IrisClusteringModel.zip"
                                                    );


Task("Learn")
	.Does
    (
        () => 
        {
            Information("   Learning");
        }
    );

Task("Consume")
	.Does
    (
        () => 
        {
            Information("   Consume");

            ModelEvaluationPrediction.EvaluateSingleTestPrediction
                                            (
                                                new IrisData
                                                {
                                                    SepalLength = 5.1f,
                                                    SepalWidth = 3.5f,
                                                    PetalLength = 1.4f,
                                                    PetalWidth = 0.2f
                                                }
                                            );
        }
    );

Task("Evaluate")
	.Does
    (
        () => 
        {
            Information("   Evaluate");
            Information("       calling Consume");

            RunTarget("Consume");

            return;
        }
    );

Task("Predict")
    .IsDependentOn("Evaluate")
	.Does
    (
        () => 
        {
            Information("   Predict");

            return;
        }
    );

RunTarget(TARGET);
