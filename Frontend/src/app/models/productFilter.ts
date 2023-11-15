export class ProductFilter {
  Name?: string;
  Brand?: string;
  Category?: string;

  constructor(name?: string, brand?: string, category?: string) {
    this.Name = name;
    this.Brand = brand;
    this.Category = category;
  }
}
