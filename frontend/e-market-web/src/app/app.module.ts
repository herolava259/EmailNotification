import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './layout/navbar/navbar.component';
import { FooterComponent } from './layout/footer/footer.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fontAwesomeIcons, fontAwesomeIconsAny } from './shared/utilities/font-awesome-icons';
import { AdminCategoryComponent } from './examples/admin/category/admin-category/admin-category.component';
import { CreateCategoryComponent } from './examples/admin/category/create-category/create-category.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminProductComponent } from './examples/admin/admin-product/admin-product.component';
import { CreateProductComponent } from './examples/admin/create-product/create-product.component';
import { FeaturedComponent } from './examples/home/featured/featured.component';
import { HomeComponent } from './examples/home/home.component';
import { ProductCardComponent } from './examples/market/product-card/product-card.component';
import { ProductDetailsComponent } from './examples/market/product-details/product-details.component';
import { ProductsComponent } from './examples/market/products/products.component';
import { ProductsFilterComponent } from './examples/market/products-filter/products-filter.component';
import { CartComponent } from './examples/market/cart/cart.component';
import { CartSuccessComponent } from './examples/market/cart-success/cart-success.component';
import { UserOrdersComponent } from './examples/user/user-orders/user-orders.component';
import { AdminOrdersComponent } from './examples/admin/admin-orders/admin-orders.component';



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
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
    AdminOrdersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {
    // This constructor can be used for any additional initialization if needed
    
    // Add FontAwesome icons to the library

    library.add(...fontAwesomeIconsAny);
  }
  
}
