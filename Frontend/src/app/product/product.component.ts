import { Component, ViewChild, ElementRef, Input } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiConfig } from 'src/ApiConfig';
import { Product } from 'src/Types/Product';
import { Utilities } from 'src/Utilities';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent {
  response: string = '';
  product!: Product;

  @Input() receivedProduct?: Product;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.product = this.receivedProduct!;
  }

  addToCart() {
    Utilities.addProductToStorage(this.product);
    // const postData = this.product.id;
    // console.log(postData);

    // // Define the HTTP headers if needed (e.g., for authentication)
    // const headers = new HttpHeaders().set('Content-Type', 'application/json');
    // headers.set('Access-Control-Allow-Origin', 'true');

    // // Make the POST request
    // this.http
    //   .post(
    //     `${ApiConfig.route}${ApiConfig.shoppingCart}/${this.product.id}`,
    //     postData,
    //     {
    //       headers,
    //       observe: 'response',
    //     }
    //   )
    //   .subscribe(
    //     (response: any) => {
    //       Utilities.addProductToStorage(this.product);
    //     },
    //     (error) => {
    //       console.error('POST Request Error:', error);
    //       // Handle any errors here
    //       // this.errorMessage = error.error.message;
    //     }
    //   );
  }
}
