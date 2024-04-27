using MXMDesafio.WorkerService.Application.Interfaces;

namespace MXMDesafio.WorkerService
{
    public class HistoricoWorker : BackgroundService
    {
        private readonly IRequisicaoCotacaoPeriodoService _requisicaoService;
        private readonly IArquivoHistoricoService _arquivoService;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<Worker> _logger;
        public HistoricoWorker(
            IRequisicaoCotacaoPeriodoService requisicaoService,
            IArquivoHistoricoService arquivoService,
            IHostApplicationLifetime applicationLifetime,
            ILogger<Worker> logger
        )
        {
            _requisicaoService = requisicaoService;
            _arquivoService = arquivoService;
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                    try
                    {
                        var cotacoes = await _requisicaoService
                            .Solicitar(_arquivoService.DataUltimaAtualizacao(), DateTime.Now);
                        if(cotacoes != null)
                        {
                            foreach(var c in cotacoes)
                            {
                                _arquivoService.SalvarEmArquivo(c.ToString());
                            }
                        }
                        else
                            await Task.CompletedTask;
                    }
                    catch (HttpRequestException ex)
                    {
                        _logger.LogInformation($"Erro na requisição: {ex.Message}");
                    }
                    await Task.Delay(5000);
            }
        }

    }
}
