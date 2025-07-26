import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormVentaComponent } from './form-venta.component';

describe('FormVentaComponent', () => {
  let component: FormVentaComponent;
  let fixture: ComponentFixture<FormVentaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormVentaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormVentaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
