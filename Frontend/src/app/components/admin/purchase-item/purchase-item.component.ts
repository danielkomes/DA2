import { Component, Input } from '@angular/core';
import { Product } from 'src/app/types/Product';
import { Purchase } from 'src/app/types/Purchase';

@Component({
  selector: 'app-purchase-item',
  templateUrl: './purchase-item.component.html',
  styleUrls: ['./purchase-item.component.css'],
})
export class PurchaseItemComponent {
  purchase!: Purchase;
  user!: string;
  promotion!: string;
  date!: string;
  products!: Product[];

  @Input() purchaseReceived!: Purchase;

  constructor() {}

  ngOnInit() {
    this.purchase = this.purchaseReceived;
    this.user = this.purchase.user.email;
    this.promotion = this.purchase.promotion?.name || 'None';
    this.date = this.purchase.date;
    this.products = this.purchase.products;
  }
}
