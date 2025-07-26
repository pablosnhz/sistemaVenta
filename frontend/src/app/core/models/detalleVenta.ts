export interface DetalleVenta {
  idDetalleVenta: number;
  idVenta: number;
  idProducto: number;
  nombreProducto: string;
  precio: number;
  cantidad: number;
  descuento: number;
  total: number;
}
