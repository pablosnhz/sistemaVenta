using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Pos.Dto.Dto;
using Pos.Model.Model;
using Pos.Service.Interface;
using Pos.Service.Service;

namespace sistemaventa.Controllers
{
    [Route("api/negocio")]
    [ApiController]
    public class NegocioController : ControllerBase
    {
        private readonly INegocio_Service _negocioService;
        private readonly IMapper _mapper;

        public NegocioController(INegocio_Service negocioService, IMapper mapper)
        {
            _negocioService = negocioService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<NegocioDto>> Get()
        {
            try
            {
                var negocioDto = await _negocioService.Get();
                if (negocioDto == null)
                {
                    return NoContent();
                }
                return Ok(_mapper.Map<Negocio>(negocioDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error al obtener el registro", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] NegocioDto negocioDto)
        {
            try
            {
                if(negocioDto == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Los datos de la empresa son invalidos" });
                }
                var entidad = _mapper.Map<Negocio>(negocioDto);
                var entidadCreada = await _negocioService.Save(entidad);
                return Ok(new { StatusCode = 200, message = "Registro creado con exito!", data = _mapper.Map<NegocioDto>(entidadCreada) });

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    return BadRequest(new { StatusCode = 400, message = "Ya existe un registro con la misma descripcion. Intentelo de nuevo." });
                }
                return StatusCode(500, new { StatusCode = 500, message = "Error al guardar el registro.", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error al guardar el registro.", error = ex.Message });
            }
        }
    }
}
