using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Infrastructure.Configuration
{
    public interface IDefaultService
    {
        void SetValidationDictionary(IValidationDictionary validationDictionary);
    }

    public class DefaultService : IDefaultService, IDisposable
    {
        protected IValidationDictionary _validationDictionary;
        public DefaultService()
        {
            _validationDictionary = new CustomValidationDictionary();
        }

        public void Dispose()
        {

        }

        public virtual void SetValidationDictionary(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }
    }
}
