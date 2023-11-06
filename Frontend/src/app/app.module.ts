import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegisterPageComponent } from './register-page/register-page.component';
import { ProductsPageComponent } from './products-page/products-page.component';
import { ProductComponent } from './product/product.component';
import { UserPageComponent } from './user-page/user-page.component';
import { UserPageAdminComponent } from './user-page-admin/user-page-admin.component';
import { ShoppingCartPageComponent } from './shopping-cart-page/shopping-cart-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { TopbarComponent } from './topbar/topbar.component';

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
