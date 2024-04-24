using MXMDesafio.WorkerService.Domain.Models;
using MXMDesafio.WorkerService.FuncAuxiliares;
using MXMDesafio.WorkerService.Application.Interfaces;

namespace MXMDesafio.WorkerService.Application.Services
{
    public class RequisicaoCotacaoService : IRequisicaoCotacaoService
    {
        private static readonly HttpClient _http = new HttpClient();

        public async Task<List<Boletim>> Solicitar()
        {
            string dataAtual = DateTime.Today.ToString("MM-dd-yyyy");
            string fullUrl = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda='USD'&@dataCotacao='{dataAtual}'&$top=100&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao,tipoBoletim";

            try
            {
                using (HttpResponseMessage res = await _http.GetAsync(fullUrl))
                {
                    res.EnsureSuccessStatusCode();
                    var resString = await res.Content.ReadAsStringAsync();
                    var boletim = Desestruturador.ParaListaObjeto<Boletim>(resString);
                    return boletim;
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}
