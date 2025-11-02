import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogTagComponent } from './catalog-tag.component';

describe('CatalogTagComponent', () => {
  let component: CatalogTagComponent;
  let fixture: ComponentFixture<CatalogTagComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CatalogTagComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CatalogTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
