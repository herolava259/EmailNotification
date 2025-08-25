import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AdminProductService } from '../../services/admin-product.service';
import { ToastService } from '../../../shared/services/toast.service';
import { Router } from '@angular/router';
import { CreateCategoryFormContent, ProductCategory, ProductPicture, ProductSizes } from '../../models/product.model';
import { injectMutation } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'emartket-create-category',
  standalone: false,
  templateUrl: './create-category.component.html',
  styleUrl: './create-category.component.scss'
})
export class CreateCategoryComponent {
  formBuilder = inject(FormBuilder);

  productService = inject(AdminProductService);
  toastService = inject(ToastService);

  router = inject(Router);

  name = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});

  public createForm = this.formBuilder.nonNullable.group<CreateCategoryFormContent>({
    name: this.name,

  });

  loading = false;

  createMutation = injectMutation(() => ({
    mutationFn:
      (categoryToCreate: ProductCategory) => lastValueFrom(this.productService.createCategory(categoryToCreate)),
      onSettled: () => this.onCreationSettled(),
      onSuccess: () => this.onCreationSuccess(),
      onError: () => this.onCreationError(),
  }));

  create(): void {
    const categoryToCreate: ProductCategory = {
      name: this.createForm.getRawValue().name,
    };

    this.loading = true;
    this.createMutation.mutate(categoryToCreate);
  }

  private onCreationSettled(): void {
    this.loading = false;
  }

  private onCreationSuccess(): void {
    this.toastService.show('Category created', 'SUCCESS');
    this.router.navigate(['/admin/categories/list']);
  }

  private onCreationError(): void {

  }

}


// name = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});

//   description = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});

//   price = new FormControl<number>(0, {nonNullable: true, validators: [Validators.required]});

//   size = new FormControl<ProductSizes>('XS', {nonNullable: true, validators: [Validators.required]});

//   category = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});

//   brand = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});

//   color = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});

//   featured = new FormControl<boolean>(false, {nonNullable: true, validators: [Validators.required]});

//   pictures = new FormControl<Array<ProductPicture>>([], {nonNullable: true, validators: [Validators.required]});

//   stock = new FormControl<number>(0, {nonNullable: true, validators: [Validators.required]});
