using System;

namespace Dashboard.MVVM.Model
{
    public class Project
    {
        public string ProjectName { get; set; }
        public string Category { get; set; }
        public bool InProgress { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Details { get; set; }
        public Client Client { get; set; }


        internal bool Conflicts(Project project)
        {
            return project.ProjectName == ProjectName;
        }
    }
}
