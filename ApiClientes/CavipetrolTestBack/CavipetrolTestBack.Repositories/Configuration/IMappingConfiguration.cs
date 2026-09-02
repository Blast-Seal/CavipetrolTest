using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Repositories.Configuration
{
    public partial interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
