using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace WebHookService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public void GetWebHook(ReleaseApprovalPendingEvent approvalPending)
        {

            var collectionUri = "https://{your tenant name}.vsrm.visualstudio.com/DefaultCollection";

            string patToken = "<PAT Token>";

            VssConnection connection = new VssConnection(new Uri(collectionUri), new VssBasicCredential("username",patToken));

            ReleaseHttpClient rmClient = connection.GetClient<ReleaseHttpClient>();

            string projectName = approvalPending.resource.project.name;

            var workItem = rmClient.GetReleaseWorkItemsRefsAsync(projectName, approvalPending.resource.release.Id).Result;

            string reAssginedApproverId = "<Approver Id>";

            string approverComment = "good to go";

            string approverReassignedcomment = "Reassigned to Foo team";

            if (approvalPending.resource.approval.Approver.Id == reAssginedApproverId)
            {
                return;
            }
            else if (workItem.Count < 4)
            {
                var approval = new ReleaseApproval() { Status = ApprovalStatus.Approved, Comments = approverComment };

                rmClient.UpdateReleaseApprovalAsync(approval, projectName, approvalPending.resource.approval.Id);
            }
            else 
            {
                var reAssignedApprover = new Microsoft.VisualStudio.Services.WebApi.IdentityRef() { Id = reAssginedApproverId };

                var reAssignedToFooTeam = new ReleaseApproval() { Approver = reAssignedApprover, Status = ApprovalStatus.Reassigned, Comments = approverReassignedcomment };

                rmClient.UpdateReleaseApprovalAsync(reAssignedToFooTeam, projectName, approvalPending.resource.approval.Id);
            }


        }
    }
}