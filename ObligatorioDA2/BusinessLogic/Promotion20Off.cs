using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Promotion20Off : PromotionAbstract
    {
        private const EPromotionType TYPE = EPromotionType.Promotion20Off;
        public Promotion20Off(PromotionEntity promotionEntity) : base(promotionEntity, TYPE)
        {
        }

        //tener una constante de tipo Enum que será la usada para chequear
        //si es la correcta para usar cuando me traiga las PromotionEntity
        //tener un método de comparación con el enum
        public override PromotionResult GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return new PromotionResult(0, false, PromotionEntity.Id);
            float total = 0;
            bool applied = false;
            float maxPrice = products.ElementAt(0).Price;
            foreach (Product product in products)
            {
                total += product.Price;
                if (product.Price > maxPrice)
                {
                    maxPrice = product.Price;
                }
            }
            if (products.Count() >= 2)
            {
                total -= maxPrice * 0.2f;
                applied = true;
            }
            return new PromotionResult(total, applied, PromotionEntity.Id);
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Promotion20Off) return false;
            Promotion20Off other = obj as Promotion20Off;
            return PromotionEntity.Equals(other.PromotionEntity);
        }
    }
}