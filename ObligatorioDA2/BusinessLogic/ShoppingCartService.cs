using DataAccess.Exceptions;
using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;
using Domain.PaymentMethods.CreditCards;
using Domain.PaymentMethods.DebitCards;
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

        public PaymentMethod GetPaymentMethod(User user, EPaymentMethodType paymentMethod)
        {
            PaymentMethod ret = null;
            PaymentMethodEntity selectedMethod = new PaymentMethodEntity(user, paymentMethod);
            try
            {
                selectedMethod = DatabaseHelper.GetPaymentMethod(selectedMethod);
            }
            catch (ResourceNotFoundException) { };
            switch (selectedMethod.Type)
            {
                case EPaymentMethodType.MasterCard:
                    {
                        ret = new MasterCard(selectedMethod);
                        break;
                    }
                case EPaymentMethodType.Visa:
                    {
                        ret = new Visa(selectedMethod);
                        break;
                    }
                case EPaymentMethodType.Bbva:
                    {
                        ret = new Bbva(selectedMethod);
                        break;
                    }
                case EPaymentMethodType.Itau:
                    {
                        ret = new Itau(selectedMethod);
                        break;
                    }
                case EPaymentMethodType.Santander:
                    {
                        ret = new Santander(selectedMethod);
                        break;
                    }
                case EPaymentMethodType.Paganza:
                    {
                        ret = new Paganza(selectedMethod);
                        break;
                    }
                case EPaymentMethodType.Paypal:
                    {
                        ret = new Paypal(selectedMethod);
                        break;
                    }
                default:
                    {
                        throw new InvalidDataException("Invalid payment method");
                    }
            }
            return ret;
        }

        public void InsertPurchase(Purchase purchase)
        {
            DatabaseHelper.InsertPurchase(purchase);
        }

    }
}
