import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ShoppingCartPageComponent } from './components/shopping-cart-page/shopping-cart-page.component';
import { UserPageComponent } from './components/user-page/user-page.component';
import { UserPageAdminComponent } from './components/user-page-admin/user-page-admin.component';
import { ProductsPageComponent } from './components/products-page/products-page.component';

const routes: Routes = [
  { path: 'login', component: LoginPageComponent, pathMatch: 'full' },
  { path: 'register', component: RegisterPageComponent, pathMatch: 'full' },
  { path: 'users', component: UserPageComponent, pathMatch: 'full' },
  { path: 'users/admin', component: UserPageAdminComponent, pathMatch: 'full' },
  {
    path: 'shopping-cart',
    component: ShoppingCartPageComponent,
    pathMatch: 'full',
  },
  { path: 'products', component: ProductsPageComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
