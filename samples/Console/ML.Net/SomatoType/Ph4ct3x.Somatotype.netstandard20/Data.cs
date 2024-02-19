using System;
using System.IO;

using Microsoft.ML;
using Microsoft.ML.Transforms;

using HolisticWare.Ph4ct3x.DiagnosticTests.Morphological.SomatoTypes.ML.Evaluation.MachineLearningDotNet;

namespace Ph4ct3x.Somatotype.BusinessLogic
{
    public class Data
    {
        public static MLContext Context = null;

        public static ITransformer Transformer = null;
        public static IDataView DataViewTraining = null;
        public static IDataView DataViewTesting = null;

        public static DataViewSchema DataViewSchema = null;
        public static DataOperationsCatalog.TrainTestData DataViewSplit;

        public static ColumnCopyingEstimator TransformPipeline = null;

        public static void Initialize()
        {
            Context = new MLContext(seed: 0);

            IDataView data_view_full = null;

            data_view_full = Context
                                .Data
                                .LoadFromTextFile<SomatotypeInputData>
                                                            (
                                                                Data.File,
                                                                hasHeader: true,
                                                                separatorChar: ','
                                                            );

            Data.DataViewSchema = data_view_full.Schema; ;
            Data.DataViewSplit = Context.Data.TrainTestSplit(data_view_full, testFraction: 0.25);
            Data.DataViewTraining = Data.DataViewSplit.TrainSet;
            Data.DataViewTesting = Data.DataViewSplit.TestSet;


            Data.TransformPipeline = Context.Transforms.CopyColumns
                                                            (
                                                                outputColumnName: "Label",
                                                                inputColumnName: "EndomorphicComponent"
                                                            );

            Data.TransformPipeline.Append
                                        (
                                            Context.Transforms.Concatenate
                                            (
                                                "Features",
                                                "Height",
                                                "Mass",
                                                "BreadthHumerus",
                                                "BreadthFemur",
                                                "GirthArmUpper",
                                                "GirthCalfStanding",
                                                "SkinfoldSubscapular",
                                                "SkinfoldTriceps",
                                                "SkinfoldSupraspinale",
                                                "SkinfoldMedialCalf"
                                            )
                                        );

            ColumnCopyingTransformer model_endomorphic = null;

            Data.Transformer = model_endomorphic;
            
            return;
        }

        public static readonly string File = Path.Combine
                                                        (
                                                            new string[]
                                                            {
                                                                Environment.CurrentDirectory,
                                                                "CSV",
                                                                "Ph4ct3x",
                                                                "DiagonsticTests",
                                                                "Morphological",
                                                                "SomatoType",
                                                                "data01",
                                                                "SomatoType.Data.Step01.Regression.csv"
                                                            }
                                                        );

        public static readonly string TrainDataPath = File.Replace(".csv", ".results.csv");
        public static string ModelPathEndomorphic = Path.Combine
                                                            (
                                                                new string[]
                                                                {
                                                                    Environment.CurrentDirectory,
                                                                    "MachineLearningDotNet",
                                                                    "Models",
                                                                    "Model_Endomorphic.zip"
                                                                }
                                                            );

        public static SomatotypeInputData SampleTest
        {
            get;
        }  = new SomatotypeInputData()
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
                            };

    }
}
