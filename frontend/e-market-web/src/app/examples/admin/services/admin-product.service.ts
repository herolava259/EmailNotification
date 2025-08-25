import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseProduct, Product, ProductCategory } from '../models/product.model';
import { Observable, ObservableLike } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { createPaginationOption, Page, Pagination } from '../../shared/models/request.model';

@Injectable({
  providedIn: 'root'
})
export class AdminProductService {

  httpClient = inject(HttpClient);

  createCategory(category: ProductCategory): Observable<ProductCategory>{
    return this.httpClient.post<ProductCategory>(`${environment.apiUrl}/categories`, category);
  }

  deleteCategory(publicId: string): Observable<string> {
    const params = new HttpParams().set('publicId', publicId);
    
    return this.httpClient.delete<string>(`${environment.apiUrl}/categories`, { params });
  }

  findAllCategories(): Observable<Page<ProductCategory>> {
    return this.httpClient.get<Page<ProductCategory>>(`${environment.apiUrl}/categories`);
  }

  createProduct(product: BaseProduct): Observable<Product>{
    const formData = new FormData();

    for(let i = 0; i < product.pictures.length; i++){
      formData.append("picture-" + i, product.pictures[i].file)
    }

    const clone = structuredClone(product);
    clone.pictures = [];
    formData.append("dto", JSON.stringify(clone));

    return this.httpClient.post<Product>(`${environment.apiUrl}/products`, formData);
  }

  deleteProduct(publicId: string): Observable<string> {
    const params = new HttpParams().set('publicId', publicId);
    
    return this.httpClient.delete<string>(`${environment.apiUrl}/products`, { params });
  }


  findAllProducts(pageRequest: Pagination): Observable<Page<Product>> {
    const params = createPaginationOption(pageRequest)
    return this.httpClient.get<Page<Product>>(`${environment.apiUrl}/products`, { params });
  } 
  
  constructor() { }
}
