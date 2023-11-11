import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ProductsPageComponent } from './components/products-page/products-page.component';
import { ProductComponent } from './components/product/product.component';
import { UserPageComponent } from './components/user-page/user-page.component';
import { UserPageAdminComponent } from './components/user-page-admin/user-page-admin.component';
import { ShoppingCartPageComponent } from './components/shopping-cart-page/shopping-cart-page.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { TopbarComponent } from './components/topbar/topbar.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterPageComponent,
    ProductsPageComponent,
    ProductComponent,
    UserPageComponent,
    UserPageAdminComponent,
    ShoppingCartPageComponent,
    LoginPageComponent,
    SidebarComponent,
    TopbarComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
