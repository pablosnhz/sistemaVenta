import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Producto } from '../../../core/models/products';
import { ProductosService } from '../../../core/services/productos.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-form-venta',
  imports: [MatIcon, CommonModule, FormsModule],
  standalone: true,
  templateUrl: './form-venta.component.html',
  styleUrl: './form-venta.component.scss',
})
export class FormVentaComponent implements OnInit {
  listaProductos: boolean = false;
  productos: Producto[] = [];

  productoFiltro: string = '';
  productosFiltrados: Producto[] = [];
  codigoProducto: string = '';

  private productoService = inject(ProductosService);

  ngOnInit(): void {
    this.getProductosFromService();
  }

  getProductosFromService() {
    this.productoService.getProducts().subscribe({
      next: (response) => {
        if (response && Array.isArray(response)) {
          this.productos = response;
          this.productosFiltrados = response;
        }
      },
      error: (error) => {
        window.alert(error.message);
      },
    });
  }

  productosFilter() {
    this.listaProductos = this.productoFiltro.length > 0;
    this.productosFiltrados = this.productos.filter((producto) =>
      producto.descripcion
        .toLocaleLowerCase()
        .includes(this.productoFiltro.toLocaleLowerCase())
    );
  }

  selectProduct(producto: Producto) {
    this.productoFiltro = producto.descripcion;
    this.codigoProducto = producto.codigoBarra;
    this.listaProductos = false;

    // hacemos focus al producto seleccionado (input)
    setTimeout(() => {
      const inputProducto = document.querySelector(
        "input[name='producto']"
      ) as HTMLInputElement;
      if (inputProducto) {
        inputProducto.focus();
      }
    }, 500);
  }

  buscarProductoCodigo(): void {
    const productoEncontrado = this.productos.find(
      (producto) => producto.codigoBarra === this.codigoProducto
    );
    if (productoEncontrado) {
      this.productoFiltro = productoEncontrado.descripcion;
      this.listaProductos = false;
      setTimeout(() => {
        const inputProducto = document.querySelector(
          "input[name='producto']"
        ) as HTMLInputElement;
        if (inputProducto) {
          inputProducto.focus();
        }
      }, 100);
    } else {
      window.alert('Producto no encontrado');
      this.productoFiltro = '';
    }
  }

  mostrarListaProductos() {
    this.listaProductos = true;
  }

  cerrarListaProductos() {
    setTimeout(() => {
      this.listaProductos = false;
    }, 150);
  }
}
