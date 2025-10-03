import { Component, effect, inject } from '@angular/core';
import { injectQueryParams } from 'ngxtension/inject-query-params'
import { ProductFilter } from '../../admin/models/product.model';
import { UserProductService } from '../../shared/services/user-product.service';
import { Router } from '@angular/router';
import { ToastService } from '../../shared/services/toast.service';
import { Pagination } from '../../shared/models/request.model';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { filter, lastValueFrom } from 'rxjs';


@Component({
  selector: 'emartket-products',
  standalone: false,
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent {

  category = injectQueryParams('category');
  size = injectQueryParams('size');
  sort = injectQueryParams('sort');
  productService = inject(UserProductService);
  router = inject(Router);
  toastService = inject(ToastService);

  pageRequest: Pagination = {
    page: 0, 
    size: 20,
    sort: ['createdDate,desc']
  }

  filterProducts: ProductFilter = {
    category: this.category(),
    size: this.size() ?? "",
    sort: [this.sort() ?? ""]
  }

  lastCategory = "";

  constructor(){
    effect(() => this.handleFilteredProductQueryError());
    effect(() => this.handleParametersChange());
  }

  filteredProductsQuery = injectQuery(() => ({
    queryKey: ['products', this.filterProducts],
    queryFn: () => lastValueFrom(this.productService.filter(this.pageRequest, this.filterProducts))
  }));

  onFilterChange($event: ProductFilter){
    this.filterProducts.category = this.category();
    this.filterProducts = $event;
    this.pageRequest.sort = $event.sort;
    this.router.navigate(['/products'], {
      queryParams:{
        ...$event
      }
    });
    this.filteredProductsQuery.refetch();
  }

  private handleFilteredProductQueryError(): void {
    if(this.filteredProductsQuery.isError()){
      this.toastService.show('Error! Failed to load products, please try again later', 'ERROR')
    }
  }

  private handleParametersChange(): void {
    if(this.category()) {
      if(this.lastCategory != this.category() && this.lastCategory !== ""){
        this.filterProducts = {
          category: this.category(),
          size: this.size() ?? '',
          sort: [this.sort() ?? '']
        }

        this.filteredProductsQuery.refetch();
      }
    }

    this.lastCategory = this.category()!;
  }

  protected readonly filter = filter;
}
