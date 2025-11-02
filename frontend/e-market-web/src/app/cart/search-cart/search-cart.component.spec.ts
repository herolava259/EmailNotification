import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchCartComponent } from './search-cart.component';

describe('SearchCartComponent', () => {
  let component: SearchCartComponent;
  let fixture: ComponentFixture<SearchCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchCartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
