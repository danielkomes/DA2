import { Component, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders, HttpStatusCode } from '@angular/common/http';
import { Product } from 'src/app/types/Product';
import { LocalStorageOperations } from 'src/LocalStorageOperations';
import { endpoints } from 'src/app/networking/endpoints';
import { environment } from 'src/environments/environment.development';
import { Router } from '@angular/router';
import { EPaymentMethodType } from 'src/app/types/EPaymentMethodType';

@Component({
  selector: 'app-shopping-cart-page',
  templateUrl: './shopping-cart-page.component.html',
  styleUrls: ['./shopping-cart-page.component.css'],
})
export class ShoppingCartPageComponent {
  products: Product[] = [];
  totalPrice: number = 0;
  promotionApplied: string = 'None';
  productCount: number = 0;
  productsInCartText!: string;

  paymentMethodValue: string = 'Visa';
  paymentMethods: string[] = [
    'Visa',
    'MasterCard',
    'Santander',
    'ITAU',
    'BBVA',
    'Paypal',
    'Paganza',
  ];
  paymentMethod!: EPaymentMethodType;

  @Output() OnAddOrRemoveFromCart: EventEmitter<void> =
    new EventEmitter<void>();

  constructor(private http: HttpClient, private router: Router) {
    this.getCart();
  }

  convertPaymentMethod() {
    switch (this.paymentMethodValue) {
      case 'Visa': {
        this.paymentMethod = EPaymentMethodType.Visa;
        break;
      }
      case 'MasterCard': {
        this.paymentMethod = EPaymentMethodType.MasterCard;
        break;
      }
      case 'Santander': {
        this.paymentMethod = EPaymentMethodType.Santander;
        break;
      }
      case 'ITAU': {
        this.paymentMethod = EPaymentMethodType.Itau;
        break;
      }
      case 'BBVA': {
        this.paymentMethod = EPaymentMethodType.Bbva;
        break;
      }
      case 'Paypal': {
        this.paymentMethod = EPaymentMethodType.Paypal;
        break;
      }
      case 'Paganza': {
        this.paymentMethod = EPaymentMethodType.Paganza;
        break;
      }
    }
  }

  onSuccess(response: any) {
    this.products = response.checkedProducts;
    this.totalPrice = response.totalPrice;
    this.promotionApplied = response.promotionApplied;
    this.productCount = this.products.length;
    if (this.productCount == 1) {
      this.productsInCartText = ' product in your cart';
    } else {
      this.productsInCartText = ' products in your cart';
    }
  }

  onProductCountChanged() {
    this.OnAddOrRemoveFromCart.emit();
  }

  getCart() {
    // console.log('getCart');
    this.convertPaymentMethod();
    const data = {
      products: LocalStorageOperations.getProductsFromStorage(),
      paymentMethod: {
        type: this.paymentMethod,
      },
    };
    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');
    // Make the POST request
    this.http
      .post(`${environment.API_HOST}${endpoints.shoppingCart}`, data, {
        headers: headers,
        observe: 'response',
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            this.products = response.body.checkedProducts;
            this.totalPrice = response.body.totalPrice;
            this.onSuccess(response.body);
          }
        },
        (error) => {
          console.error('POST Request Error:', error);
          // Handle any errors here
          // this.errorMessage = error.error.message;
        }
      );
  }

  doPurchase() {
    this.convertPaymentMethod();
    const token: string | null = localStorage.getItem('token');
    if (token == null) {
      this.router.navigateByUrl('users');
    }
    const data = {
      products: LocalStorageOperations.getProductsFromStorage(),
      paymentMethod: {
        type: this.paymentMethod,
      },
    };
    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', token!);
    // Make the POST request
    this.http
      .post(`${environment.API_HOST}${endpoints.purhcases}`, data, {
        headers: headers,
        observe: 'response',
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            console.log('PURCHASE DONE', response.body);
            LocalStorageOperations.removeProductsKey();
            this.router.navigateByUrl('products');
          }
        },
        (error) => {
          console.error('POST Request Error:', error);
          this.router.navigateByUrl('users');
          // Handle any errors here
          // this.errorMessage = error.error.message;
        }
      );
  }
}
