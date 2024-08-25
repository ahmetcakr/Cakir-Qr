using MassTransit;
using QrMenu.Application.Features.MenuQrCodes.Consumers;
using QrMenu.WebAPI.Dtos;

namespace QrMenu.WebAPI.Extensions;

public static class MassTransitRegister
{

    public static IServiceCollection RegisterMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(options =>
        {
            options.AddConsumer<CreatedMenuQrCodeResponseConsumer>();

            //Default port: 5672
            options.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = configuration.GetSection("RabbitMqOptions").Get<RabbitMqOptionsDto>();
                Uri.TryCreate(rabbitMqOptions?.Host ?? string.Empty, UriKind.Absolute, out var rabbitMqUri);

                if (rabbitMqUri == null) return;

                cfg.Host(rabbitMqUri, "/", host =>
                {
                    host.Username(rabbitMqOptions?.Username ?? string.Empty);
                    host.Password(rabbitMqOptions?.Password ?? string.Empty);
                });

                cfg.ReceiveEndpoint("created-menu-qr-code", e =>
                {
                    e.ConfigureConsumer<CreatedMenuQrCodeResponseConsumer>(context);
                });
            });
        });

        return services;
    }
}
