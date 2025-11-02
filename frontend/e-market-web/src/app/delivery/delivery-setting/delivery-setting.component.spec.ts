import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliverySettingComponent } from './delivery-setting.component';

describe('DeliverySettingComponent', () => {
  let component: DeliverySettingComponent;
  let fixture: ComponentFixture<DeliverySettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeliverySettingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliverySettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
