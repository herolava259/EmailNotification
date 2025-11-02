import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrerPageComponent } from './orer-page.component';

describe('OrerPageComponent', () => {
  let component: OrerPageComponent;
  let fixture: ComponentFixture<OrerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrerPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
