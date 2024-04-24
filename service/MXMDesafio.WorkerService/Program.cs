using CliWrap;
using MXMDesafio.WorkerService;
using MXMDesafio.WorkerService.Application.Interfaces;
using MXMDesafio.WorkerService.Application.Services;

const string NomeServico = "Cotacao Service";

if (args is { Length: 1 })
{
    try
    {
        string executablePath =
            Path.Combine(AppContext.BaseDirectory, "CotacaoService.exe");

        if (args[0] is "/Install")
        {
            await Cli.Wrap("sc")
                .WithArguments(new[] { "create", NomeServico, $"binPath={executablePath}", "start=delayed-auto", @"obj=NT AUTHORITY\LocalService" })
                .ExecuteAsync();
        }
        else if (args[0] is "/Uninstall")
        {
            await Cli.Wrap("sc")
                .WithArguments(new[] { "stop", NomeServico })
                .ExecuteAsync();

            await Cli.Wrap("sc")
                .WithArguments(new[] { "delete", NomeServico })
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


builder.Services.AddHostedService<Worker>()
    .AddHostedService<CotacaoWorker>()
    .AddSingleton<IRequisicaoCotacaoService, RequisicaoCotacaoService>()
    .AddSingleton<IInformacaoMercadoService, InformacaoMercadoService>()
    .AddSingleton<IArquivoService, ArquivoService>();


var host = builder.Build();
host.Run();
