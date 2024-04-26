using MXMDesafio.WorkerService.Domain.Models;

namespace MXMDesafio.WorkerService.Application.Interfaces
{
    public interface IRequisicaoCotacaoPeriodoService : IRequisicaoService<Cotacao>
    {
        Task<List<Cotacao>> Solicitar(DateTime dataInicial, DateTime dataFinal);
    }
}
