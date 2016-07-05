using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace WebHookService.Controllers
{
   
    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseResource
    {
        public ReleaseApproval approval { get; set; }

        public Release release { get; set; }

        public Project project { get; set; }
      
    }

   
   
    public class ReleaseApprovalPendingEvent
    {
        public ReleaseResource resource { get; set; }
    }
}