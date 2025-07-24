import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { Users } from '../../../core/models/users';
import { UsersService } from '../../../core/services/users.service';
import { MatIcon } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormUserComponent } from '../form-user/form-user.component';

@Component({
  selector: 'app-list-user',
  imports: [MatIcon, FormsModule, CommonModule, FormUserComponent],
  standalone: true,
  templateUrl: './list-user.component.html',
  styleUrl: './list-user.component.scss',
})
export class ListUserComponent implements OnInit {
  modalTest: boolean = false;

  registrosUsers: WritableSignal<Users[]> = signal<Users[]>([]);
  editRegistros: Users = {
    idUsuario: 0,
    nombres: '',
    apellidos: '',
    idRol: 0,
    rolDescripcion: '',
    telefono: '',
    email: '',
    clave: '',
    estado: 'Activo',
  };

  allRegisters: Users[] = [];
  searchValue: string = '';

  private usersService = inject(UsersService);

  ngOnInit(): void {
    this.getUsersFromService();
  }

  getUsersFromService() {
    this.usersService.getUser().subscribe({
      next: (response) => {
        if (response && Array.isArray(response)) {
          // agregue this.allRegisters = response; para hacer uso del search por input
          this.allRegisters = response;
          this.registrosUsers.set(response);
          // console.log(response);
        }
      },
      error: (error) => {
        window.alert(error.message);
      },
    });
  }

  guardarUsuario(users: Users) {
    // creo nuevo usuario
    if (users.idUsuario === 0) {
      this.usersService.createUser(users).subscribe({
        next: (userGuardado) => {
          this.registrosUsers.set([...this.registrosUsers(), userGuardado]);
          this.getUsersFromService();
          window.alert('Usuario guardado con exito!');
          this.modalClose();
        },
        error: (error) => {
          window.alert(error.message);
        },
      });
    } else {
      //actualizamos el usuario
      this.usersService.updateUser(users.idUsuario, users).subscribe({
        next: (usuarioUpdate) => {
          const index = this.registrosUsers().findIndex(
            (r) => r.idUsuario === usuarioUpdate.idUsuario
          );
          if (index > -1) {
            const updateUsuario = [...this.registrosUsers()];
            updateUsuario[index] = usuarioUpdate;
            this.registrosUsers.set(updateUsuario);
          }
          this.getUsersFromService();
          window.alert('Usuario actualizado con exito!');
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
      this.registrosUsers.set([...this.allRegisters]);
    } else {
      const filters = this.allRegisters.filter(
        (usuarioFiltrado) =>
          usuarioFiltrado.nombres.toLowerCase().includes(value) ||
          usuarioFiltrado.apellidos.toLowerCase().includes(value) ||
          usuarioFiltrado.rolDescripcion.toLowerCase().includes(value) ||
          usuarioFiltrado.email.toLowerCase().includes(value)
      );
      this.registrosUsers.set(filters);
    }
  }

  eliminarRol(id: number) {
    const deleteConfirm = window.confirm(
      'Estas seguro que deseas borrar este registro?'
    );
    if (deleteConfirm) {
      this.usersService.deleteUser(id).subscribe({
        next: () => {
          this.getUsersFromService();
        },
        error: (error) => {
          window.alert(error.message);
        },
      });
    }
  }

  modalOpen(users: Users | null = null) {
    if (!this.modalTest) {
      this.editRegistros = users
        ? { ...users }
        : {
            idUsuario: 0,
            nombres: '',
            apellidos: '',
            idRol: 0,
            rolDescripcion: '',
            telefono: '',
            email: '',
            clave: '',
            estado: 'Activo',
          };
      this.modalTest = true;
    }
  }

  modalClose() {
    if (this.modalTest) {
      this.modalTest = false;
    }
  }
}
