using CavipetrolTestBack.Repositories.Context;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Repositories.Configuration
{
    public interface IDBFactory : IDesignTimeDbContextFactory<CavipetrolDBContext>
    {
    }
}
