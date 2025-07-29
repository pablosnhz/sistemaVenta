import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Producto } from '../../../core/models/products';
import { ProductosService } from '../../../core/services/productos.service';
import { FormsModule } from '@angular/forms';
import { DetalleVenta } from '../../../core/models/detalleVenta';
import { Venta } from '../../../core/models/venta';
import { VentaService } from '../../../core/services/venta.service';

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

  subTotal: number = 0;
  descuento: number = 0;
  TotalGeneral: number = 0;

  bottomSaveActive: boolean = false;
  formBloqueado: boolean = false;

  // post venta
  venta: Venta = {
    idVenta: 0,
    dni: 'Sin identificacion.',
    cliente: 'Cliente mostrador',
    descuento: 0,
    total: 0,
    idUsuario: 13,
  };

  private productoService = inject(ProductosService);
  private ventaService = inject(VentaService);

  ngOnInit(): void {
    this.getProductosFromService();

    setTimeout(() => {
      const inputIdFocus = document.getElementById(
        'identificacion'
      ) as HTMLInputElement;
      if (inputIdFocus) {
        inputIdFocus.focus();
      }
    }, 100);
  }

  moveFocusEnter(nextId: string) {
    setTimeout(() => {
      const nextInput = document.getElementById(nextId) as HTMLInputElement;
      if (nextInput) {
        nextInput.focus();
      }
    }, 100);
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

        this.bottomSaveActive = this.detalleVentas.length > 0;

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

  eliminarProducto(detalle: DetalleVenta): void {
    const confirmar = confirm(
      `Estas seguro que desea eliminar el producto: "${detalle.nombreProducto}"?`
    );
    if (!confirmar) {
      return;
    }

    this.detalleVentas = this.detalleVentas.filter(
      (fila) => fila.idProducto !== detalle.idProducto
    );
    this.totalGeneralVenta();
    this.bottomSaveActive = this.detalleVentas.length > 0;
  }

  calcularTotales(detalle: DetalleVenta): void {
    const cantidad = Number(detalle.cantidad) || 0;
    const precio = Number(detalle.precio) || 0;
    const descuento = Number(detalle.descuento) || 0;

    detalle.total = cantidad * precio - descuento;

    this.totalGeneralVenta();
  }

  totalGeneralVenta(): void {
    const subTotal = this.detalleVentas.reduce(
      (sum, item) => sum + item.precio * item.cantidad,
      0
    );
    const descuento = this.detalleVentas.reduce(
      (sum, item) => sum + item.descuento,
      0
    );
    const totalGeneral = subTotal - descuento;

    this.subTotal = subTotal;
    this.descuento = descuento;
    this.TotalGeneral = totalGeneral;
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

  // venta producto POST saveSale
  guardarVenta(): void {
    if (!this.venta.dni?.trim() || !this.venta.cliente?.trim()) {
      window.alert('Se debe completar todos los campos requeridos.');
      return;
    }
    if (this.detalleVentas.length === 0) {
      window.alert(
        'Se debe agregar al menos un producto antes de guardar la venta.'
      );
      return;
    }

    this.venta.descuento = this.descuento;
    this.venta.total = this.TotalGeneral;
    this.venta.idUsuario = 13;
    this.venta.detalleVentas = this.detalleVentas;

    this.ventaService.createVenta(this.venta).subscribe({
      next: (response: any) => {
        if (response && response.data) {
          this.venta = response.data;
          window.alert('La venta se ha registrado con exito!');

          this.bottomSaveActive = false;
          this.formBloqueado = true;
        }
      },
      error: (error) => {
        window.alert('Error al guardar la venta: ' + error.message);
      },
    });
  }

  nuevaVentaButton(): void {
    this.codigoProducto = '';
    this.productoFiltro = '';
    this.detalleVentas = [];

    this.subTotal = 0;
    this.descuento = 0;
    this.TotalGeneral = 0;
    this.bottomSaveActive = false;
    this.formBloqueado = false;

    setTimeout(() => {
      const inputIdentificacion = document.getElementById(
        'identificacion'
      ) as HTMLInputElement;
      if (inputIdentificacion) {
        inputIdentificacion.value = 'Sin identificacion';
        inputIdentificacion.focus();
      }
      const inputCliente = document.getElementById(
        'cliente'
      ) as HTMLInputElement;
      if (inputCliente) {
        inputCliente.value = 'Cliente mostrador';
      }
      const inputCodigo = document.getElementById('codigo') as HTMLInputElement;
      if (inputCodigo) {
        inputCodigo.value = '';
      }
    }, 100);
  }
}
