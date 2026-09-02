using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Infrastructure.Configuration
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
        Hashtable Values { get; }
    }
    public class CustomValidationDictionary : IValidationDictionary
    {

        private readonly Hashtable _dictionary;

        bool IValidationDictionary.IsValid
        {
            get
            {
                return _dictionary.Count == 0;
            }
        }

        Hashtable IValidationDictionary.Values
        {
            get
            {
                return _dictionary;
            }
        }

        public CustomValidationDictionary()
        {
            _dictionary = new Hashtable();
        }

        public void AddError(string key, string errorMessage)
        {
            _dictionary.Add(key, errorMessage);
        }
    }
}
