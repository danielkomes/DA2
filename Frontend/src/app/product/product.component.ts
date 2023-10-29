import { Component, ViewChild, ElementRef, Input } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiConfig } from './../../ApiConfig';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent {
  response: string = '';

  id: string = '';
  name: string = '';
  description: string = '';
  price: number = 0;
  category: string = '';
  colors: string[] = [];

  @Input() product: any;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.id = this.product.id;
    this.name = this.product.name;
    this.description = this.product.description;
    this.price = this.product.price;
    this.category = this.product.category;
    this.colors = this.product.colors;
  }

  addToCart() {
    const postData = this.id;
    console.log(postData);

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');

    // Make the POST request
    this.http
      .post(
        `${ApiConfig.route}${ApiConfig.shoppingCart}/${this.id}`,
        postData,
        {
          headers,
          observe: 'response',
        }
      )
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
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
