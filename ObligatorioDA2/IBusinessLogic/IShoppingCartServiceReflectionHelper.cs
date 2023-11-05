using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLogic
{
    public interface IShoppingCartServiceReflectionHelper
    {
        public IEnumerable<PromotionAbstract> GetPromotions();
    }
}
