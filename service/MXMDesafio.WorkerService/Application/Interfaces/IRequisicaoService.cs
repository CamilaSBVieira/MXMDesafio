namespace MXMDesafio.WorkerService.Application.Interfaces
{
    public interface IRequisicaoService<T>
    {
        Task<List<T>> Solicitar();
    }
}
