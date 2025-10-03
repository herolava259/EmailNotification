import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { provideServerRouting } from '@angular/ssr';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { serverRoutes } from './app.routes.server';
import { provideNgxStripe } from 'ngx-stripe';
import { environment } from '../environments/environment.development';

@NgModule({
  imports: [AppModule, ServerModule],
  providers: [
    provideServerRouting(serverRoutes),
    provideNgxStripe(environment.stripePublishableKey),
  ],
  bootstrap: [AppComponent],
})
export class AppServerModule {}

