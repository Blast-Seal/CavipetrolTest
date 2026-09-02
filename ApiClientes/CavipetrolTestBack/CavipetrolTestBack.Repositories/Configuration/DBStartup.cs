using CavipetrolTestBack.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CavipetrolTestBack.Repositories.Configuration
{
    public static class DBStartup
    {
        public static void Initialize(IServiceProvider serviceProvider, string adminEmail)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CavipetrolDBContext>();

                //bool newDatabase = !(context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
                //bool SeedDefaultData = newDatabase || DatabaseIsEmpty(serviceProvider, context);

                context.Database.Migrate();

                //SeedUserStatuses(context, scope.ServiceProvider.GetRequiredService<ILogger<CavipetrolDBContext>>());
                //SeedDocumentTypes(context, scope.ServiceProvider.GetRequiredService<ILogger<CavipetrolDBContext>>());

                //if (!string.IsNullOrEmpty(adminEmail))
                //    CreateAdminUser(scope.ServiceProvider, adminEmail);

                //if (!SeedDefaultData)
                //{
                //    return;
                //}

                //var installFiles = new List<string>
                //{
                //    Path.Combine(AppContext.BaseDirectory, @"App_Data/Install/SqlServer.StoredProcedures.sql"),
                //    Path.Combine(AppContext.BaseDirectory, @"App_Data/Install/SqlServer.Data.sql")
                //};

                //foreach (var installFile in installFiles)
                //{
                //    ExecuteSqlScriptFromFile(context, installFile, serviceProvider);
                //}
            }
        }
    }
}
