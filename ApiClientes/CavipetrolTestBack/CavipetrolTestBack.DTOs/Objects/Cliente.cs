using CavipetrolTestBack.DTOs.Extends;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.DTOs.Objects
{
    public class Cliente : MasterBase
    {
        #region Properties
        public required string Identificacion { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        #endregion        
    }
}
