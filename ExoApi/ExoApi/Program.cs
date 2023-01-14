using ExoApi.Contexts;
using ExoApi.Interface;
using ExoApi.Repositorys;
using ExoApi.ViewModel;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ChapterContext, ChapterContext>(); // config serviço contexto
builder.Services.AddTransient<IProjetos, ProjetoRepository>(); //  config serviço repository
builder.Services.AddTransient<IUsuario, UsusarioRepository>();
//builder.Services.AddTransient<IUsuario, LoginViewModel>();
//builder.Services.AddTransient<IProjetos, IProjetos>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:7205")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

});
// serviço de jwt bearer
builder.Services.AddAuthentication(opitons =>
{
    opitons.DefaultChallengeScheme = "JwtBearer";
    opitons.DefaultAuthenticateScheme = "JwtBeare";
})

.AddJwtBearer("JwtBearer", options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         //valida quem está solicitanod
         ValidateIssuer = true,
         //valida qeum esta recebendo
         ValidateAudience = true,
         // valida o tempo
         ValidateLifetime = true,
         // fomra de criptografia e valida chave de autentuficação
         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chave-autentificacao")),
         ClockSkew = TimeSpan.FromMinutes(15),
         // NOME O INSSURE DE ORIGEM    
         ValidIssuer = "EXOAPI",
         //NOME DO AUDICEN PARA ONDE INDO
         ValidAudience = "EXOAPI"


     };
 });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
