using MXMDesafio.WorkerService.Application.Interfaces;
using MXMDesafio.WorkerService.FuncAuxiliares;

namespace MXMDesafio.WorkerService
{
    public class HistoricoWorker : BackgroundService
    {
        private readonly IRequisicaoCotacaoPeriodoService _requisicaoService;
        private readonly IArquivoHistoricoService _arquivoService;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private int Delay;

        public HistoricoWorker(
            IRequisicaoCotacaoPeriodoService requisicaoService,
            IArquivoHistoricoService arquivoService,
            IHostApplicationLifetime applicationLifetime
        )
        {
            _requisicaoService = requisicaoService;
            _arquivoService = arquivoService;
            _applicationLifetime = applicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await AtualizarArquivo();
            while (!stoppingToken.IsCancellationRequested)
            {
                Delay = Data.DeAgoraAteCinco();
                if (Data.DepoisDasCinco())
                {
                    try
                    {
                        var cotacao = await _requisicaoService.Solicitar();
                        if(cotacao != null)
                            _arquivoService.SalvarEmArquivo(cotacao.FirstOrDefault()!.ToString());
                        else
                            await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        _arquivoService.SalvarEmArquivo($"Erro na requisição: {ex.Message}");
                        _applicationLifetime.StopApplication();
                    }
                }
                await Task.Delay(Delay);
            }
        }

        protected async Task AtualizarArquivo()
        {
            if (_arquivoService.ArquivoJaExiste())
            {
                var ultimaAtualizacao = _arquivoService.UltimaAtualizacao();
                var historico = await _requisicaoService.Solicitar(ultimaAtualizacao.Date.AddDays(1), DateTime.Now.Date.AddDays(-1));
                if (historico.Count > 0)
                {
                    foreach (var h in historico)
                        _arquivoService.SalvarEmArquivo(h.ToString());
                }
            }
            else
                await Task.CompletedTask;
        }
    }
}
