using IBusinessLogic;

namespace Importers
{
    public interface IPromotionImporter
    {
        public IEnumerable<PromotionAbstract> ImportPromotions();
    }
}