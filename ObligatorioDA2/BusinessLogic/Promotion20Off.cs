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
        public override float GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return 0;
            float total = 0;
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
            }
            return total;
        }
    }
}