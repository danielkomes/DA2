import { Component, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders, HttpStatusCode } from '@angular/common/http';
import { Product } from 'src/app/types/Product';
import { LocalStorageOperations } from 'src/LocalStorageOperations';
import { endpoints } from 'src/app/networking/endpoints';
import { environment } from 'src/environments/environment.development';
import { Router } from '@angular/router';

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

  @Output() OnAddOrRemoveFromCart: EventEmitter<void> =
    new EventEmitter<void>();

  constructor(private http: HttpClient, private router: Router) {
    this.getCart();
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
    const data = LocalStorageOperations.getProductsFromStorage();
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
    const token: string | null = localStorage.getItem('token');
    if (token == null) {
      this.router.navigateByUrl('users');
    }
    const data = LocalStorageOperations.getProductsFromStorage();
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
            localStorage.removeItem('products');
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
