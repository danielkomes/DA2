using DataAccess.Exceptions;
using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;

namespace BusinessLogic
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartServiceDatabaseHelper DatabaseHelper;
        private readonly IShoppingCartServiceReflectionHelper ReflectionHelper;

        public ShoppingCartService(
            IShoppingCartServiceDatabaseHelper databaseHelper,
            IShoppingCartServiceReflectionHelper reflectionHelper
            )
        {
            DatabaseHelper = databaseHelper;
            ReflectionHelper = reflectionHelper;
        }

        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids)
        {
            return DatabaseHelper.GetProducts(ids);
        }

        public Product GetProduct(Guid id)
        {
            return DatabaseHelper.GetProduct(id);
        }

        public IEnumerable<PromotionAbstract> GetPromotions()
        {
            IEnumerable<PromotionAbstract> ret = new List<PromotionAbstract>();
            foreach (PromotionAbstract promotion in DatabaseHelper.GetPromotions())
            {
                ret = ret.Append(promotion);
            }
            foreach (PromotionAbstract promotion in ReflectionHelper.GetPromotions())
            {
                ret = ret.Append(promotion);
            }
            return ret;
        }

        public PaymentMethodEntity GetPaymentMethod(User user, EPaymentMethodType paymentMethod)
        {
            //PaymentMethod selectedMethod = null;
            //switch (paymentMethod)
            //{
            //    case EPaymentMethodType.MasterCard:
            //        {
            //            selectedMethod = new MasterCard(user);
            //            break;
            //        }
            //    case EPaymentMethodType.Visa:
            //        {
            //            selectedMethod = new Visa(user);
            //            break;
            //        }
            //    case EPaymentMethodType.Bbva:
            //        {
            //            selectedMethod = new Bbva(user);
            //            break;
            //        }
            //    case EPaymentMethodType.Itau:
            //        {
            //            selectedMethod = new Itau(user);
            //            break;
            //        }
            //    case EPaymentMethodType.Santander:
            //        {
            //            selectedMethod = new Santander(user);
            //            break;
            //        }
            //    case EPaymentMethodType.Paganza:
            //        {
            //            selectedMethod = new Paganza(user);
            //            break;
            //        }
            //    case EPaymentMethodType.Paypal:
            //        {
            //            selectedMethod = new Paypal(user);
            //            break;
            //        }
            //    default:
            //        {
            //            throw new InvalidDataException("Invalid payment method");
            //        }
            //}
            PaymentMethodEntity selectedMethod = new PaymentMethodEntity(user, paymentMethod);
            try
            {
                return DatabaseHelper.GetPaymentMethod(selectedMethod);
            }
            catch (ResourceNotFoundException)
            {
                return selectedMethod;
            }
        }

        public void InsertPurchase(Purchase purchase)
        {
            DatabaseHelper.InsertPurchase(purchase);
        }

    }
}
