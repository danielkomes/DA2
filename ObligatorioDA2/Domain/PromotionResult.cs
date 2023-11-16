namespace Domain
{
    public class PromotionResult
    {
        public readonly float Result;
        public readonly bool IsApplied;

        public PromotionResult(float result, bool isApplied)
        {
            Result = result;
            IsApplied = isApplied;
        }
    }
}
