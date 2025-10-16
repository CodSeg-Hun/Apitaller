using ApiTaller.Repositorio;
using ApiTaller.Repositorio.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTaller
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cadenaConexionSqlConfiguracion = new AccesoDatos(Configuration.GetConnectionString("SQL"));
            services.AddSingleton(cadenaConexionSqlConfiguracion);

            services.AddSingleton<IConsultarEnMemoria, ConsultarSQLServer>();
            services.AddSingleton<IUsuariosSQLServer, UsuariosSQLServer>();
            services.AddSingleton<IChequeo, ChequeoSQL>();
            services.AddSingleton<ITrabajo, TrabajoSQL>();
            services.AddSingleton<IRecepcion, RecepcionSQL>();
            services.AddSingleton<IServicio, ServicioSQL>();
            services.AddSingleton<IActaEntrega, ActaEntregaSQL>();

            //services.AddControllers();
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiTaller", Description = "API de Taller", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                //para activar los comentarios
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorizacion JWT esquema. \r\n\r\n Escribe 'Bearer' [espacio] y escribe tu token.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["JWT:ClaveSecreta"]))
                };
            });

            services.AddCors(p => p.AddPolicy("PolicyCors", build =>
            {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.DefaultModelsExpandDepth(-1); //quitar schemas de la pantalla
            //    c.SwaggerEndpoint("./v1/swagger.json", "ApiTaller v1");

            //});
            //app.UseHttpsRedirection();
            //app.UseRouting();
            //app.Use(async (context, next) =>
            //{
            //    // Evita clickjacking
            //    context.Response.Headers["X-Frame-Options"] = "DENY";
            //    // Cabeceras adicionales de seguridad
            //    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            //    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
            //    context.Response.Headers["Referrer-Policy"] = "no-referrer";
            //    context.Response.Headers["Content-Security-Policy"] = "frame-ancestors 'self';";
            //    await next();
            //});
            ////app.UseHttpsRedirection();           
            //app.UseAuthorization();
            //app.UseCors("PolicyCors");
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            //app.UseCors();
            // Middleware de seguridad debe ir ANTES de Swagger y después de HTTPS redirection
            app.UseHttpsRedirection();
            app.UseRouting();
            // Cabeceras de seguridad
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["X-Frame-Options"] = "DENY";
                    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
                    context.Response.Headers["Referrer-Policy"] = "no-referrer";
                    context.Response.Headers["Content-Security-Policy"] = "frame-ancestors 'self';";
                    return Task.CompletedTask;
                });
                await next();
            });
            // CORS debe ir ANTES de Auth
            app.UseCors("PolicyCors");

            // Autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Swagger UI (ahora pasa por el middleware de seguridad)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("./v1/swagger.json", "ApiTaller v1");
            });
           
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
