using System;
using Amazon.Lambda.Core;
using AWS.Lambda.Notifier.Domain;
using Newtonsoft.Json;

namespace AWS.Lambda.Notifier.Factories
{
    public sealed class LambdaIncomeRequestFactory : ILambdaIncomeRequestFactory
    {
        public LambdaIncomeRequest CreateFrom(string requestBody)
        {
            if (string.IsNullOrWhiteSpace(requestBody))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestBody));

            try
            {
                var result = JsonConvert.DeserializeObject<LambdaIncomeRequest>(requestBody);

                return result;
            }
            catch (JsonSerializationException)
            {
                LambdaLogger.Log("An error occured while trying to deserialize request body.");

                throw;
            }
        }
    }
}
