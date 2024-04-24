using MXMDesafio.WorkerService.FuncAuxiliares;
using System.Text;
using MXMDesafio.WorkerService.Application.Interfaces;

namespace MXMDesafio.WorkerService.Application.Services
{
    public class InformacaoMercadoService : IInformacaoMercadoService
    {
        public string InformarMercadoFechado()
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine($"Mercado fechado! ({DateTime.Now})");
            texto.AppendLine($"Mercado abre em: {Data.QuandoMercadoAbreAmigavel()}");
            return texto.ToString();
        }
    }
}
