using CavipetrolTestBack.API.Controllers.Bases;
using CavipetrolTestBack.API.Models.Requests;
using CavipetrolTestBack.API.Models.Responses;
using CavipetrolTestBack.DTOs.Objects;
using CavipetrolTestBack.DTOs.Objects.DTO;
using CavipetrolTestBack.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CavipetrolTestBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ApiBaseController
    {
        #region services
        private readonly IClienteService _clienteService;
        #endregion

        #region constructor
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        #endregion

        #region methods
        // GET: api/<ItemController>
        [HttpGet("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseOk), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var clientes = _clienteService.GetAll();

            if (clientes != null)
            {
                return ResponseApi(StatusCodes.Status200OK, clientes);
            }
            else
            {
                var responseError = new ResponseError
                {
                    Message = "Error, no se pueden obtener los clientes",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                    StateCode = StatusCodes.Status400BadRequest,
                };
                return ResponseApi(responseError.StateCode, responseError);
            }
        }

        // GET api/<ItemController>/5
        [HttpGet("[action]/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseOk), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id)
        {
            Cliente cliente = _clienteService.GetById(id);

            if (cliente != null)
            {
                return ResponseApi(StatusCodes.Status200OK, cliente);
            }
            else
            {
                var responseError = new ResponseError
                {
                    Message = "Cliente no encontrado",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                    StateCode = StatusCodes.Status400BadRequest,
                };
                return ResponseApi(responseError.StateCode, responseError);

            }
        }

        // GET api/<ItemController>/5
        [HttpGet("[action]/{identificacion}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseOk), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdentification(string identificacion)
        {
            Cliente cliente = _clienteService.GetByIdentification(identificacion);

            if (cliente != null)
            {
                return ResponseApi(StatusCodes.Status200OK, cliente);
            }
            else
            {
                var responseError = new ResponseError
                {
                    Message = "Cliente no encontrado",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                    StateCode = StatusCodes.Status404NotFound,
                };
                return ResponseApi(responseError.StateCode, responseError);

            }
        }

        // GET api/<ItemController>/5
        [HttpGet("[action]/{identificacion}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseOk), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdentificationSP(string identificacion)
        {
            ClienteDto? cliente = _clienteService.GetByIdentificationSP(identificacion);

            if (cliente != null)
            {
                return ResponseApi(StatusCodes.Status200OK, cliente);
            }
            else
            {
                var responseError = new ResponseError
                {
                    Message = "Cliente no encontrado",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                    StateCode = StatusCodes.Status404NotFound,
                };
                return ResponseApi(responseError.StateCode, responseError);
            }
        }

        // POST api/<ItemController>
        [HttpPost("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseOk), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> New([FromBody] ClienteRequest request)
        {
            var responseError = new ResponseError();
            if (!ModelState.IsValid)
            {
                responseError = new ResponseError
                {
                    Message = "Error, modelo no valido",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                    StateCode = StatusCodes.Status400BadRequest,
                };
                return ResponseApi(responseError.StateCode, responseError);
            }

            Cliente cliente = new Cliente()
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Identificacion = request.Identificacion,
                Email = request.Email,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
            };

            if (_clienteService.Insert(cliente))
            {
                return ResponseApi(StatusCodes.Status200OK, cliente);
            }

            responseError = new ResponseError
            {
                Message = "Error, al crear el Registro",
                Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                StateCode = StatusCodes.Status500InternalServerError,
            };
            return ResponseApi(responseError.StateCode, responseError);

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] ClienteRequest request)
        {
            var responseError = new ResponseError();

            if (!ModelState.IsValid)
            {
                responseError = new ResponseError
                {
                    Message = "Error, modelo no valido",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                    StateCode = StatusCodes.Status400BadRequest,
                };
                return ResponseApi(responseError.StateCode, responseError);
            }

            Cliente cliente = _clienteService.GetById(request.Id);

            cliente.Nombre = request.Nombre;
            cliente.Apellido = request.Apellido;
            cliente.Identificacion = request.Identificacion;
            cliente.Email = request.Email;

            cliente.UpdatedBy = "Admin";
            cliente.UpdatedDate = DateTime.Now;

            if (_clienteService.Update(cliente))
            {
                return ResponseApi(StatusCodes.Status200OK, cliente);
            }

            responseError = new ResponseError
            {
                Message = "Error, no se pudo modificar la información",
                Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                StateCode = StatusCodes.Status500InternalServerError,
            };
            return ResponseApi(responseError.StateCode, responseError);
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseError = new ResponseError();

            if (_clienteService.Delete(id))
            {
                ResponseOk responseOk = new ResponseOk
                {
                    Message = "Registro eliminado correctamente",
                    StateCode = StatusCodes.Status200OK,
                };
                return ResponseApi(StatusCodes.Status200OK, responseOk);
            }
            else
            {
                responseError = new ResponseError
                {
                    Message = "Error al eliminar el registro",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList(),
                    StateCode = StatusCodes.Status500InternalServerError,
                };
                return ResponseApi(responseError.StateCode, responseError);
            }
        }
        #endregion
    }
}
