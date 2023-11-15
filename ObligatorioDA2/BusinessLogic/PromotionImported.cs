using Domain;
using IBusinessLogic;
using PromotionInterface;

namespace BusinessLogic
{
    public class PromotionImported : PromotionAbstract
    {
        private const EPromotionType TYPE = EPromotionType.PromotionImported;

        public readonly PromotionAbstractModelIn ImportedLogic;
        public readonly IProductLogic ProductLogic;
        public readonly IPromotionLogic PromotionLogic;
        public PromotionImported(
            PromotionAbstractModelIn logic,
            PromotionEntity promotionEntity,
            IProductLogic productLogic,
            IPromotionLogic promotionLogic
            ) : base(promotionEntity, TYPE)
        {
            ImportedLogic = logic;
            ProductLogic = productLogic;
            PromotionLogic = promotionLogic;
        }
        public override PromotionResult GetTotal(IEnumerable<Product> products)
        {
            IEnumerable<ProductModelIn> productModels = new List<ProductModelIn>();
            foreach (Product product in products)
            {
                ProductModelIn model = ProductLogic.CreateProductModelIn(product);
                productModels = productModels.Append(model);
            }
            PromotionResultModelIn importedResult = ImportedLogic.GetTotal(productModels);
            return PromotionLogic.CreatePromotionResult(importedResult);
        }
    }
}
