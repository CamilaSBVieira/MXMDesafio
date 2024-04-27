using MXMDesafio.WorkerService.Application.Interfaces;
using MXMDesafio.WorkerService.FuncAuxiliares;

namespace MXMDesafio.WorkerService
{
    public class CotacaoWorker : BackgroundService
    {
        private readonly IRequisicaoCotacaoService _requisicaoService;
        private readonly IArquivoLogService _arquivoService;
        private readonly IInformacaoMercadoService _informacaoMercadoService;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<Worker> _logger;
        private int _ultimaCotacao = 0;
        private int Delay;

        public CotacaoWorker(
            IRequisicaoCotacaoService requisicaoService,
            IArquivoLogService arquivoService,
            IInformacaoMercadoService informacaoMercadoService,
            IHostApplicationLifetime applicationLifetime,
            ILogger<Worker> logger
        )
        {
            _requisicaoService = requisicaoService;
            _arquivoService = arquivoService;
            _informacaoMercadoService = informacaoMercadoService;
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (Data.MercadoFechado())
                {
                    Delay = Data.QuandoMercadoAbre();
                    var texto = _informacaoMercadoService.InformarMercadoFechado();
                    _arquivoService.SalvarEmArquivo(texto);
                }
                else
                {
                    Delay = 1800000;
                    try
                    {
                        var boletim = await _requisicaoService.Solicitar();
                        if (_ultimaCotacao == 0)
                        {
                            if (boletim.Count == 0)
                                _arquivoService.SalvarEmArquivo($"Valores ainda não foram atualizados pelo Banco Central! ({DateTime.Now})");
                            else
                            {
                                _arquivoService.SalvarEmArquivo($"Cotação de compra | Cotação de venda | Data e Hora da Cotação | Data e Hora de Atualização | Tipo do Boletim");
                                _ultimaCotacao = boletim.Count;
                                foreach (var c in boletim)
                                    _arquivoService.SalvarEmArquivo(c.ToString());
                            }
                        }
                        if (boletim.Count != _ultimaCotacao)
                        {

                            for (int i = _ultimaCotacao; i < boletim.Count; i++)
                                _arquivoService.SalvarEmArquivo(boletim[i].ToString());

                            _ultimaCotacao = boletim.Count;
                        }
                        else
                        {
                            await Task.CompletedTask;
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        _logger.LogInformation($"Erro na requisição: {ex.Message}");
                    }
                }
                await Task.Delay(Delay);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _arquivoService.SalvarEmArquivo($"\nServiço foi parado! {DateTime.Now}_________________\n");
            return Task.CompletedTask;
        }
    }
}
