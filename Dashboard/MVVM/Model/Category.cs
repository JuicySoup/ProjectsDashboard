using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.MVVM.Model
{
    class Category
    {
        public string Name { get; set; }


        internal bool Conflicts(Category category)
        {
            return category.Name == Name;
        }
    }
}
