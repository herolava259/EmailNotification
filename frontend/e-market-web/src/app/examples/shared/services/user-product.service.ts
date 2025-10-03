import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { createPaginationOption, Page, Pagination } from '../models/request.model';
import { Observable } from 'rxjs';
import { Product, ProductCategory, ProductFilter } from '../../admin/models/product.model';
import { environment } from '../../../../environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class UserProductService {

  httpClient = inject(HttpClient)
  
  findAllFeaturedProducts(pageRequest: Pagination): Observable<Page<Product>>{
    const params = createPaginationOption(pageRequest);

    return this.httpClient.get<Page<Product>>(`${environment.apiUrl}/product-shop/featured`, );
  }

  findOneByPublicId(publicId: string): Observable<Product>{
    return this.httpClient.get<Product>(`${environment.apiUrl}/products-shop/find-one`, {params: {publicId}});
  }

  findRelatedProducts(pageRequest: Pagination, productPublicId: string): Observable<Page<Product>>{
    let params = createPaginationOption(pageRequest);

    params = params.append('publicId', productPublicId);

    return this.httpClient.get<Page<Product>>(`${environment.apiUrl}/product-shop/related`, {params});
  }

  constructor() { }

  findAllCategories(): Observable<ProductCategory>{
    return this.httpClient.get<ProductCategory>(`${environment.apiUrl}/categories`);
  }

  public filter(
    pageRequest: Pagination,
    productFilter: ProductFilter
  ): Observable<Page<Product>>{

    let params = createPaginationOption(pageRequest);
    if(productFilter.category){
      params = params.append('categoryId', productFilter.category);
    }

    if(productFilter.size){
      params = params.append('productSizes', productFilter.size);
    }

    return this.httpClient.get<Page<Product>>(`${environment.apiUrl}/product-shop/filter`, {params});

  }
}
