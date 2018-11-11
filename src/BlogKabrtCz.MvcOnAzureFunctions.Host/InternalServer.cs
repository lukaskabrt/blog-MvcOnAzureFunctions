using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogKabrtCz.MvcOnAzureFunctions.Host {
    public class InternalServer : IServer {
        private bool _disposed = false;

        public static InternalServer Instance { get; set; }
        static InternalServer() {
            var builder = new WebHostBuilder()
                .UseStartup<Api.Startup>();
            Instance = new InternalServer(builder);
        }

        public IHttpApplication<Microsoft.AspNetCore.Hosting.Internal.HostingApplication.Context> Application;
        public IWebHost Host;

        public InternalServer(IWebHostBuilder builder)
                    : this(builder, new FeatureCollection()) {
        }

        public InternalServer(IWebHostBuilder builder, IFeatureCollection featureCollection) {
            var host = builder.UseServer(this).Build();
            host.StartAsync().GetAwaiter().GetResult();
            this.Host = host;
        }

        public IFeatureCollection Features { get; }

        public void Dispose() {
            if (!_disposed) {
                _disposed = true;
                this.Host.Dispose();
            }
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken) {
            this.Application = (IHttpApplication<Microsoft.AspNetCore.Hosting.Internal.HostingApplication.Context>)application;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}
