import { User } from '../models/user';
import { Product } from './Product';
import { Promotion } from './Promotion';

export class Purchase {
  user: User;
  promotion?: Promotion;
  date: string;
  products: Product[];

  constructor(
    user: User,
    promotion: Promotion,
    date: string,
    products: Product[]
  ) {
    this.user = user;
    this.promotion = promotion;
    this.date = date;
    this.products = products;
  }
}
