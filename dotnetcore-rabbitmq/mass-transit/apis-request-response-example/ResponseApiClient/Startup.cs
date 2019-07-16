using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResponseApiClient.Messaging.Users.Consumers;

namespace ResponseApiClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register MassTransit
            services.AddMassTransit(x =>
            {
                x.AddConsumer<GetUserDetailsConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://fly.rmq.cloudamqp.com/umeawlvd"), hostConfigurator =>
                    {
                        hostConfigurator.Username("umeawlvd");
                        hostConfigurator.Password("O_CTKoSkU1FCHhYIydwlEeaO6NPk7ESE");
                    });

                    cfg.ReceiveEndpoint(host, "getUserDetails", ep =>
                    {
                        ep.ConfigureConsumer<GetUserDetailsConsumer>(provider);
                    });
                }));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
