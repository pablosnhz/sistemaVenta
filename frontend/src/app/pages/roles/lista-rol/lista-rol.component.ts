import { Component, inject, signal, WritableSignal } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { OnInit } from '@angular/core';
import { Rol } from '../../../core/models/rol';
import { RolService } from '../../../core/services/rol.service';
import { NgFor, NgIf } from '@angular/common';
import { FormRolComponent } from '../form-rol/form-rol.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-lista-rol',
  standalone: true,
  imports: [MatIcon, NgFor, NgIf, FormRolComponent, FormsModule],
  templateUrl: './lista-rol.component.html',
  styleUrl: './lista-rol.component.scss',
})
export class ListaRolComponent implements OnInit {
  modalTest: boolean = false;

  registrosRoles: WritableSignal<Rol[]> = signal<Rol[]>([]);
  editRegistros: Rol = { idRol: 0, descripcion: '', estado: 'Activo' };

  //hago busqueda por input
  allRegisters: Rol[] = [];
  searchValue: string = '';

  private rolService = inject(RolService);

  ngOnInit(): void {
    this.getRolesFromService();
  }

  getRolesFromService() {
    this.rolService.getRoles().subscribe({
      next: (response) => {
        if (response && Array.isArray(response)) {
          // agregue this.allRegisters = response; para hacer uso del search por input
          this.allRegisters = response;
          this.registrosRoles.set(response);
        }
      },
      error: (error) => {
        window.alert(error.message);
      },
    });
  }

  guardarRol(rol: Rol) {
    // creo nuevo rol
    if (rol.idRol === 0) {
      this.rolService.createRol(rol).subscribe({
        next: (rolGuardado) => {
          this.registrosRoles.set([...this.registrosRoles(), rolGuardado]);
          this.getRolesFromService();
          window.alert('Registro guardado con exito!');
          this.modalClose();
        },
        error: (error) => {
          window.alert(error.message);
        },
      });
    } else {
      //actualizamos el registro
      this.rolService.updateRol(rol.idRol, rol).subscribe({
        next: (rolUpdate) => {
          const index = this.registrosRoles().findIndex(
            (r) => r.idRol === rolUpdate.idRol
          );
          if (index > -1) {
            const updateRoles = [...this.registrosRoles()];
            updateRoles[index] = rolUpdate;
            this.registrosRoles.set(updateRoles);
          }
          this.getRolesFromService();
          window.alert('Registro actualizado con exito!');
          this.modalClose();
        },
        error: (error) => {
          window.alert(error.message);
        },
      });
    }
  }

  searchRegisters() {
    const value = this.searchValue.toLowerCase().trim();
    if (value === '') {
      this.registrosRoles.set([...this.allRegisters]);
    } else {
      const filters = this.allRegisters.filter(
        (rol) =>
          rol.descripcion.toLowerCase().includes(value) ||
          rol.estado.toLowerCase().includes(value)
      );
      this.registrosRoles.set(filters);
    }
  }

  eliminarRol(id: number) {
    const deleteConfirm = window.confirm(
      'Estas seguro que deseas borrar este registro?'
    );
    if (deleteConfirm) {
      this.rolService.deleteRol(id).subscribe({
        next: () => {
          this.getRolesFromService();
        },
        error: (error) => {
          window.alert(error.message);
        },
      });
    }
  }

  modalOpen(rol: Rol | null = null) {
    if (!this.modalTest) {
      this.editRegistros = rol
        ? { ...rol }
        : { idRol: 0, descripcion: '', estado: 'Activo' };
      this.modalTest = true;
    }
  }

  modalClose() {
    if (this.modalTest) {
      this.modalTest = false;
    }
  }
}
