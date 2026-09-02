using System.ComponentModel.DataAnnotations;

namespace CavipetrolTestBack.API.Models.Requests
{
    public class ClienteRequest
    {
        #region Properties
        public Guid Id { get; set; }
        [Required]
        public required string Identificacion { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }
        [Required]
        public required string Email { get; set; }
        #endregion
    }
}
