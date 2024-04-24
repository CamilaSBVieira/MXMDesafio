using MXMDesafio.WorkerService.Application.Interfaces;

namespace MXMDesafio.WorkerService.Application.Services
{
    public class ArquivoService : IArquivoService
    {
        private string caminhoPasta;
        private string nomeArquivo;
        private string caminhoArquivo;
        public ArquivoService()
        {
            caminhoPasta = AppDomain.CurrentDomain.BaseDirectory + @"/Logs";
            nomeArquivo = "ServicoLog_.txt";
            caminhoArquivo = Path.Combine(caminhoPasta, nomeArquivo);

        }

        public bool ArquivoJaExiste()
        {
            if (!File.Exists(caminhoArquivo))
            {
                return false;
            }
            return true;
        }
        public bool ArquivoVazio()
        {
            if (new FileInfo(caminhoArquivo).Length == 0)
            {
                return true;
            }
            return false;
        }
        public void SalvarEmArquivo(string msg)
        {
            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            if (!ArquivoJaExiste())
            {
                using (StreamWriter escritor = File.CreateText(caminhoArquivo))
                {
                    escritor.WriteLine(msg);
                }
            }
            else
            {
                using (StreamWriter escritor = File.AppendText(caminhoArquivo))
                {
                    escritor.WriteLine(msg);
                }
            }
        }
    }
}
