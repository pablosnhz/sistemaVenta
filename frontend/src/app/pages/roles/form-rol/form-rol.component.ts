import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { FormsModule, NgForm } from '@angular/forms';
import { Rol } from '../../../core/models/rol';

@Component({
  selector: 'app-form-rol',
  imports: [MatIcon, FormsModule],
  standalone: true,
  templateUrl: './form-rol.component.html',
  styleUrl: './form-rol.component.scss',
})
export class FormRolComponent {
  @Output() cerrar = new EventEmitter<void>();

  @Input() rol: Rol = { idRol: 0, descripcion: '', estado: 'Activo' };
  @Output() guardar = new EventEmitter<Rol>();

  guardarForm(rolForm: NgForm) {
    rolForm.form.markAllAsTouched();
    if (rolForm.invalid) {
      window.alert('Se deben especificar todos los campos obligatorios.');
      return;
    }
    this.guardar.emit(this.rol);
  }

  cerrarModal() {
    this.cerrar.emit();
  }
}
