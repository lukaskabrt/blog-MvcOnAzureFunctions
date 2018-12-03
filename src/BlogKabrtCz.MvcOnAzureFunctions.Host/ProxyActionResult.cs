using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogKabrtCz.MvcOnAzureFunctions.Host {
    public class ProxyActionResult : IActionResult {
        public async Task ExecuteResultAsync(ActionContext context) {
            context.HttpContext.Features.Set<IServiceProvidersFeature>(null); // remove WebJob host service provider

            await InternalServer.Instance.Application.ProcessRequestAsync(new Microsoft.AspNetCore.Hosting.Internal.HostingApplication.Context() { HttpContext = context.HttpContext });
        }
    }
}
