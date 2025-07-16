using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pos.Model.Model;
using Pos.Service.Interface;

namespace sistemaventa.Controllers
{
    [Route("api/documento")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumento_Service _documentoService;
        private readonly IMapper _mapper;

        public DocumentoController(IDocumento_Service documentoService, IMapper mapper)
        {
            _documentoService = documentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NumeroDocumento>>> Get()
        {
            try
            {
                var documentoDto = await _documentoService.Get();
                if(documentoDto == null)
                {
                    return NoContent();
                }
                return Ok(_mapper.Map<NumeroDocumento>(documentoDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error al obtener el registro", error = ex.Message });
            }
        }
    }
}
