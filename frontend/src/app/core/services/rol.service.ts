import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { Rol } from '../models/rol';

@Injectable({
  providedIn: 'root',
})
export class RolService {
  private apiUrl = environment.apiUrl + '/roles';
  private http = inject(HttpClient);

  getRoles(): Observable<Rol[]> {
    return this.http.get<Rol[]>(this.apiUrl).pipe(catchError(this.handleError));
  }

  createRol(rol: Rol): Observable<any> {
    return this.http
      .post<any>(this.apiUrl, rol)
      .pipe(catchError(this.handleError));
  }

  updateRol(id: number, rol: Rol): Observable<any> {
    return this.http
      .put<any>(`${this.apiUrl}/${id}`, rol)
      .pipe(catchError(this.handleError));
  }

  deleteRol(id: number): Observable<any> {
    return this.http
      .delete<any>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let mensajeError = 'Ocurrio un error inesperado.';
    let statusCode = error.status || 0;
    let detallesErrores: any = null;

    if (error.error instanceof ErrorEvent) {
      mensajeError = `Error: ${error.error.message}`;
    } else {
      if (statusCode === 0) {
        mensajeError = 'No hay conexion con el servidor.';
      } else if (error.error) {
        mensajeError =
          error.error.message ||
          `Codigo: ${statusCode}, Mensaje: ${error.message}`;

        if (error.error.errores) {
          detallesErrores.error.error.errrores;
        }
      }
    }
    return throwError(() => ({
      message: mensajeError,
      status: statusCode,
      detalles: detallesErrores,
    }));
  }
}
