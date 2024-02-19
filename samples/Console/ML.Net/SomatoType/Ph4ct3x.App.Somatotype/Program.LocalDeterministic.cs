using System;
using System.Text;

using HolisticWare.Ph4ct3x.DiagnosticTests.Morphological.SomatoTypes;
using HolisticWare.Ph4ct3x.DiagnosticTests.Morphological.SomatoTypes.ML.Evaluation.Core;

using Ph4ct3x.Somatotype.BusinessLogic;

namespace Ph4ct3x.App.Somatotype
{
    partial class Program
    {

        public static void LocalDeterministic()
        {
            System.IO.TextReader reader = new System.IO.StreamReader(Data.File);
            string s = reader.ReadToEnd();
            string[] rows = s.Split
                                    (
                                        '\n'
                                        //System.Environment.NewLine
                                    );

            System.Collections.Generic.List<string[]> records = new System.Collections.Generic.List<string[]>();
            foreach(string row in rows)
            {
                string[] columns = row.Split(new char[] { ',' });

                records.Add(columns);
            }


            loop_data = new System.Collections.Generic.List<SomatotypeInputData>();
            records.RemoveAt(0);

            foreach (string[] record in records)
            {
                SomatotypeInputData stid = new SomatotypeInputData()
                {
                    Id = int.Parse(record[3]),
                    Height = double.Parse(record[4]),
                    Mass = double.Parse(record[5]),
                    BreadthHumerus = double.Parse(record[6]),
                    BreadthFemur = double.Parse(record[7]),
                    GirthArmUpper = double.Parse(record[8]),
                    GirthCalfStanding = double.Parse(record[9]),
                    SkinfoldSubscapular = double.Parse(record[10]),
                    SkinfoldTriceps = double.Parse(record[11]),
                    SkinfoldSupraspinale = double.Parse(record[12]),
                    SkinfoldMedialCalf = double.Parse(record[13]),
                };
                loop_data.Add(stid);
            }

            HeathCarterMisigojDurakovic();



            string content = null;
            //string header = null;
            StringBuilder sb = new StringBuilder();

            //header =
            //    "Endomorphic,Mesomorphic,EctoMorphic"
            //    + "," +
            //    "Id,Height,Mass"
            //    + "," +
            //    "SkinfoldTriceps,SkinfoldSubscapular,SkinfoldSupraspinale,SkinfoldMedialCalf"
            //    + "," +
            //    "BreadthFemur,BreadthHumerus"
            //    + "," +
            //    "GirthArmUpper,GirthCalfStanding"
            //    ;

            sb.Clear();
            System.IO.TextWriter writer = new System.IO.StreamWriter(Data.File.Replace(".csv", ".results.csv"));
            foreach (SomatotypeOutputtData stod1 in results)
            {
                sb.Append($"{stod1.Endomorphic},");
                sb.Append($"{stod1.Mesomorphic},");
                sb.Append($"{stod1.EctoMorphic},");
                sb.Append($"{stod1.Id},");
                sb.Append($"{stod1.Height},");
                sb.Append($"{stod1.Mass},");
                sb.Append($"{stod1.SkinfoldTriceps},");
                sb.Append($"{stod1.SkinfoldSubscapular},");
                sb.Append($"{stod1.SkinfoldSupraspinale},");
                sb.Append($"{stod1.SkinfoldMedialCalf},");
                sb.Append($"{stod1.BreadthFemur},");
                sb.Append($"{stod1.BreadthHumerus},");
                sb.Append($"{stod1.GirthArmUpper},");
                sb.Append($"{stod1.GirthCalfStanding}");
                sb.AppendLine();
            }
            writer.WriteLine(content + Environment.NewLine + sb.ToString());
            writer.Flush();

            sb.Clear();
            System.IO.TextWriter writer_1 = new System.IO.StreamWriter(Data.File.Replace(".csv", ".results_ectomorph_range_error.csv"));
            foreach (SomatotypeOutputtData stod1 in results_ectomorph_range_error)
            {
                sb.Append($"{stod1.Endomorphic},");
                sb.Append($"{stod1.Mesomorphic},");
                sb.Append($"{stod1.EctoMorphic},");
                sb.Append($"{stod1.Id},");
                sb.Append($"{stod1.Height},");
                sb.Append($"{stod1.Mass},");
                sb.Append($"{stod1.SkinfoldTriceps},");
                sb.Append($"{stod1.SkinfoldSubscapular},");
                sb.Append($"{stod1.SkinfoldSupraspinale},");
                sb.Append($"{stod1.SkinfoldMedialCalf},");
                sb.Append($"{stod1.BreadthFemur},");
                sb.Append($"{stod1.BreadthHumerus},");
                sb.Append($"{stod1.GirthArmUpper},");
                sb.Append($"{stod1.GirthCalfStanding}");
                sb.AppendLine();
            }
            writer_1.WriteLine(content + Environment.NewLine + sb.ToString());
            writer_1.Flush();

            System.IO.TextWriter writer_2 = new System.IO.StreamWriter(Data.File.Replace(".csv", ".results_ectomorph_extreme.csv"));
            foreach (SomatotypeOutputtData stod1 in results_ectomorph_extreme)
            {
                sb.Append($"{stod1.Endomorphic},");
                sb.Append($"{stod1.Mesomorphic},");
                sb.Append($"{stod1.EctoMorphic},");
                sb.Append($"{stod1.Id},");
                sb.Append($"{stod1.Height},");
                sb.Append($"{stod1.Mass},");
                sb.Append($"{stod1.SkinfoldTriceps},");
                sb.Append($"{stod1.SkinfoldSubscapular},");
                sb.Append($"{stod1.SkinfoldSupraspinale},");
                sb.Append($"{stod1.SkinfoldMedialCalf},");
                sb.Append($"{stod1.BreadthFemur},");
                sb.Append($"{stod1.BreadthHumerus},");
                sb.Append($"{stod1.GirthArmUpper},");
                sb.Append($"{stod1.GirthCalfStanding}");
                sb.AppendLine();
            }
            writer_2.WriteLine(content + Environment.NewLine + sb.ToString());
            writer_2.Flush();


            return;
        }

