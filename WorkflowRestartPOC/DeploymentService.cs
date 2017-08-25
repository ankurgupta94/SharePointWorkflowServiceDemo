using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WorkflowServices;

namespace WorkflowRestartPOC
{
     public class DeploymentService
    {
      public static void ShowAllInstalledWorkflows(ref ClientContext clientContext, ref WorkflowServicesManager wfsm)
        {
            //connect to deployment service
            WorkflowDeploymentService depService = wfsm.GetWorkflowDeploymentService();

            // get all installed workflows
            bool showOnlyPublishWorkflows = true;
            WorkflowDefinitionCollection definitions = depService.EnumerateDefinitions(showOnlyPublishWorkflows);
            clientContext.Load(definitions);
            clientContext.ExecuteQuery();
            foreach(var def in definitions)
            {
                Console.WriteLine("Workflow ID: {0} - Workflow Name: {1}", def.Id, def.DisplayName);
               
            }
        }
       public static Guid GetOneInstalledWorkflow(ref ClientContext clientContext, ref WorkflowServicesManager wfServiceManager)
        {
            WorkflowDeploymentService depService = wfServiceManager.GetWorkflowDeploymentService();
            bool showPublishedWorkflows = true;
            WorkflowDefinitionCollection wfdefinitions = depService.EnumerateDefinitions(showPublishedWorkflows);
            clientContext.Load(wfdefinitions);
            clientContext.ExecuteQuery();
            Console.WriteLine(wfdefinitions.First().DisplayName);
            return wfdefinitions.First().Id;
            
        } 

    }
}
