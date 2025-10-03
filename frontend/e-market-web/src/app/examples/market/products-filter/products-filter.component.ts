import { Component, effect, inject, input, output } from '@angular/core';
import { FilterProductsFormContent, ProductFilter, ProductFilterForm, sizes } from '../../admin/models/product.model';
import { FormBuilder, FormControl, FormRecord, Validators } from '@angular/forms';

@Component({
  selector: 'emartket-products-filter',
  standalone: false,
  templateUrl: './products-filter.component.html',
  styleUrl: './products-filter.component.scss'
})
export class ProductsFilterComponent {
  sort = input<string>("createdDate,asc");
  size = input<string>();

  productFilter = output<ProductFilter>();

  formBuilder = inject(FormBuilder);

  formFilterProducts = this.formBuilder.nonNullable.group<FilterProductsFormContent>({
    sort: new FormControl<string>(this.sort().split(',')[1], {nonNullable: true, validators: [Validators.required]}),
    size: this.buildSizeFormControl()
  });

  private buildSizeFormControl(): FormRecord<FormControl<boolean>>{
    const sizeFormControl = this.formBuilder.nonNullable.record<FormControl<boolean>>({});

    for(const size of sizes){
      sizeFormControl.addControl(size, new FormControl<boolean>(false, {nonNullable: true}))
    }

    return sizeFormControl;
  }

  constructor(){
    effect(() => this.updateSizeFormValue());
    effect(() => this.updateSortFormValue());
    this.formFilterProducts.valueChanges.subscribe(() => this.onFilterChange(this.formFilterProducts.getRawValue()));
  }
  onFilterChange(filter: Partial<ProductFilterForm>): void {
    const filterProduct: ProductFilter = {
      size: '',
      sort: [`createdDate,${filter.sort}`]
    }

    let sizes: [string, boolean][] = [];

    if(filter.size !== undefined){
      sizes = Object.entries(filter.size);
    }

    for(const [sizeKey, sizeValue] of sizes){
      if(sizeValue){
        if(filterProduct.size?.length === 0){
          filterProduct.size = sizeKey;
        } else {
          filterProduct.size += `${sizeKey}`;
        }
      }
    }

    this.productFilter.emit(filterProduct);
  }

  public getSizeFormGroup() : FormRecord<FormControl<boolean>> {
    return this.formFilterProducts.get('size') as FormRecord<FormControl<boolean>>;
  }

  private updateSizeFormValue(): void {
    if(this.size()){
      const sizes = this.size()!.split(',');
      for(const size of sizes){
        this.getSizeFormGroup().get(size)?.setValue(true, {emitValue: false});
      }
    }
  }

  private updateSortFormValue(): void {
    if(this.sort()){
      this.formFilterProducts.controls.sort.setValue(
        this.sort().split(',')[1],
        { emitEvent: false}
      );
    }
  }

  protected readonly sizes = sizes
}


