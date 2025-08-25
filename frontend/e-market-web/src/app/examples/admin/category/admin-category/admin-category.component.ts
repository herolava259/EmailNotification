import { Component, effect, inject } from '@angular/core';
import { AdminProductService } from '../../services/admin-product.service';
import { ToastService } from '../../../shared/services/toast.service';
import { injectQueryClient, injectQuery, injectMutation } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'emartket-admin-category',
  standalone: false,
  templateUrl: './admin-category.component.html',
  styleUrl: './admin-category.component.scss'
})
export class AdminCategoryComponent {

  constructor() {
    effect(() => this.handleCategoryQueryError());
  }

  productAdminService = inject(AdminProductService);
  
  toastService = inject(ToastService);  

  queryClient = injectQueryClient();

  categoriesQuery = injectQuery(() =>({
    queryKey: ['categories'],
    queryFn: () => lastValueFrom(this.productAdminService.findAllCategories()),
  }));

  deleteMutation = injectMutation(() => ({
    mutationFn: (categoryPublicId: string) => lastValueFrom(this.productAdminService.deleteCategory(categoryPublicId)),
    onSuccess: () => this.onDeletionSuccess(),
    onError: () => this.onDeletionError(),
  }));

  private onDeletionSuccess(): void {
    this.queryClient.invalidateQueries({queryKey: ['categories']});
    this.toastService.show('Category deleted', 'SUCCESS');
  }

  private onDeletionError(): void{
    this.toastService.show('Issue when deleting category', 'ERROR');

  }

  private handleCategoryQueryError(): void {
    if(this.categoriesQuery.isError()){
      this.toastService.show(
        'Error! Failed to load categories. please try again later.',
        'ERROR'
      );
    }
  }

  deleteCategory(publicId: string): void {
    this.deleteMutation.mutate(publicId);
  }
}
