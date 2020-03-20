using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;
namespace D365.Plugins.Helper
{
	public abstract class PluginBase
	{
		protected class LocalPluginContext
		{
			internal IServiceProvider ServiceProvider { get; private set; }
			public IOrganizationService OrganizationService { get; }
			public IPluginExecutionContext PluginExecutionContext { get; }

			private LocalPluginContext() { }

			internal LocalPluginContext(IServiceProvider serviceProvider)
			{
				if (serviceProvider == null)
				{
					throw new ArgumentNullException(nameof(serviceProvider));
				}

				// Obtain the execution context service from the service provider.
				PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
				// Obtain the tracing service from the service provider.
				
				//TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

				// Obtain the organization factory service from the service provider.
				IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

				// Use the factory to generate the organization service.
				OrganizationService = factory.CreateOrganizationService(null);
			}
		}

		private string ChildClassName { get; }

		internal PluginBase(Type childClassName)
		{
			ChildClassName = childClassName.ToString();
		}

		public void Execute(IServiceProvider serviceProvider)
		{
			// Construct the local plug-in context.
			LocalPluginContext localcontext = new LocalPluginContext(serviceProvider);
			try
			{
				ExecuteCrmPlugin(localcontext);
			}
			catch (FaultException<OrganizationServiceFault> e)
			{
				// Handle the exception.
				throw;
			}
			catch (Exception e)
			{
				throw new InvalidPluginExecutionException(e.Message);
			}
		}

		protected virtual void ExecuteCrmPlugin(LocalPluginContext localContext) { }
	}
}