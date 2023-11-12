import { Component } from '@angular/core';
import { LocalStorageOperations } from 'src/LocalStorageOperations';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css'],
})
export class TopbarComponent {
  productsUrl: string = 'products';
  shoppingCartUrl: string = 'shopping-cart';
  accountUrl: string = 'users';
  productCount: number = 0;

  constructor() {
    this.OnProductCountChange();
  }
  OnProductCountChange() {
    let productsString = localStorage.getItem('products') || '';
    let productsList = JSON.parse(productsString);
    this.productCount = productsList.length;
  }
}
