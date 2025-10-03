import { Component, effect, inject } from '@angular/core';
import { injectParams } from 'ngxtension/inject-params';
import { UserProductService } from '../../shared/services/user-product.service';
import { Router } from '@angular/router';
import { Pagination } from '../../shared/models/request.model';
import { interval, lastValueFrom, take } from 'rxjs';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { ToastService } from '../../shared/services/toast.service';
import { CartService } from '../cart.service';
import { Product } from '../../admin/models/product.model';

@Component({
  selector: 'emartket-product-details',
  standalone: false,
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent {
  publicId = injectParams('publicId');

  productService = inject(UserProductService);

  router = inject(Router);
  toastService = inject(ToastService);
  cartService = inject(CartService);

  lastPublicId: string = '';

  pageRequest: Pagination = {
    page: 0,
    size: 20,
    sort: []
  }

  labelAddToCart = 'Add to cart';
  iconAddToCart = 'shopping-cart';

  constructor(){
    effect(() => this.handlePublicIdChange());
    effect(() => this.handleRelatedProductQueryError());
    effect(() => this.handleProductQueryError())
  }

  productQuery = injectQuery(() => ({
    queryKey: ['product', this.publicId()],
    queryFn: () => lastValueFrom(this.productService.findOneByPublicId(this.publicId()!))
  }))

  relatedProductQuery = injectQuery(() => ({
    queryKey: ['related-product', this.publicId(), this.pageRequest],
    queryFn: () => lastValueFrom(this.productService
      .findRelatedProducts(this.pageRequest, this.publicId()!))
  }));

  private handlePublicIdChange() {
    if (this.publicId()) {
      if (this.lastPublicId != this.publicId() && this.lastPublicId !== '') {
        this.relatedProductQuery.refetch();
        this.productQuery.refetch();
      }
      this.lastPublicId = this.publicId()!;
    }
  }

  private handleRelatedProductQueryError(): void{
    if(this.relatedProductQuery.isError()){
      this.toastService.show('Error! Failed to load related products. please try again later. ', 'ERROR');
    }
  }

  private handleProductQueryError(): void{
    if(this.productQuery.isError())
      this.toastService.show('Error! Failed to load product details. please try again later. ', 'ERROR');
  }

  addToCart(productToAdd: Product)
  {
    this.cartService.addToCart(productToAdd.publicId, 'add');

    this.labelAddToCart = 'Added to cart';

    this.iconAddToCart = 'check';

    interval(3000).pipe(take(1)).subscribe(() => {
      this.labelAddToCart = 'Add to cart';
      this.iconAddToCart = 'shopping-cart';
    })
  }
}
