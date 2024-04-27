namespace MXMDesafio.WorkerService.Application.Interfaces
{
    public interface IArquivoHistoricoService : IArquivoService
    {
        DateTime DataUltimaAtualizacao();
    }
}
