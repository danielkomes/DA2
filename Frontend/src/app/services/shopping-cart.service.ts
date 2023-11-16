import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { endpoints } from '../networking/endpoints';
import { IShoppingCart } from '../interfaces/shopping-cart.interface';
import { IShoppingCartResponse } from '../models/shopping-cart-response.interface';

@Injectable({
  providedIn: 'root',
})
export class ShoppingCartService implements IShoppingCart {
  constructor(private http: HttpClient) {}

  public calculateTotal(products: string[]): IShoppingCartResponse {
    return this.http.post<IShoppingCartResponse>(
      `${environment.API_HOST}${endpoints.shoppingCart}`,
      { products }
    );
  }
}
