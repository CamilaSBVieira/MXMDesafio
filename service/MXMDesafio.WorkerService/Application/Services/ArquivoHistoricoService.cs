using MXMDesafio.WorkerService.Application.Interfaces;
using System.Globalization;

namespace MXMDesafio.WorkerService.Application.Services
{
    public class ArquivoHistoricoService : IArquivoHistoricoService
    {
        private string caminhoPasta;
        private string nomeArquivo;
        private string caminhoArquivo;
        public ArquivoHistoricoService()
        {
            caminhoPasta = AppDomain.CurrentDomain.BaseDirectory + @"/Logs";
            nomeArquivo = "HistoricoCotacoes_.txt";
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

        public DateTime DataUltimaAtualizacao()
        {
            if (ArquivoJaExiste())
            {
                var ultimaLinha = File.ReadLines(caminhoArquivo).Last();
                string formato = "dd/MM/yyyy HH:mm:ss";
                string datePart = ultimaLinha.Substring(ultimaLinha.IndexOf(":") + 2);

                DateTime ultimaAtt = DateTime.ParseExact(datePart, formato, CultureInfo.InvariantCulture);
                return ultimaAtt.AddDays(1);
            }
            else
                return DateTime.Now;
        }
    }
}
