using System;
using System.Linq;
using System.ServiceModel;

namespace D365.Plugins
{
	public class SumCalculateForContact
	{
		public void Execute(IServiceProvider serviceProvider)
		{
			ITracingService tracingService =
(ITracingService)serviceProvider.GetService(typeof(ITracingService));

			IPluginExecutionContext context = (IPluginExecutionContext)
	serviceProvider.GetService(typeof(IPluginExecutionContext));

			if (context.InputParameters.Contains("Target") &&
	context.InputParameters["Target"] is Entity)
			{
				Entity entity = (Entity)context.InputParameters["Target"];

				// Obtain the organization service reference which you will need for  
				// web service calls.  
				IOrganizationServiceFactory serviceFactory =
				   serviceProvider.GetService(typeof(IOrganizationServiceFactory));
				var service = serviceFactory.CreateOrganizationService(context.UserId);

				try
				{
					if (context.MessageName == "UPDATE")
					{
						if (entity.LogicalName == "CONTACT")
						{
							var preImageContact = context.PreEntityImages["PREIMAGE"];
							var employerSalary = preImageContact["neu_employersalary"];
							var otherIncome = preImageContact.GetAttributeValue<int>("neu_othersalary");

							var helper = new Helper();

							var totalIncome = (int)helper.Sum(employerSalary, otherIncome);

							entity["neu_totalincome"] = totalIncome;

							service.Update(entity);
						}
					}
				}

				catch (FaultException<OrganizationServiceFault> ex)
				{
					throw new InvalidPluginExecutionException("An error occurred in plugin.", ex);
				}

				catch (Exception ex)
				{
					tracingService.Trace("Plugin: {0}", ex.ToString());
					throw;
				}
			}
		}
	}
}
