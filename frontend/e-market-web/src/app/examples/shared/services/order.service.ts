import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { createPaginationOption, Page, Pagination } from '../models/request.model';
import { Observable } from 'rxjs';
import { AdminOrderDetail, UserOrderDetail } from '../models/order.model';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor() { }

  httpClient = inject(HttpClient)

  getOrdersForConnectedUser(pageRequest: Pagination): Observable<Page<UserOrderDetail>>
  {
    const params = createPaginationOption(pageRequest);

    return this.httpClient.get<Page<UserOrderDetail>>(`${environment.apiUrl}/orders/user`, {params})
  }

  getOrdersForAdmin(pageRequest: Pagination): Observable<Page<AdminOrderDetail>> {
    const params = createPaginationOption(pageRequest);

    return this.httpClient.get<Page<AdminOrderDetail>>(`${environment.apiUrl}/orders/admin`, {params});

  }
}
