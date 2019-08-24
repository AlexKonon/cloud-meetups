using System.ComponentModel.DataAnnotations;

namespace AWS.Lambda.Notifier.Domain
{
    public sealed class LambdaIncomeRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
