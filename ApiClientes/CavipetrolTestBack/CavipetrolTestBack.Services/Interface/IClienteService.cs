using CavipetrolTestBack.DTOs.Objects;
using CavipetrolTestBack.DTOs.Objects.DTO;
using CavipetrolTestBack.Infrastructure.Configuration;
using CavipetrolTestBack.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Services.Interface
{
    public interface IClienteService : IDefaultService
    {
        Cliente GetById(Guid id);
        Cliente GetByIdentification(string identificacion);
        ClienteDto? GetByIdentificationSP(string identificacion);
        List<Cliente> GetAll();
        PagedResult<Cliente> GetAll(String Consecutivo = "", double? Monto = null, double? Flete = null, Int32? EstadoId = -1, Int32? CatalogoId = -1, String NumeroConvenioContrato = "", Int32 page = 1, Int32 pageSize = 10);
        bool Insert(Cliente cliente);
        bool Update(Cliente cliente);
        bool Delete(int id);
    }
}
