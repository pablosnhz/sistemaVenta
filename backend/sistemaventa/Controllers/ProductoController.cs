using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Pos.Dto.Dto;
using Pos.Model.Model;
using Pos.Service.Service;

namespace sistemaventa.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly Producto_Service _productoService;
        private readonly IMapper _mapper;

        public ProductoController(Producto_Service productoService, IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetAll()
        {
            try
            {
                var entidad = await _productoService.GetAll();
                var entidadDto = _mapper.Map<List<ProductoDto>>(entidad);
                return Ok(entidadDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el listado de registro.", ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetById(int id)
        {
            try
            {
                var entidad = await _productoService.GetById(id);
                if (entidad == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Registro no encontrado" });
                }
                return Ok(_mapper.Map<ProductoDto>(entidad));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error al crear el obtener el registro.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDto productoDto)
        {
            if(!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(p => p.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { StatusCode = 400, message = "Los datos proporcionados son invalidos", errores = errores ?? new List<string>() });
            }

            try
            {
                var entidad = _mapper.Map<Producto>(productoDto);
                var entidadCreada = await _productoService.Create(entidad);
                return Ok(new { StatusCode = 200, message = "Registro creado con exito!", data = _mapper.Map<ProductoDto>(entidadCreada) });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    string campoDuplicado = "un valor unico";

                    // verificamos que no devuelva un valor nulo
                    if(!string.IsNullOrEmpty(pgEx.ConstraintName))
                    {
                        if (pgEx.ConstraintName.Contains("IX_Productos_codigoBarra"))
                            campoDuplicado = "el codigo de barra";
                        else if (pgEx.ConstraintName.Contains("IX_Productos_descripcion"))
                            campoDuplicado = "el nombre";

                    }

                    return BadRequest(new { StatusCode = 400, message = $"Ya existe un registro con {campoDuplicado} ingresado. Intentelo nuevamente." });
                }
                return StatusCode(500, new { StatusCode = 500, message = "Error al crear el registro.", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error al crear el registro.", error = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(p => p.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { StatusCode = 400, message = "Los datos proporcionados son invalidos", errores = errores ?? new List<string>() });
            }

            try
            {
                var entidad = _mapper.Map<Producto>(productoDto);
                var entidadActualizada = await _productoService.Update(entidad);
                return Ok(new { StatusCode = 200, message = "Registro actualizado con exito!", data = _mapper.Map<ProductoDto>(entidadActualizada) });
            }
            catch(KeyNotFoundException ex) { 
                return NotFound(new { StatusCode = 404, error = ex.Message });
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    string campoDuplicado = "un valor unico";

                    // verificamos que no devuelva un valor nulo
                    if (!string.IsNullOrEmpty(pgEx.ConstraintName))
                    {
                        if (pgEx.ConstraintName.Contains("IX_Productos_codigoBarra"))
                            campoDuplicado = "el codigo de barra";
                        else if (pgEx.ConstraintName.Contains("IX_Productos_descripcion"))
                            campoDuplicado = "el nombre";

                    }

                    return BadRequest(new { StatusCode = 400, message = $"Ya existe un registro con {campoDuplicado} ingresado. Intentelo nuevamente." });
                }
                return StatusCode(500, new { StatusCode = 500, message = "Error al actualizar el registro.", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error al actualizar el registro.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _productoService.Delete(id);
                if (!resultado)
                {
                    return NotFound(new { StatusCode = 404, message = "Registro no encontrado." });
                }
                return Ok(new { StatusCode = 200, message = "Registro eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error al eliminar el registro.", error = ex.Message });
            }
        }
    }
}
