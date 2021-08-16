using System;
using Microsoft.Extensions.ML;
using Microsoft.AspNetCore.Mvc;
using BlazorSentiment.Server.ML.DataModels;

namespace BlazorSentiment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentimentController : ControllerBase
    {
        private readonly PredictionEnginePool<SampleObservation, SamplePrediction> _predictionEnginePool;

        public SentimentController(PredictionEnginePool<SampleObservation, SamplePrediction> predictionEnginePool)
        {
            // Get the ML Model Engine injected, for scoring
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpGet("[action]")]
        [Route("sentimentprediction")]
        public ActionResult<float> PredictSentiment([FromQuery]string sentimentText)
        {
            SampleObservation sampleData = new SampleObservation { Col0 = sentimentText };
            
            SamplePrediction prediction = _predictionEnginePool.Predict(sampleData);
            
            float percentage = CalculatePercentage(prediction.Score);

            return percentage;
        }

        public float CalculatePercentage(double value)
        {
            return 100 * (1.0f / (1.0f + (float)Math.Exp(-value)));
        }
    }
}