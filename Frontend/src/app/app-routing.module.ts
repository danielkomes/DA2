import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ShoppingCartPageComponent } from './components/shopping-cart-page/shopping-cart-page.component';
import { UserPageComponent } from './components/user-page/user-page.component';
import { ProductsPageComponent } from './components/products-page/products-page.component';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { UsersListComponent } from './components/admin/users-list/users-list.component';
import { PurchasesListComponent } from './components/admin/purchases-list/purchases-list.component';
import { EditUserPageComponent } from './components/admin/edit-user-page/edit-user-page.component';

const routes: Routes = [
  { path: 'login', component: LoginPageComponent, pathMatch: 'full' },
  { path: 'register', component: RegisterPageComponent, pathMatch: 'full' },
  { path: 'users', component: UserPageComponent, pathMatch: 'full' },
  {
    path: 'shopping-cart',
    component: ShoppingCartPageComponent,
    pathMatch: 'full',
  },
  { path: 'products', component: ProductsPageComponent, pathMatch: 'full' },
  { path: 'admin', component: AdminPanelComponent, pathMatch: 'full' },
  { path: 'admin/users', component: UsersListComponent, pathMatch: 'full' },
  {
    path: 'admin/purchases',
    component: PurchasesListComponent,
    pathMatch: 'full',
  },
  { path: 'admin/users/:email', component: EditUserPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
