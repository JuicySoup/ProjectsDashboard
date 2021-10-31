using Dashboard.MVVM.Model;
using Dashboard.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Commands
{
    class NewCategoryCommand : CommandBase
    {
        private readonly CategoriesViewModel _projectsViewModel;
        public NewCategoryCommand(CategoriesViewModel projectsViewModel)
        {
            _projectsViewModel = projectsViewModel;
        }
        public override void Execute(object parameter)
        {
            //Category category = new Category()
            //{
            //    Name = _projectsViewModel.CategoryName
            //};
            //_projectsViewModel.ListCategories(category);

        }
    }
}
