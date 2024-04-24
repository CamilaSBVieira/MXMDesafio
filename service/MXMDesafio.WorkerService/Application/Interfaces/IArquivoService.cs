namespace MXMDesafio.WorkerService.Application.Interfaces
{
    public interface IArquivoService
    {
        bool ArquivoJaExiste();
        bool ArquivoVazio();
        void SalvarEmArquivo(string msg);
    }
}
