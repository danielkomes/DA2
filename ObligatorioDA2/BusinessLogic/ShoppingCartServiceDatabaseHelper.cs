﻿using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;
using IDataAccess;
using Promotions;

namespace BusinessLogic
{
    public class ShoppingCartServiceDatabaseHelper : IShoppingCartServiceDatabaseHelper
    {
        private readonly IService<Product> ProductService;
        private readonly IService<PromotionEntity> PromotionService;
        private readonly IService<Purchase> PurchaseService;
        private readonly IService<PaymentMethodEntity> PaymentMethodService;

        public ShoppingCartServiceDatabaseHelper(
            IService<Product> productService,
            IService<PromotionEntity> promotionService,
            IService<Purchase> purchaseService,
            IService<PaymentMethodEntity> paymentMethodService
            )
        {
            ProductService = productService;
            PromotionService = promotionService;
            PurchaseService = purchaseService;
            PaymentMethodService = paymentMethodService;
        }

        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids)
        {
            IEnumerable<Product> products = new List<Product>();
            foreach (Guid id in ids)
            {
                products = products.Append(GetProduct(id));
            }
            return products;
        }

        public Product GetProduct(Guid id)
        {
            Product p = new Product()
            {
                Id = id
            };
            return ProductService.Get(p);
        }

        public IEnumerable<PromotionAbstract> GetPromotions()
        {
            IEnumerable<PromotionEntity> entities = PromotionService.GetAll();
            IEnumerable<PromotionAbstract> ret = new List<PromotionAbstract>();
            foreach (PromotionEntity entity in entities)
            {
                switch (entity.Type)
                {
                    case EPromotionType.Promotion20Off:
                        ret = ret.Append(new Promotion20Off(entity));
                        break;
                    case EPromotionType.Promotion3x2:
                        ret = ret.Append(new Promotion3x2(entity));
                        break;
                    case EPromotionType.PromotionTotalLook:
                        ret = ret.Append(new PromotionTotalLook(entity));
                        break;
                }
            }
            return ret;
        }

        public PaymentMethodEntity GetPaymentMethod(PaymentMethodEntity paymentMethod)
        {
            return PaymentMethodService.Get(paymentMethod);
        }

        public void InsertPurchase(Purchase purchase)
        {
            PurchaseService.Add(purchase);
            bool paymentMethodExists = PaymentMethodService.Exists(purchase.PaymentMethod);
            if (!paymentMethodExists)
            {
                PaymentMethodService.Add(purchase.PaymentMethod);
            }
        }
    }
}
