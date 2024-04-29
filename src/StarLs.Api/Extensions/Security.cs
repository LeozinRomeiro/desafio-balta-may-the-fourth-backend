namespace StarLs.Api.Extensions;

public static class Security
{
    public static void AddPolicyPermission(this WebApplicationBuilder builder, string corsName)
    {
        builder.Services.AddCors(x =>
        {
            x.AddPolicy(corsName,policy =>
                                    policy
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowAnyOrigin()
                       );
        });
    }
}
