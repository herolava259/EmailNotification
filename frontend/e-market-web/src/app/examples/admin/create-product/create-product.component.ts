import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AdminProductService } from '../services/admin-product.service';
import { Router } from '@angular/router';
import { BaseProduct, CreateProductFormContent, ProductCategory, ProductPicture, ProductSizes, sizes } from '../models/product.model';
import { ToastService } from '../../shared/services/toast.service';
import { injectQuery, injectMutation } from '@tanstack/angular-query-experimental'
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'emartket-create-product',
  standalone: false,
  templateUrl: './create-product.component.html',
  styleUrl: './create-product.component.scss'
})
export class CreateProductComponent {


  public formBuilder = inject(FormBuilder);

  productService = inject(AdminProductService);

  toastService = inject(ToastService);

  router = inject(Router);

  public productPictures = new Array<ProductPicture>();

  name = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});
  description = new FormControl<string>( '', {nonNullable: true, validators: [Validators.required]});
  price = new FormControl<number>(0, {nonNullable: true, validators: [Validators.required]});
  size = new FormControl<ProductSizes>('XS', {nonNullable: true, validators: [Validators.required]});
  category = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});
  brand = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});
  color = new FormControl<string>('', {nonNullable: true, validators: [Validators.required]});
  featured = new FormControl<boolean>(false,{nonNullable: true, validators: [Validators.required]});
  pictures = new FormControl<Array<ProductPicture>>([], {nonNullable: true, validators: [Validators.required]});

  stock = new FormControl<number>(0, {nonNullable: true, validators: [Validators.required]});

  public createForm = this.formBuilder.nonNullable.group<CreateProductFormContent>({
    brand: this.brand,
    color: this.color,
    description: this.description,
    name: this.name,
    price: this.price,
    size: this.size,
    category: this.category,
    featured: this.featured,
    pictures: this.pictures,
    stock: this.stock
  });

  loading = false;

  productAdminService = inject(AdminProductService);

  createMutation = injectMutation(() =>({
    mutationFn: (product: BaseProduct) => lastValueFrom(this.productService.createProduct(product)),
    onSettled: () => this.onCreationSettled(),
    onSuccess: () => this.onCreatingSuccess(),
    onError: () => this.onCreationError(),
  }));

  categoriesQuery = injectQuery(() =>({
      queryKey: ['categories'],
      queryFn: () => lastValueFrom(this.productAdminService.findAllCategories()),
    }));

  create(): void {

    const productToCreate: BaseProduct = {
      brand: this.createForm.getRawValue().brand,
      color: this.createForm.getRawValue().color,
      description: this.createForm.getRawValue().description,
      name: this.createForm.getRawValue().name,
      price: this.createForm.getRawValue().price,
      size: this.createForm.getRawValue().size, 
      category: {
        publicId: this.createForm.getRawValue().category.split("+")[0],
        name: this.createForm.getRawValue().category.split('+')[1]
      },
      featured: this.createForm.getRawValue().featured,
      pictures: this.productPictures,
      nbInStock: this.createForm.getRawValue().stock
    };
    this.loading = true;

    this.createMutation.mutate(productToCreate);
  }

  private extractFileFromTarget(target: EventTarget | null): FileList | null{
    const htmlInputTarget = target as HTMLInputElement;

    if(target === null || htmlInputTarget.files === null)
      return null;
    return htmlInputTarget.files;
  }

  public onUploadNewPicture(target: EventTarget | null){
    this.productPictures = [];
    const pictureFileList = this.extractFileFromTarget(target);

    if(pictureFileList !== null){
      for(let i =0; i < pictureFileList.length; ++i){
        const picture = pictureFileList.item(i);
        if(picture !== null){
          const productPicture = {
            file: picture,
            mimeType: picture.type,
          }

          this.productPictures.push(productPicture);
        }
      }
    }
  }

  private onCreationSettled(){
    this.loading = false;

  }

  private onCreatingSuccess(){
    this.router.navigate(['/admin/products/list']);
    this.toastService.show('Product created successfully', 'SUCCESS');
  }

  private onCreationError(){
    this.toastService.show('Issue when creating product', "ERROR")
  }

  protected readonly sizes = sizes;
}
