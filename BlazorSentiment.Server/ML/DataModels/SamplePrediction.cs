using Microsoft.ML.Data;

namespace BlazorSentiment.Server.ML.DataModels
{
    public class SamplePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Score { get; set; }
    }
}



