namespace Domain
{
    public class PromotionResult
    {
        public readonly Guid PromotionId;
        public readonly float Result;
        public readonly bool IsApplied;

        public PromotionResult(float result, bool isApplied, Guid promotionId)
        {
            Result = result;
            IsApplied = isApplied;
            PromotionId = promotionId;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not PromotionResult) return false;
            PromotionResult other = obj as PromotionResult;
            return Result == other.Result &&
                IsApplied == other.IsApplied &&
                PromotionId == other.PromotionId;
        }
    }
}
