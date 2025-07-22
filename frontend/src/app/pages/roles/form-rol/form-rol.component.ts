import { Component, EventEmitter, Output } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-form-rol',
  imports: [MatIcon, FormsModule],
  standalone: true,
  templateUrl: './form-rol.component.html',
  styleUrl: './form-rol.component.scss',
})
export class FormRolComponent {
  @Output() cerrar = new EventEmitter<void>();

  descripcion: string = '';
  estado: string = 'Activo';

  guardarRol() {
    const nuevoRol = {
      descripcion: this.descripcion,
      estado: this.estado,
    };
    console.log('Nuevo rol:', nuevoRol);
    // Acá podrías emitir el rol o llamar a un servicio
    this.cerrar.emit(); // Cerramos el modal
  }

  cerrarModal() {
    this.cerrar.emit();
  }
}
