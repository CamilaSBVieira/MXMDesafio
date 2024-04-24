﻿using CliWrap;
using MXMDesafio.WorkerService;
using MXMDesafio.WorkerService.Application.Interfaces;
using MXMDesafio.WorkerService.Application.Services;

const string ServiceName = "Cotação Service";

if (args is { Length: 1 })
{
    try
    {
        string executablePath =
            Path.Combine(AppContext.BaseDirectory, "MXMDesafio.WorkerService.exe");

        if (args[0] is "/Install")
        {
            await Cli.Wrap("sc")
                .WithArguments(new[] { "create", ServiceName, $"binPath={executablePath}", "start=auto" })
                .ExecuteAsync();
        }
        else if (args[0] is "/Uninstall")
        {
            await Cli.Wrap("sc")
                .WithArguments(new[] { "stop", ServiceName })
                .ExecuteAsync();

            await Cli.Wrap("sc")
                .WithArguments(new[] { "delete", ServiceName })
                .ExecuteAsync();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }

    return;
}


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Cotação Service";
});

builder.Services.AddHostedService<Worker>()
    .AddHostedService<CotacaoWorker>()
    .AddSingleton<IRequisicaoCotacaoService, RequisicaoCotacaoService>()
    .AddSingleton<IInformacaoMercadoService, InformacaoMercadoService>()
    .AddSingleton<IArquivoService, ArquivoService>();


var host = builder.Build();
host.Run();
