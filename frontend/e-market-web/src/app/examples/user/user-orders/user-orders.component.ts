import { Component, inject, PLATFORM_ID } from '@angular/core';
import { OrderService } from '../../shared/services/order.service';
import { Pagination } from '../../shared/models/request.model';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { OrderedItems } from '../../shared/models/order.model';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'emartket-user-orders',
  standalone: false,
  templateUrl: './user-orders.component.html',
  styleUrl: './user-orders.component.scss'
})
export class UserOrdersComponent {
  orderService = inject(OrderService);

  pageRequest: Pagination = {
    page: 0,
    size: 20,
    sort: []
  }

  platformId = inject(PLATFORM_ID);

  ordersQuery = injectQuery(() => ({
    queryKey: ['user-orders', this.pageRequest],
    queryFn: () => lastValueFrom(this.orderService.getOrdersForConnectedUser(this.pageRequest)),
  }));

  computeItemsName(items: OrderedItems[])
  {
    return items.map(item => item.name).join(', ')
  }

  computeItemsQuantity(items: OrderedItems[]): number
  {
    return items.reduce((acc, item) => acc + item.quantity, 0)
  }

  computeTotal(items: OrderedItems[]): number {
    return items.reduce((acc, item) => acc + item.price * item.quantity, 0);
  }

  checkIfPlatformBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }
}
