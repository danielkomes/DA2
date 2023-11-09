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
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiConfig } from 'src/ApiConfig';
import { Product } from 'src/Types/Product';
import { LocalStorageOperations } from 'src/LocalStorageOperations';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent {
  response: string = '';
  product!: Product;
  // removeFromCartButton!: boolean;

  @Input() receivedProduct?: Product;
  @Input() showRemoveFromCartButton?: boolean;
  @Output() OnRemoveFromCart: EventEmitter<void> = new EventEmitter<void>();

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.product = this.receivedProduct!;
  }

  addToCart() {
    LocalStorageOperations.addProductToStorage(this.product);
  }

  removeFromCart() {
    LocalStorageOperations.removeProductFromStorage(this.product);
    this.OnRemoveFromCart.emit();
  }
}
