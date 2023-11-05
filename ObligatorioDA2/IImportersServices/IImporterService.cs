using IBusinessLogic;
using PromotionInterface;

namespace IImportersServices
{
    public interface IImporterService
    {

        public PromotionAbstract CreatePromotionAbstract(PromotionAbstractModelIn model);

    }
}