import { Component, Input } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { getLocaleEraNames } from '@angular/common';
import { Product } from 'src/Types/Product';
import { Utilities } from 'src/Utilities';
import { ApiConfig } from 'src/ApiConfig';

@Component({
  selector: 'app-shopping-cart-page',
  templateUrl: './shopping-cart-page.component.html',
  styleUrls: ['./shopping-cart-page.component.css'],
})
export class ShoppingCartPageComponent {
  products!: Product[];

  constructor(private http: HttpClient) {
    this.getCart();
  }

  getCart() {
    const data = JSON.parse(localStorage.getItem('products')!);
    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');

    // Make the POST request
    this.http
      .request('get', `${ApiConfig.route}${ApiConfig.shoppingCart}`, {
        headers: headers,
        observe: 'response',
        body: data,
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            this.products = response.checkedProducts;
          }
        },
        (error) => {
          console.error('POST Request Error:', error);
          // Handle any errors here
          // this.errorMessage = error.error.message;
        }
      );
  }
}
