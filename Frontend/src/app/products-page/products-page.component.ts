import { Component, ViewChild, ElementRef, Input } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParams,
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiConfig } from 'src/ApiConfig';
import { Product } from 'src/Types/Product';
import { Utilities } from 'src/Utilities';
import { Filters } from 'src/Types/Filters';

@Component({
  selector: 'app-products-page',
  templateUrl: './products-page.component.html',
  styleUrls: ['./products-page.component.css'],
})
export class ProductsPageComponent {
  products!: Product[];
  nameFilter!: string;
  brandFilter!: string;
  categoryFilter!: string;

  constructor(private http: HttpClient, private router: Router) {
    console.log('constructor');
    this.getProducts();
  }

  updateFilter(filters: Filters) {
    this.nameFilter = filters.nameFilter;
    this.brandFilter = filters.brandFilter;
    this.categoryFilter = filters.categoryFilter;
    this.getProducts();
  }

  getProducts() {
    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');

    let params = new HttpParams();
    if (this.nameFilter != undefined)
      params = params.set('name', this.nameFilter);
    if (this.brandFilter != undefined)
      params = params.set('brand', this.brandFilter);
    if (this.categoryFilter != undefined)
      params = params.set('category', this.categoryFilter);

    // Make the POST request
    this.http
      .get(`${ApiConfig.route}${ApiConfig.products}`, {
        headers,
        observe: 'response',
        params: params,
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            this.products = response.body;
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
