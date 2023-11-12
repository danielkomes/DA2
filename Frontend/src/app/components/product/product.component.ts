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
import { endpoints } from 'src/app/networking/endpoints';
import { Product } from 'src/app/types/Product';
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
  @Output() OnAddToCart: EventEmitter<void> = new EventEmitter<void>();
  @Output() OnRemoveFromCart: EventEmitter<void> = new EventEmitter<void>();
  @Output() OnAddOrRemoveFromCart: EventEmitter<void> =
    new EventEmitter<void>();

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.product = this.receivedProduct!;
  }

  addToCart() {
    LocalStorageOperations.addProductToStorage(this.product);
    this.OnAddToCart.emit();
    this.OnAddOrRemoveFromCart.emit();
  }

  removeFromCart() {
    LocalStorageOperations.removeProductFromStorage(this.product);
    this.OnRemoveFromCart.emit();
    this.OnAddOrRemoveFromCart.emit();
  }
}
