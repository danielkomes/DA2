import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiConfig } from 'src/ApiConfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Frontend';
  shoppingCartUrl: string = 'shopping-cart';
}