        static System.Collections.Generic.List<SomatotypeOutputtData> results = null;
        static System.Collections.Generic.List<SomatotypeOutputtData> results_ectomorph_extreme = null;
        static System.Collections.Generic.List<SomatotypeOutputtData> results_ectomorph_range_error = null;

        static System.Collections.Generic.List<SomatotypeInputData> loop_data = null;

        static void HeathCarterMisigojDurakovic()
        {
            results = new System.Collections.Generic.List<SomatotypeOutputtData>();
            results_ectomorph_extreme = new System.Collections.Generic.List<SomatotypeOutputtData>();
            results_ectomorph_range_error = new System.Collections.Generic.List<SomatotypeOutputtData>();


            foreach (SomatotypeInputData stid in loop_data)
            {
                HeathCarterMisigojDurakovic hc = new HeathCarterMisigojDurakovic()
                {
                    Mass = stid.Mass,
                    Height = stid.Height,
                    Skinfolds = new Skinfolds
                    {
                        SubTriceps = stid.SkinfoldTriceps,
                        SubScapular = stid.SkinfoldSubscapular,
                        SupraIliac = stid.SkinfoldSupraspinale,
                        Calf = stid.SkinfoldMedialCalf
                    },
                    Bicondyles = new Bicondyles()
                    {
                        Femur = stid.BreadthFemur,
                        Humerus = stid.BreadthHumerus
                    },
                    Circumferences = new Circumferences
                    {
                        ArmUpper = stid.GirthArmUpper,
                        Calf = stid.GirthCalfStanding
                    }
                };

                double endomorphic = double.NaN;
                double mesomorphic = double.NaN;
                double ectomorphic = double.NaN;

                try
                {
                    endomorphic = hc.EndomorphicComponent();
                    mesomorphic = hc.MesomorphicComponent();
                    ectomorphic = hc.EctomorphicComponent();
                }
                catch(Exception)
                {
                    SomatotypeOutputtData stod_error = new SomatotypeOutputtData(stid)
                    {
                        Endomorphic = endomorphic,
                        Mesomorphic = mesomorphic,
                        EctoMorphic = ectomorphic
                    };

                    results_ectomorph_range_error.Add(stod_error);
                    continue;
                }

                SomatotypeOutputtData stod = new SomatotypeOutputtData(stid)
                {
                    Endomorphic = endomorphic,
                    Mesomorphic = mesomorphic,
                    EctoMorphic = ectomorphic
                };

                if (stod.Mesomorphic < 0)
                {
                    results_ectomorph_extreme.Add(stod);
                }
                else
                {
                    results.Add(stod);
                }
            }

            return;
        }

    }
}
