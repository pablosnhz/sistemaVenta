import { Component, inject, signal, WritableSignal } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { OnInit } from '@angular/core';
import { Rol } from '../../../core/models/rol';
import { RolService } from '../../../core/services/rol.service';
import { NgFor, NgIf } from '@angular/common';
import { FormRolComponent } from '../form-rol/form-rol.component';

@Component({
  selector: 'app-lista-rol',
  standalone: true,
  imports: [MatIcon, NgFor, NgIf, FormRolComponent],
  templateUrl: './lista-rol.component.html',
  styleUrl: './lista-rol.component.scss',
})
export class ListaRolComponent implements OnInit {
  modalTest: boolean = false;

  registros: WritableSignal<Rol[]> = signal<Rol[]>([]);

  private rolService = inject(RolService);

  ngOnInit(): void {
    this.getRolesFromService();
  }

  getRolesFromService() {
    this.rolService.getRoles().subscribe({
      next: (response) => {
        if (response && Array.isArray(response)) {
          console.log(`mostrando los datos del service`);

          this.registros.set(response);
          console.log(response);
        }
      },
      error: (error) => {
        window.alert(error.message);
      },
    });
  }

  modalOpen() {
    this.modalTest = true;
  }

  modalClose() {
    if (this.modalTest) {
      this.modalTest = false;
    }
  }
}
