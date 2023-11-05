using PromotionInterface;

namespace Importers
{
    public interface IPromotionImporter
    {
        public IEnumerable<PromotionAbstractModelIn> ImportPromotions();
    }
}