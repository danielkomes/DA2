import { Component, EventEmitter, Output } from '@angular/core';
// import { NgModel } from '@angular/forms';
import { Filters } from 'src/app/types/Filters';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {
  nameFilter: string = '';
  brandFilter!: string;
  categoryFilter!: string;

  @Output() OnFilterUpdate: EventEmitter<Filters> = new EventEmitter<Filters>();

  update() {
    const filters: Filters = new Filters(
      this.nameFilter,
      this.brandFilter,
      this.categoryFilter
    );
    this.OnFilterUpdate.emit(filters);
  }
}
