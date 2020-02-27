#! "netcoreapp3.0"
//#r "nuget: NetStandard.Library, 2.0.0"
// #r "nuget: System.Memory 4.5.2"
// #r "nuget: System.ValueTuple, 4.5.0"
#r "nuget: Microsoft.ML, 0.10.0"
#r "nuget: Microsoft.Data.DataView, 0.10.0"

//#r "../../source/HolisticWare.Core.Text.NetStandard13/bin/Debug/netstandard1.3/HolisticWare.Core.Text.NetStandard13.dll"

// WaitForDebugger();
// using (var streamReader = new StreamReader(Console.OpenStandardInput()))
// {
//     Write(streamReader.ReadToEnd().ToUpper()); // <- SET BREAKPOINT HERE
// }

/*    
    dotnet tool uninstall -g dotnet-script
    dotnet tool install -g dotnet-script
    dotnet tool list -g

    ./g;obal.json

    https://en.wikipedia.org/wiki/Iris_flower_data_set

    https://archive.ics.uci.edu/ml/datasets/Iris

    https://raw.githubusercontent.com/dotnet/machinelearning/master/test/data/iris.data

 */
//using Core.Text;
using System;
using System.IO;
using Microsoft.ML;
using Microsoft.Data.DataView;
using Microsoft.ML.Data;


// string file = "iris.data.csv";
// CharacterSeparatedValues csv = new CharacterSeparatedValues();
// string content = await csv.LoadAsync(file);

// var mapping = csv
//                 .ParseTemporaryImplementation()
//                 .ToList()
//                 ;
// foreach(string[] row in mapping)
// {
//     foreach(string c in row)
//     {
//         Console.Write($"csv = {c}    ");
//     }
//     Console.WriteLine($"");
// }

public class IrisData
{
    [LoadColumn(0)]
    public float SepalLength;

    [LoadColumn(1)]
    public float SepalWidth;

    [LoadColumn(2)]
    public float PetalLength;

    [LoadColumn(3)]
    public float PetalWidth;
}

public class ClusterPrediction
{
    [ColumnName("PredictedLabel")]
    public uint PredictedClusterId;

    [ColumnName("Score")]
    public float[] Distances;
}


static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "iris.data");
static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "IrisClusteringModel.zip");

var mlContext = new MLContext(seed: 0);

TextLoader textLoader =
                    null
                    //new TextLoader()
                    ;
textLoader = mlContext.Data.CreateTextLoader<IrisData>(hasHeader: false, separatorChar: ',');
IDataView dataView = textLoader.Read(_dataPath);


string featuresColumnName = "Features";
var pipeline = mlContext.Transforms
    .Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
    .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, clustersCount: 3));