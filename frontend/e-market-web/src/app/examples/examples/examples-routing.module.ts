import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExamplesComponent } from './examples.component';
import { CreateProductComponent } from '../admin/create-product/create-product.component';
import { roleCheckGuard } from '../auth/role-check.guard';
import { AdminCategoryComponent } from '../admin/category/admin-category/admin-category.component';
import { AdminOrdersComponent } from '../admin/admin-orders/admin-orders.component';
import { HomeComponent } from '../home/home.component';
import { ProductDetailsComponent } from '../market/product-details/product-details.component';
import { ProductsComponent } from '../market/products/products.component';
import { CartComponent } from '../market/cart/cart.component';
import { CartSuccessComponent } from '../market/cart-success/cart-success.component';
import { UserOrdersComponent } from '../user/user-orders/user-orders.component';

const routes: Routes = [
  { path: '', component: ExamplesComponent },
    {
    path: 'examples/categories/list',
    component: AdminCategoryComponent,
    canActivate: [roleCheckGuard],
    data: {
      authorities: ['ROLE_ADMIN'],
    }
  },
  {
    path: 'examples/admin/products/create',
    component: CreateProductComponent,
    canActivate: [roleCheckGuard],
    data: {
      authorities: ['ROLE_ADMIN'],
    }
  },
  // {
  //   path: 'admin/products/list',
  //   component: AdminProductsComponent,
  //   canActivate: [roleCheckGuard],
  //   data: {
  //     authorities: ['ROLE_ADMIN'],
  //   },
  // },
  {
    path: 'examples/admin/orders/list',
    component: AdminOrdersComponent,
    canActivate: [roleCheckGuard],
    data: {
      authorities: ['ROLE_ADMIN'],
    },
  },
  {
    path: '/examples',
    component: HomeComponent,
  },
  {
    path: '/examples/product/:publicId',
    component: ProductDetailsComponent,
  },
  {
    path: 'examples/products',
    component: ProductsComponent,
  },
  {
    path: 'examples/cart',
    component: CartComponent,
  },
  {
    path: 'examples/cart/success',
    component: CartSuccessComponent,
  },
  {
    path: 'examples/users/orders',
    component: UserOrdersComponent
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamplesRoutingModule { }
