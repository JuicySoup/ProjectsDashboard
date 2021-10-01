using Dashboard.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.MVVM.Exceptions
{
    class ProjectConflictException : Exception
    {
        public Project ExistingProject { get; }
        public Project IncomingProject { get; }
        public ProjectConflictException(Project existingProject, Project incomingProject)
        {
            ExistingProject = existingProject;
            IncomingProject = incomingProject;
        }

        public ProjectConflictException(string message, Project existingProject, Project incomingProject) : base(message)
        {
            ExistingProject = existingProject;
            IncomingProject = incomingProject;
        }

        public ProjectConflictException(string message, Exception innerException, Project existingProject, Project incomingProject) : base(message, innerException)
        {
            ExistingProject = existingProject;
            IncomingProject = incomingProject;
        }

    }
}
