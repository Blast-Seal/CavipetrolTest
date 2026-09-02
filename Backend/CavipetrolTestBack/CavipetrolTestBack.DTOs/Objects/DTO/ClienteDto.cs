using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.DTOs.Objects.DTO
{
    public class ClienteDto
    {
        #region Properties
        public Guid Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        #endregion

        #region audit properties
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion
    }
}
