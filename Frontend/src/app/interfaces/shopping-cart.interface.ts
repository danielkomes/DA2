import { Observable } from 'rxjs/internal/Observable';
import { IShoppingCartResponse } from '../models/shopping-cart-response.interface';

export interface IShoppingCart {
  calculateTotal(products: string[]): IShoppingCartResponse;
}
