using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModelStationAPI.Authorization;
using ModelStationAPI.Entities;
using ModelStationAPI.Interfaces;
using ModelStationAPI.Middleware;
using ModelStationAPI.Models;
using ModelStationAPI.Services;
using ModelStationAPI.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }
        public void AddAuthentication(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });
        }
        public void AddAuthorizationPolicy(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //HasAccessLevelAtLEast
                options.AddPolicy("IsUser", builder => builder.AddRequirements(new HasAccessLevelAtLeast(3)));
                options.AddPolicy("IsModerator", builder => builder.AddRequirements(new HasAccessLevelAtLeast(6)));
                options.AddPolicy("IsAdmin", builder => builder.AddRequirements(new HasAccessLevelAtLeast(10)));
            });
        }
        public void UseDatabase(IServiceCollection services)
        {
            var _conectionParams = Configuration;

            string connectionString = "Server=" + _conectionParams.GetSection("ProductionDatabase:Server").Value +
                @"\" + _conectionParams.GetSection("ProductionDatabase:Service").Value +
                "; Database=" + _conectionParams.GetSection("ProductionDatabase:Database").Value +
                "; Trusted_Connection=" + _conectionParams.GetSection("ProductionDatabase:TrustedConnection").Value +
                ";";

            services.AddDbContext<ModelStationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Authentication
            AddAuthentication(services);



            //Authorization
            AddAuthorizationPolicy(services);
            //AuthorizationHandlers
            services.AddScoped<IAuthorizationHandler, HasAccessLevelAtLeastHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementPostHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementUserHandler>();



            //Controllers
            services.AddControllers();



            //DbContext
            UseDatabase(services);



            //AutoMapper
            services.AddAutoMapper(this.GetType().Assembly);



            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<ILikedPostService, LikedPostService>();
            services.AddScoped<ILikedCommentService, LikedCommentService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IRoleService, RoleService>();

            

            //Middleware
            services.AddScoped<ErrorHandlingMiddleware>();



            //Hasher
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();



            //FluentValidation
            services.AddFluentValidation();
            //Create
            services.AddScoped<IValidator<CreatePostDTO>, CreatePostDTO_Validator>();
            services.AddScoped<IValidator<CreateUserDTO>, CreateUserDTO_Validator>();
            services.AddScoped<IValidator<CreateCommentDTO>, CreateCommentDTO_Validator>();
            services.AddScoped<IValidator<CreatePostCategoryDTO>, CreatePostCategoryDTO_Validator>();
            services.AddScoped<IValidator<CreateLikedPostDTO>, CreateLikedPostDTO_Validator>();
            services.AddScoped<IValidator<CreatePostWithPostCategoryNameDTO>, CreatePostWithCategoryNameDTO_Validator>();

            //Edit
            services.AddScoped<IValidator<EditPostDTO>, EditPostDTO_Validator>();
            services.AddScoped<IValidator<EditUserDTO>, EditUserDTO_Validator>();
            services.AddScoped<IValidator<EditLikedPostDTO>, EditLikedPostDTO_Validator>();
            services.AddScoped<IValidator<EditLikedCommentDTO>, EditLikedCommentDTO_Validator>();
            services.AddScoped<IValidator<EditCommentDTO>, EditCommentDTO_Validator>();
            services.AddScoped<IValidator<EditPostCategoryDTO>, EditPostCategoryDTO_Validator>();

            //Querry
            services.AddScoped<IValidator<PostQuery>, PostQuery_Validator>();


            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(Configuration["AllowedOrigins"]);
                });
            });



            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ModelStationAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //UseCORS
            app.UseCors("FrontEndClient");

            //UseCache
            app.UseResponseCaching();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ModelStationAPI v1"));
            }

            //Middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
