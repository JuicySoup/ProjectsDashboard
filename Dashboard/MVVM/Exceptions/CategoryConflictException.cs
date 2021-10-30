using Dashboard.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.MVVM.Exceptions
{
    class CategoryConflictException : Exception
    {

        public Category ExistingCategory { get; }
        public Category IncomingCategory { get; }
        public CategoryConflictException(Category existingCategory, Category incomingCategory)
        {
            ExistingCategory = existingCategory;
            IncomingCategory = incomingCategory;
        }

        public CategoryConflictException(string message, Category existingCategory, Category incomingCategory) : base(message)
        {
            ExistingCategory = existingCategory;
            IncomingCategory = incomingCategory;
        }

        public CategoryConflictException(string message, Exception innerException, Category existingCategory, Category incomingCategory) : base(message, innerException)
        {
            ExistingCategory = existingCategory;
            IncomingCategory = incomingCategory;
        }

    }
}
