using System;

namespace Dashboard.MVVM.Model
{
    public class Project
    {
        public string Name { get; set; }
        public bool InProgress { get; set; }
        public string Created { get; set; }
        public string Details { get; set; }
        public string Client { get; set; }

        public Color Color { get; set; }


        internal bool Conflicts(Project project)
        {
            return project.Name == Name;
        }
    }
}
