import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaRolComponent } from './lista-rol.component';

describe('ListaRolComponent', () => {
  let component: ListaRolComponent;
  let fixture: ComponentFixture<ListaRolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaRolComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaRolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
