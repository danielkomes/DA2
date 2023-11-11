export class Filters {
  nameFilter!: string;
  brandFilter!: string;
  categoryFilter!: string;

  constructor(name: string, brand: string, category: string) {
    this.nameFilter = name;
    this.brandFilter = brand;
    this.categoryFilter = category;
  }
}
