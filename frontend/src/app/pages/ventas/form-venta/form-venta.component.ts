import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Producto } from '../../../core/models/products';
import { ProductosService } from '../../../core/services/productos.service';
import { FormsModule } from '@angular/forms';
import { DetalleVenta } from '../../../core/models/detalleVenta';

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

  detalleVentas: DetalleVenta[] = [];

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

  // show detalleVenta
  agregarProductosTabla(): void {
    if (this.productoFiltro.trim()) {
      const productoSeleccionado = this.productos.find(
        (producto) => producto.descripcion === this.productoFiltro
      );

      if (productoSeleccionado) {
        if (productoSeleccionado.stock <= 0) {
          window.alert('El producto no esta disponible: "stock = 0".');
          return;
        }
        const productoRepetido = this.detalleVentas.some(
          (detalle) => detalle.idProducto === productoSeleccionado.idProducto
        );
        if (productoRepetido) {
          window.alert('El producto ya se encuentra en la lista.');
          return;
        }
        const detalleVenta: DetalleVenta = {
          idDetalleVenta: 0,
          idVenta: 0,
          idProducto: productoSeleccionado.idProducto,
          nombreProducto: productoSeleccionado.descripcion,
          precio: productoSeleccionado.precioVenta,
          cantidad: 1,
          descuento: 0,
          total: 0,
        };
        this.detalleVentas.push(detalleVenta);
        this.calcularTotales(detalleVenta);

        this.codigoProducto = '';
        this.productoFiltro = '';

        // pasamos focus al input codigo del producto
        setTimeout(() => {
          const inputCodigo = document.querySelector(
            'input[name="codigo"]'
          ) as HTMLFormElement;
          if (inputCodigo) {
            inputCodigo.focus();
          }
        }, 100);
      }
    }
  }

  calcularTotales(detalle: DetalleVenta): void {
    const cantidad = Number(detalle.cantidad) || 0;
    const precio = Number(detalle.precio) || 0;
    const descuento = Number(detalle.descuento) || 0;

    detalle.total = cantidad * precio - descuento;
  }

  validarCantidadVendida(detalle: DetalleVenta): void {
    const productoSeleccionado = this.productos.find(
      (producto) => producto.idProducto === detalle.idProducto
    );
    if (productoSeleccionado) {
      if (
        detalle.cantidad == null ||
        detalle.cantidad <= 0 ||
        isNaN(detalle.cantidad)
      ) {
        window.alert('Se debe ingresar una cantidad valida mayor a cero');
        detalle.cantidad = 1;
        return;
      }

      if (detalle.cantidad > productoSeleccionado.stock) {
        window.alert(
          'La cantidad no puede ser mayor que el stock disponible. Disponibilidad en inventario: ' +
            productoSeleccionado.stock +
            '.'
        );
        detalle.cantidad = productoSeleccionado.stock;
      } else {
        detalle.editarCantidad = false;
        this.calcularTotales(detalle);
      }
    }
  }

  validarCantidadDescuento(detalle: DetalleVenta): void {
    const productoSeleccionado = this.productos.find(
      (producto) => producto.idProducto === detalle.idProducto
    );
    if (productoSeleccionado) {
      if (
        detalle.descuento == null ||
        detalle.descuento < 0 ||
        isNaN(detalle.descuento)
      ) {
        window.alert(
          'Se debe ingresar un descuento valido mayor o igual a cero'
        );
        detalle.descuento = 0;
        return;
      }

      if (detalle.descuento > productoSeleccionado.precioVenta) {
        window.alert(
          'El descuento no puede ser mayor que el precio de la venta.'
        );
        detalle.descuento = 0;
      } else {
        detalle.editarDescuento = false;
        this.calcularTotales(detalle);
      }
    }
  }

  cerrarInputCantidadVendida(detalle: DetalleVenta, event: MouseEvent): void {
    const inputElement = event.target as HTMLElement;
    if (inputElement.tagName.toLowerCase() != 'input') {
      this.validarCantidadVendida(detalle);
      this.validarCantidadDescuento(detalle);
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
