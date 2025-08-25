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


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    AdminCategoryComponent,
    CreateCategoryComponent,
    AdminProductComponent,
    CreateProductComponent
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
