using D365.Plugins.Helper;
using D365.Plugins.Model;
using Microsoft.Xrm.Sdk;
using System;

namespace D365.Plugins
{
	public class UpdateEmployeeInfoByApi : PluginBase
	{
		EmployeeRequest request = null;

		public UpdateEmployeeInfoByApi()
		{
		}

		protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
		{
			localContext.TracingService.Trace("Plugin Execution started");
			if (localContext.PluginExecutionContext.InputParameters.Contains("Target") && localContext.PluginExecutionContext.InputParameters["Target"] is Entity)
			{
				Entity entity = (Entity)PluginExecutionContext.InputParameters["Target"];

				if (entity.LogicalName.Equals("CONTACT", System.StringComparison.InvariantCulture))
				{
					request.EmployeeName = entity.Attributes["fullname"];
					request.Salary = entity.Attributes["neu_employersalary"];
					request.Age = entity.Attributes["neu_age"];
					int employeeId = entity.Attributes["neu_employeeid"];
					bool isUpdatedInApi = UpdateEmployeebyApi(request, employeeId);
					if (isUpdatedInApi)
					{
						entity.Attributes["neu_updatedTime"] = DateTime.Now;
						localContext.OrganizationService.Update(entity);
					}
				}
			}
		}

		bool UpdateEmployeebyApi(EmployeeRequest employeeRequest, int employeeId)
		{
			string requestURL = "http://dummy.restapiexample.com/api/v1/update/" + employeeId;
			// Make a API call to above RequestURL 
			// IF Success return True
			// Else throw an exception
			return false;
		}
	}
}