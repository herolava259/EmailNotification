import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminCategoryComponent } from './examples/admin/category/admin-category/admin-category.component';
import { roleCheckGuard } from './examples/auth/role-check.guard';
import { CreateProductComponent } from './examples/admin/create-product/create-product.component';
import { HomeComponent } from './examples/home/home.component';
import { ProductDetailsComponent } from './examples/market/product-details/product-details.component';
import { ProductsComponent } from './examples/market/products/products.component';
import { CartComponent } from './examples/market/cart/cart.component';
import { UserOrdersComponent } from './examples/user/user-orders/user-orders.component';
import { CartSuccessComponent } from './examples/market/cart-success/cart-success.component';
import { AdminOrdersComponent } from './examples/admin/admin-orders/admin-orders.component';

const routes: Routes = [
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
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
