using PromotionInterface;

namespace IImporters
{
    public interface IPromotionImporter
    {
        public IEnumerable<PromotionAbstractModelIn> ImportPromotions();
    }
}