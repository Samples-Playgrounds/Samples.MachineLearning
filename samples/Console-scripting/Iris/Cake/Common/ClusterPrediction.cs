
using Microsoft.ML;
using Microsoft.ML.Transforms;
using Microsoft.ML.Data;

public class ClusterPrediction
{
    [ColumnName("PredictedLabel")]
    public uint PredictedClusterId;

    [ColumnName("Score")]
    public float[] Distances;
}
