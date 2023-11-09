import { Component, Input } from '@angular/core';
import { HttpClient, HttpHeaders, HttpStatusCode } from '@angular/common/http';
import { Product } from 'src/Types/Product';
import { LocalStorageOperations } from 'src/LocalStorageOperations';
import { ApiConfig } from 'src/ApiConfig';

@Component({
  selector: 'app-shopping-cart-page',
  templateUrl: './shopping-cart-page.component.html',
  styleUrls: ['./shopping-cart-page.component.css'],
})
export class ShoppingCartPageComponent {
  products: Product[] = [];
  total: number = 0;

  constructor(private http: HttpClient) {
    this.getCart();
  }

  getCart() {
    const data = LocalStorageOperations.getProductsFromStorage();
    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');
    // Make the POST request
    this.http
      .post(`${ApiConfig.route}${ApiConfig.shoppingCart}`, data, {
        headers: headers,
        observe: 'response',
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            this.products = response.body.checkedProducts;
            this.total = response.body.totalPrice;
            console.log(response.body);
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
    const data = LocalStorageOperations.getProductsFromStorage();
    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', '518F02A4-9F50-48DA-8363-141B64DD6318');
    // Make the POST request
    this.http
      .post(`${ApiConfig.route}${ApiConfig.purhcases}`, data, {
        headers: headers,
        observe: 'response',
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            console.log('PURCHASE DONE', response.body);
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
