import { Component, effect, inject } from '@angular/core';
import { AdminProductService } from '../services/admin-product.service';
import { ToastService } from '../../shared/services/toast.service';
import { injectMutation, injectQuery, injectQueryClient } from '@tanstack/angular-query-experimental';
import { Pagination } from '../../shared/models/request.model';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'emartket-admin-product',
  standalone: false,
  templateUrl: './admin-product.component.html',
  styleUrl: './admin-product.component.scss'
})
export class AdminProductComponent {


  productService = inject(AdminProductService);
  toastService = inject(ToastService);
  queryClient = injectQueryClient();

  pageRequest: Pagination = {
    page: 0,
    size: 20,
    sort: ['createdDate,desc']
  }

  construct(){
    effect(() => this.handleProductQueryError());
  }
  
  productQuery = injectQuery(() =>({
    queryKey: ['products', this.pageRequest],
    queryFn: () => lastValueFrom(this.productService.findAllProducts(this.pageRequest)),
  }));

  deleteMutation = injectMutation(() => ({
    mutationFn: (productPublicId: string) =>
      lastValueFrom(this.productService.deleteProduct(productPublicId)),
    onSuccess: () => this.onDeletionSuccess(),
    onError: () => this.onDeletionError(),
  }));

  public deleteProduct(publicId: string){
    this.deleteMutation.mutate(publicId);
  }

  private onDeletionSuccess() {
    this.queryClient.invalidateQueries({ queryKey: ['products'] });
    this.toastService.show("Product deleted", "SUCCESS");
  }

  private onDeletionError() {
    this.toastService.show('Issue when deleting product', 'ERROR');
  }

  private handleProductQueryError() {
    if(this.productQuery.isError()){
      this.toastService.show(
        'Error failed to load products, please try again',
        'ERROR'
      );
    }
  }
}
