using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class PromotionTotalLook : IPromotion
    {
        public float GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return 0;
            float total = 0;
            float maxPrice = products.ElementAt(0).Price;
            Dictionary<string, int> colorCount = new Dictionary<string, int>();
            foreach (Product product in products)
            {
                total += product.Price;
                foreach (string color in product.Colors)
                {
                    if (colorCount.ContainsKey(color))
                    {
                        colorCount[color]++;
                    }
                    else
                    {
                        colorCount.Add(color, 1);
                    }
                }
                if (product.Price > maxPrice)
                {
                    maxPrice = product.Price;
                }
            }
            if (colorCount.Values.Max() >= 3)
            {
                total -= maxPrice * 0.5f;
            }
            return total;
        }
    }
}