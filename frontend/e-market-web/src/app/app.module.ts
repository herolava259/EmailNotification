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
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { SearchbarComponent } from './layout/searchbar/searchbar.component';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { CustomerHomeComponent } from './pages/customer-home/customer-home.component';
import { SearchCartComponent } from './cart/search-cart/search-cart.component';
import { CartItemComponent } from './cart/cart-item/cart-item.component';
import { OrerPageComponent } from './pages/orer-page/orer-page.component';
import { CartItemShopComponent } from './cart/cart-item-shop/cart-item-shop.component';
import { BuyItemComponent } from './cart/buy-item/buy-item.component';
import { CartListComponent } from './cart/cart-list/cart-list.component';
import { CustomerProfileComponent } from './pages/customer-profile/customer-profile.component';
import { OrderListComponent } from './order/order-list/order-list.component';
import { OrderItemComponent } from './order/order-item/order-item.component';
import { ProductItemComponent } from './product/product-item/product-item.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { DeliveryDetailComponent } from './delivery/delivery-detail/delivery-detail.component';
import { ProductListComponent } from './product/product-list/product-list.component';
import { ProductDetailComponent } from './product/product-detail/product-detail.component';
import { OrderDetailComponent } from './order/order-detail/order-detail.component';
import { ShopPageComponent } from './pages/shop-page/shop-page.component';
import { ShopOverviewComponent } from './shop/shop-overview/shop-overview.component';
import { CatalogListComponent } from './catalog/catalog-list/catalog-list.component';
import { CatalogTagComponent } from './catalog/catalog-tag/catalog-tag.component';
import { DeliverySettingComponent } from './delivery/delivery-setting/delivery-setting.component';
import { ProfileDetailComponent } from './profile/profile-detail/profile-detail.component';
import { ProfileSettingComponent } from './profile/profile-setting/profile-setting.component';
import { ProductReviewComponent } from './product/product-review/product-review.component';




@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    AdminOrdersComponent,
    SidebarComponent,
    SearchbarComponent,
    CartPageComponent,
    CustomerHomeComponent,
    SearchCartComponent,
    CartItemComponent,
    OrerPageComponent,
    CartItemShopComponent,
    BuyItemComponent,
    CartListComponent,
    CustomerProfileComponent,
    OrderListComponent,
    OrderItemComponent,
    ProductItemComponent,
    ProductPageComponent,
    DeliveryDetailComponent,
    ProductListComponent,
    ProductDetailComponent,
    OrderDetailComponent,
    ShopPageComponent,
    ShopOverviewComponent,
    CatalogListComponent,
    CatalogTagComponent,
    DeliverySettingComponent,
    ProfileDetailComponent,
    ProfileSettingComponent,
    ProductReviewComponent
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
