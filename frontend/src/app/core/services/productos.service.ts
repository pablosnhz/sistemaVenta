import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { Producto } from '../models/products';

@Injectable({
  providedIn: 'root',
})
export class ProductosService {
  private apiUrl = environment.apiUrl + '/productos';
  private http = inject(HttpClient);

  getProducts(): Observable<Producto[]> {
    return this.http
      .get<Producto[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  createProducts(products: Producto): Observable<any> {
    return this.http
      .post<any>(this.apiUrl, products)
      .pipe(catchError(this.handleError));
  }

  updateProducts(id: number, products: Producto): Observable<any> {
    return this.http
      .put<any>(`${this.apiUrl}/${id}`, products)
      .pipe(catchError(this.handleError));
  }

  deleteProducts(id: number): Observable<any> {
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
