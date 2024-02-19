using System;

using HolisticWare.Ph4ct3x.DiagnosticTests.Morphological.SomatoTypes.ML.Evaluation.AzureMachineLearningStudio;

using Ph4ct3x.Somatotype.BusinessLogic;

namespace Ph4ct3x.App.Somatotype
{
    partial class Program
    {

        public static void CallAzureMachineLearningService()
        {
            AzureMachineLearningService.Request
                (
                    Data.SampleTest.Height,
                    Data.SampleTest.Mass,
                    Data.SampleTest.SkinfoldTriceps,
                    Data.SampleTest.SkinfoldSubscapular,
                    Data.SampleTest.SkinfoldSupraspinale,
                    Data.SampleTest.SkinfoldMedialCalf,
                    Data.SampleTest.GirthArmUpper,
                    Data.SampleTest.GirthCalfStanding,
                    Data.SampleTest.BreadthFemur,
                    Data.SampleTest.BreadthHumerus
                );

            return;
        }
    }
}
