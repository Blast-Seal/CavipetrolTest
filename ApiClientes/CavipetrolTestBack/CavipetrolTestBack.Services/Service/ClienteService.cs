using CavipetrolTestBack.DTOs.Contracts;
using CavipetrolTestBack.DTOs.Objects;
using CavipetrolTestBack.DTOs.Objects.DTO;
using CavipetrolTestBack.Infrastructure.Configuration;
using CavipetrolTestBack.Infrastructure.Utils;
using CavipetrolTestBack.Repositories.Configuration;
using CavipetrolTestBack.Repositories.Context;
using CavipetrolTestBack.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Services.Service
{
    public class ClienteService : DefaultService, IClienteService
    {
        #region properties
        private readonly ILogger<ClienteService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly CavipetrolDBContext _context;
        #endregion

        #region constructor
        public ClienteService(ILogger<ClienteService> logger, IUnitOfWork unitOfWork, IRepository<Cliente> clienteRepository, CavipetrolDBContext context)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _clienteRepository = clienteRepository;
            _context = context;
        }
        #endregion

        #region methods
        public Cliente GetById(Guid id)
        {
            try
            {
                return _clienteRepository.GetById(id);
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al obtener los datos");
                _logger.LogError(ex, nameMethod);
                return null;
            }
        }
        public Cliente GetByIdentification(string identificacion)
        {
            try
            {
                return _clienteRepository.Get(x => x.Identificacion == identificacion);
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al obtener los datos");
                _logger.LogError(ex, nameMethod);
                return null;
            }
        }

        public ClienteDto? GetByIdentificationSP(string identificacion)
        {
            try
            {
                string query = "EXEC sp_GetClientByIdentification";
                var parametro = new SqlParameter("Identificacion", identificacion);

                var entidades = _context.EntityFromSql<Cliente>(query, parametro).AsEnumerable();
                var resultadoDto = entidades.Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Identificacion = c.Identificacion,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,                    
                    Email = c.Email,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    UpdatedBy = c.UpdatedBy,
                    UpdatedDate = c.UpdatedDate,
                }).ToList();

                return resultadoDto.Count > 0 ? resultadoDto.First() : null;
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al obtener los datos");
                _logger.LogError(ex, nameMethod);
                return null;
            }
        }

        public List<Cliente> GetAll()
        {
            try
            {
                return _clienteRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al obtener los datos");
                _logger.LogError(ex, nameMethod);
                return null;
            }
        }

        public PagedResult<Cliente> GetAll(String Consecutivo = "", double? Monto = null, double? Flete = null, Int32? EstadoId = -1, Int32? CatalogoId = -1, String NumeroConvenioContrato = "", Int32 page = 1, Int32 pageSize = 10)
        {
            try
            {
                var result = _clienteRepository.GetMany(x => (x.Nombre != ""));
                return result.GetPaged(page, pageSize);
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al obtener los datos");
                _logger.LogError(ex, nameMethod);
                return null;
            }
        }

        public bool Insert(Cliente cliente)
        {
            try
            {
                _clienteRepository.Insert(cliente);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al guardar los datos");
                _logger.LogError(ex, nameMethod);
                return false;
            }
        }

        public bool Update(Cliente cliente)
        {
            try
            {
                _clienteRepository.Update(cliente);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al guardar los datos");
                _logger.LogError(ex, nameMethod);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cliente = _clienteRepository.GetById(id);
                if (cliente != null)
                {
                    _clienteRepository.Delete(cliente);
                }

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                var nameMethod = ExceptionHelper.GetCurrentMethod();
                _validationDictionary.AddError(nameMethod, "Ocurrió un error al eliminar los datos");
                _logger.LogError(ex, nameMethod);
                return false;
            }
        }
        #endregion
    }
}
