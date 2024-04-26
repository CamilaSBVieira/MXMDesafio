using MXMDesafio.WorkerService.Application.Interfaces;
using MXMDesafio.WorkerService.Domain.Models;
using MXMDesafio.WorkerService.FuncAuxiliares;

namespace MXMDesafio.WorkerService.Infra.Services
{
    public class RequisicaoCotacaoPeriodoService : IRequisicaoCotacaoPeriodoService
    {
        private static readonly HttpClient _http = new HttpClient();

        public async Task<List<Cotacao>> Solicitar()
        {
            string dataAtual = DateTime.Today.ToString("MM-dd-yyyy");
            string fullUrl = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarDia(dataCotacao=@dataCotacao)?@dataCotacao='{dataAtual}'&$top=100&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

            try
            {
                using (HttpResponseMessage res = await _http.GetAsync(fullUrl))
                {
                    res.EnsureSuccessStatusCode();
                    var resString = await res.Content.ReadAsStringAsync();
                    var cotacoes = Desestruturador.ParaListaObjeto<Cotacao>(resString);
                    return cotacoes;
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        public async Task<List<Cotacao>> Solicitar(DateTime dataInicial, DateTime dataFinal)
        {
            string dataI = dataInicial.Date.ToString("MM-dd-yyyy");
            string dataF = dataFinal.Date.ToString("MM-dd-yyyy");

            string fullUrl = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarPeriodo(dataInicial=@dataInicial,dataFinalCotacao=@dataFinalCotacao)?@dataInicial='{dataI}'&@dataFinalCotacao='{dataF}'&$top=100&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

            try
            {
                using (HttpResponseMessage res = await _http.GetAsync(fullUrl))
                {
                    res.EnsureSuccessStatusCode();
                    var resString = await res.Content.ReadAsStringAsync();
                    var cotacoes = Desestruturador.ParaListaObjeto<Cotacao>(resString);
                    return cotacoes;
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}