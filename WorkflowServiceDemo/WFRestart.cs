using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WorkflowServices;

namespace WorkflowRestartPOC
{
    class WFRestart
    {
       static void Main(string[] args)
        {
            string siteCollectionUrl = "";

            ClientContext clientContext = new ClientContext(siteCollectionUrl);
            WorkflowServicesManager wfsm = new WorkflowServicesManager(clientContext, clientContext.Web);

            Web site = clientContext.Web;
            clientContext.Load(site, s => s.Url);

            List programList = site.Lists.GetByTitle("");
            clientContext.Load(programList, list => list.Id);

            List BFCRequestList = site.Lists.GetByTitle("");
            clientContext.Load(BFCRequestList, list => list.Id);

            List historyList = site.Lists.GetByTitle("WorkflowHistoryList");
            clientContext.Load(historyList, list => list.Id);

            List taskList = site.Lists.GetByTitle("WorkflowTaskList");
            clientContext.Load(taskList, list => list.Id);

            clientContext.ExecuteQuery();
            Console.WriteLine("Target Site:                 {0}", site.Url);
            Console.WriteLine("Target Site:                 {0}", programList.Id);
            Console.WriteLine("WorkflowHistoryList list ID: {0}", historyList.Id);
            Console.WriteLine("WorkflowTaskList list ID:    {0}", taskList.Id);

            //Using Deployment service
            DeploymentService.ShowAllInstalledWorkflows(ref clientContext, ref wfsm);

            //get a workflow definition
            //Guid WorkflowDefinitionId = DeploymentService.GetOneInstalledWorkflow(ref clientContext, ref wfsm);

            //crete a new association
            //SubscriptionService.CreateAssociation(ref clientContext, ref wfsm, WorkflowDefinitionId, BFCRequestList.Id, historyList.Id, taskList.Id);

            //remove a existing association
            //SubscriptionService.RemoveAssociation(ref clientContext, ref wfsm, WorkflowDefinitionId, BFCRequestList.Id, historyList.Id, taskList.Id);
            Console.ReadLine();



        }
    }
}
