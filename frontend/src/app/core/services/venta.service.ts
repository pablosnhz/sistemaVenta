import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { Venta } from '../models/venta';

@Injectable({
  providedIn: 'root',
})
export class VentaService {
  private apiUrl = environment.apiUrl + '/ventas';
  private http = inject(HttpClient);

  createVenta(venta: Venta): Observable<Venta> {
    return this.http
      .post<Venta>(this.apiUrl, venta)
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
