using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WorkflowServices;

namespace WorkflowRestartPOC
{
     public class SubscriptionService
    {
        public static void CreateAssociation(ref ClientContext clientContext, ref WorkflowServicesManager wfsm, Guid workflowDefinitionId, Guid listId, Guid historyListId, Guid taskListId)
        {
            WorkflowSubscriptionService subservice = wfsm.GetWorkflowSubscriptionService();

            Console.WriteLine();
            Console.WriteLine("Creating workflow association...");

            WorkflowSubscription newSubscription = new WorkflowSubscription(clientContext)
            {
                DefinitionId = workflowDefinitionId,
                Enabled = true,
                Name = "Custom Association" + DateTime.Now
            };

            // define startup options
            newSubscription.EventTypes = new List<string> { "ItemAdded", "ItemUpdated", "WorkflowStart" };

            //define history & task list
            newSubscription.SetProperty("HistoryListId", historyListId.ToString());
            newSubscription.SetProperty("TaskListId", taskListId.ToString());

            //create association
            subservice.PublishSubscriptionForList(newSubscription, listId);
            clientContext.ExecuteQuery();
            Console.WriteLine("Workflow association created!");
            Console.ReadLine();


        }
        public static void RemoveAssociation(ref ClientContext clientContext, ref WorkflowServicesManager wfsm, Guid workflowDefinitionId, Guid listId, Guid historyListId, Guid taskListId)
        {
            WorkflowSubscriptionService subservice = wfsm.GetWorkflowSubscriptionService();
            Console.WriteLine();
            Console.WriteLine("Getting Existing subscription");
            WorkflowSubscriptionCollection wfsubscriptions = subservice.EnumerateSubscriptionsByDefinition(workflowDefinitionId);
            clientContext.Load(wfsubscriptions);
            clientContext.ExecuteQuery();
            foreach(var wfsub in wfsubscriptions)
            {
                Console.WriteLine("Subscription ID:{0} - Subscription Name:{1}", wfsub.DefinitionId, wfsub.Name);
            }
            var LastWFDefId = wfsubscriptions.Last().Id;
            string LastWFDefName = wfsubscriptions.Last().Name;
            Console.WriteLine("Last Subscription ID:{0} - Last Subscription ID:{1} ", LastWFDefId.ToString(), LastWFDefName);
            Console.WriteLine("Removing Workflow Association");
            subservice.DeleteSubscription(LastWFDefId);



        }
    }
}
