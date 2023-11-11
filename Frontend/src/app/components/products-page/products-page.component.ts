import {
  Component,
  ViewChild,
  ElementRef,
  Input,
  Output,
  EventEmitter,
} from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParams,
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { endpoints } from 'src/app/networking/endpoints';
import { Product } from 'src/app/types/Product';
import { LocalStorageOperations } from 'src/LocalStorageOperations';
import { Filters } from 'src/app/types/Filters';
import { environment } from 'src/environments/environment.development';

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

  @Output() OnAddOrRemoveFromCart: EventEmitter<void> =
    new EventEmitter<void>();

  constructor(private http: HttpClient, private router: Router) {
    this.getProducts();
  }

  updateFilter(filters: Filters) {
    this.nameFilter = filters.nameFilter;
    this.brandFilter = filters.brandFilter;
    this.categoryFilter = filters.categoryFilter;
    this.getProducts();
  }

  onAddOrRemoveFromCart() {
    this.OnAddOrRemoveFromCart.emit();
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
      .get(`${environment.API_HOST}${endpoints.products}`, {
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
