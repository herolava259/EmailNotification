import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExamplesRoutingModule } from './examples-routing.module';
import { ExamplesComponent } from './examples.component';
import { AdminCategoryComponent } from '../admin/category/admin-category/admin-category.component';
import { CreateCategoryComponent } from '../admin/category/create-category/create-category.component';
import { AdminProductComponent } from '../admin/admin-product/admin-product.component';
import { CreateProductComponent } from '../admin/create-product/create-product.component';
import { FeaturedComponent } from '../home/featured/featured.component';
import { HomeComponent } from '../home/home.component';
import { ProductCardComponent } from '../market/product-card/product-card.component';
import { ProductDetailsComponent } from '../market/product-details/product-details.component';
import { ProductsComponent } from '../market/products/products.component';
import { ProductsFilterComponent } from '../market/products-filter/products-filter.component';
import { CartComponent } from '../market/cart/cart.component';
import { CartSuccessComponent } from '../market/cart-success/cart-success.component';
import { UserOrdersComponent } from '../user/user-orders/user-orders.component';


@NgModule({
  declarations: [
    ExamplesComponent,
    AdminCategoryComponent,
    CreateCategoryComponent,
    AdminProductComponent,
    CreateProductComponent,
    FeaturedComponent,
    HomeComponent,
    ProductCardComponent,
    ProductDetailsComponent,
    ProductsComponent,
    ProductsFilterComponent,
    CartComponent,
    CartSuccessComponent,
    UserOrdersComponent,
  ],
  imports: [
    CommonModule,
    ExamplesRoutingModule
  ]
})
export class ExamplesModule { }
