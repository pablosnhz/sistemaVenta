export interface Producto {
  idProducto: number;
  codigoBarra: string;
  descripcion: string;
  idCategoria: number;
  categoriaDescripcion: string;
  precioVenta: number;
  stock: number;
  stockMinimo: number;
  estado: string;
}
