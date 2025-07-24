import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Rol } from '../../../core/models/rol';
import { Users } from '../../../core/models/users';
import { MatIcon } from '@angular/material/icon';
import { RolService } from '../../../core/services/rol.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-form-user',
  imports: [MatIcon, FormsModule, CommonModule],

  templateUrl: './form-user.component.html',
  styleUrl: './form-user.component.scss',
})
export class FormUserComponent implements OnInit {
  @Output() cerrar = new EventEmitter<void>();

  @Input() users: Users = {
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
  @Output() guardar = new EventEmitter<Users>();

  roles: Rol[] = [];

  private rolService = inject(RolService);

  ngOnInit(): void {
    this.cargarRoles();
  }

  cargarRoles(): void {
    this.rolService.getRoles().subscribe({
      next: (response) => {
        this.roles = response;
      },
      error: (error) => {
        window.alert('Error al cargar los roles.' + error);
      },
    });
  }

  guardarForm(usersForm: NgForm) {
    usersForm.form.markAllAsTouched();
    if (usersForm.invalid) {
      window.alert('Se deben especificar todos los campos obligatorios.');
      return;
    }
    this.guardar.emit(this.users);
  }

  cerrarModal() {
    this.cerrar.emit();
  }
}
