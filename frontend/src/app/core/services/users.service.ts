import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Users } from '../models/users';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private apiUrl = environment.apiUrl + '/usuarios';
  private http = inject(HttpClient);

  getUser(): Observable<Users[]> {
    return this.http
      .get<Users[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  createUser(users: Users): Observable<any> {
    return this.http
      .post<any>(this.apiUrl, users)
      .pipe(catchError(this.handleError));
  }

  updateUser(id: number, users: Users): Observable<any> {
    return this.http
      .put<any>(`${this.apiUrl}/${id}`, users)
      .pipe(catchError(this.handleError));
  }

  deleteUser(id: number): Observable<any> {
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
