namespace Domain
{
    public class PromotionResult
    {
        public readonly float Result;
        public readonly bool IsApplied;
        public readonly Guid PromotionId;

        public PromotionResult(float result, bool isApplied, Guid promotionId)
        {
            Result = result;
            IsApplied = isApplied;
            PromotionId = promotionId;
        }
    }
}
