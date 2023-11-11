import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { endpoints } from 'src/app/networking/endpoints';
import { Purchase } from 'src/app/types/Purchase';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-purchases-list',
  templateUrl: './purchases-list.component.html',
  styleUrls: ['./purchases-list.component.css'],
})
export class PurchasesListComponent {
  purchases: Purchase[] = [];

  constructor(private http: HttpClient, private router: Router) {
    this.getPurchases();
  }

  getPurchases() {
    const token: string | null = localStorage.getItem('token');
    if (token == null) {
      this.router.navigateByUrl('prouducts');
    }
    const headers = new HttpHeaders().set('Authorization', token!);
    this.http
      .get(`${environment.API_HOST}${endpoints.purhcases}`, {
        headers: headers,
      })
      .subscribe(
        (response: any) => {
          this.purchases = response;
        },
        (error) => {
          console.log(error);
          this.router.navigateByUrl('prouducts');
        }
      );
  }
}
