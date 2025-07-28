import { DetalleVenta } from './detalleVenta';

export interface Venta {
  idVenta: number;
  factura?: string;
  fecha?: Date;
  dni: string;
  cliente: string;
  descuento: number;
  total: number;
  idUsuario: number;
  estado?: string;
  fechaAnula?: Date;
  motivo?: string;
  usuarioAnula?: number;

  detalleVentas?: DetalleVenta[];
}
