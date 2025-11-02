import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartItemShopComponent } from './cart-item-shop.component';

describe('CartItemShopComponent', () => {
  let component: CartItemShopComponent;
  let fixture: ComponentFixture<CartItemShopComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CartItemShopComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CartItemShopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
