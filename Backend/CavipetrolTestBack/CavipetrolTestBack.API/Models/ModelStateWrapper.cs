using CavipetrolTestBack.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;

namespace CavipetrolTestBack.API.Models
{
    public class ModelStateWrapper : IValidationDictionary
    {
        public ModelStateWrapper() { }
        private readonly ModelStateDictionary _modelState;

        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        #region IValidationDictionary Members

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }

        public bool IsValid
        {
            get { return _modelState.IsValid; }
        }

        public Hashtable Values
        {
            get
            {
                var dictionary = new Hashtable();

                _modelState.Where(x => x.Value.Errors.Count > 0).ToList().ForEach(f => dictionary.Add(f.Key, f.Value));

                return dictionary;
            }
        }
        #endregion
    }
}
