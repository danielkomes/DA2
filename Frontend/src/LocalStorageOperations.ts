import { JsonPipe } from '@angular/common';
import { Product } from './app/types/Product';
import { EventEmitter } from '@angular/core';

export class LocalStorageOperations {
  static OnProductCountChange: EventEmitter<number> =
    new EventEmitter<number>();

  static addProductToStorage(product: Product) {
    const currentProductsJson = localStorage.getItem('products');
    let currentProducts: string[];
    if (currentProductsJson) {
      currentProducts = JSON.parse(currentProductsJson);
      currentProducts.push(product.id);
    } else {
      currentProducts = [product.id];
    }
    localStorage.setItem('products', JSON.stringify(currentProducts));
    this.OnProductCountChange.emit(currentProducts.length);
  }

  static removeProductFromStorage(product: Product) {
    const currentProductsJson = localStorage.getItem('products');
    let currentProducts: string[] = [];
    if (currentProductsJson) {
      currentProducts = JSON.parse(currentProductsJson);
      let productIndex = currentProducts.indexOf(product.id);
      if (productIndex != -1) {
        currentProducts.splice(productIndex, 1);
      }
    }
    localStorage.setItem('products', JSON.stringify(currentProducts));
    this.OnProductCountChange.emit(currentProducts.length);
  }

  static removeProductsKey() {
    localStorage.removeItem('products');
    this.OnProductCountChange.emit(0);
  }

  static getProductsFromStorage() {
    const currentProductsJson = localStorage.getItem('products');
    let currentProducts: string[] = [];
    if (currentProductsJson) {
      currentProducts = JSON.parse(currentProductsJson);
    }
    // let ret: string;
    // ret = `[`;
    // currentProducts.forEach((product) => {
    //   ret += `\"`;
    //   ret += product;
    //   ret += `\"`;
    //   ret += `,`;
    // });
    // if (currentProducts.length > 0) {
    //   ret = ret.substring(0, ret.length - 1);
    // }
    // // ret += currentProducts.toString();
    // ret += `]`;
    // // return ret;

    return currentProducts;
  }
}
