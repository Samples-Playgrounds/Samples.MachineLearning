Information("Scientific Playground - Scripting - Machine Learning");

// tools (needed for code and Cake addins)
#tool nuget:?package=Microsoft.ML&version=1.4.0

// Cake Addins
// Cake specific 
//#addin nuget:?package=Microsoft.ML&version=1.4.0

#load   "./Common/Data.cake"
#load   "./TrainLearnFitModel/TrainLearnFitModel.cake"
#load   "./Evaluate/ModelEvaluationPrediction.cake"


Task("Learn-Train-Fit")
	.Does
    (
        () => 
        {
            Information("   Learning");
        }
    );

Task("Evaluate")
	.Does
    (
        () => 
        {
            Information("   Evaluating");

            Data.Initialize();

            ModelEvaluationPrediction.Metrics();

            ModelEvaluationPrediction.EvaluateSingleTestPrediction
                                                    (
                                                        new SomatotypeInputData()
                                                        {
                                                            Height = 191.7,
                                                            Mass = 82.0,
                                                            BreadthHumerus = 7.3,
                                                            BreadthFemur = 10.1,
                                                            GirthArmUpper = 33.2,
                                                            GirthCalfStanding = 36,
                                                            SkinfoldTriceps = 7,
                                                            SkinfoldSubscapular = 6,
                                                            SkinfoldMedialCalf = 4,
                                                            SkinfoldSupraspinale = 9
                                                        }
                                                    );
        }
    );


RunTarget("Evaluating");
