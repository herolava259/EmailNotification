import { Component, input } from '@angular/core';
import { Product } from '../../admin/models/product.model';

@Component({
  selector: 'emartket-product-card',
  standalone: false,
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent {

  product = input.required<Product>()

}
