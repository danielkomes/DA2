namespace PromotionInterface
{
    public class PromotionResultModelIn
    {
        public readonly float Result;
        public readonly bool IsApplied;

        public PromotionResultModelIn(float result, bool isApplied)
        {
            Result = result;
            IsApplied = isApplied;
        }
    }
}