using CavipetrolTestBack.API.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CavipetrolTestBack.API.Infrastructure
{
    public class SetValidationDictionaryAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //Se obtienen los servicios que implementan IDefaultService
            var services = context.Controller.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(x => x.FieldType.GetInterface("IDefaultService") != null).ToList();
            foreach (var service in services)
            {
                //Se obtiene el metodo
                var method = service.FieldType.GetInterface("IDefaultService").GetMethod("SetValidationDictionary");
                //Se ejecuta con el ModelState actual
                method.Invoke(service.GetValue(context.Controller), new object[] { new ModelStateWrapper(context.ModelState) });
            }

        }
    }
}
