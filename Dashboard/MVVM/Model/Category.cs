using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.MVVM.Model
{
    class Category
    {
        public string Name { get; set; }

        public string Created { get; set; }

        public Color Color { get; set; }

        //TODO
        //add colors for categories
        //public int MyProperty { get; set; } 

        public ObservableCollection<Project> Projects { get; set; }

        internal bool Conflicts(Category category)
        {
            return category.Name == Name;
        }
    }
}
