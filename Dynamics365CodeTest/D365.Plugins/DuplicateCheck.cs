using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.ServiceModel;

namespace D365.Plugins
{
	public class DuplicateCheck
	{
		public void Execute(IServiceProvider serviceProvider)
		{
			if (PluginExecutionContext.InputParameters.Contains("Target") && PluginExecutionContext.InputParameters["Target"] is Entity)
			{
				Entity entity = (Entity)PluginExecutionContext.InputParameters["Target"];

				try
				{

				}

				catch (FaultException<OrganizationServiceFault> ex)
				{
					throw new InvalidPluginExecutionException("An error occurred in FollowUpPlugin.", ex);
				}

				catch (Exception ex)
				{
					throw;
				}
			}
		}
	}
}
