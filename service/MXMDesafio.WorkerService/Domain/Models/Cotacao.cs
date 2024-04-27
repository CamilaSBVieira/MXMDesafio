using MXMDesafio.WorkerService.FuncAuxiliares;
using Newtonsoft.Json;
using System.Text;

namespace MXMDesafio.WorkerService.Domain.Models
{
    public class Cotacao
    {
        [JsonProperty("cotacaoCompra")]
        public decimal Compra { get; set; }

        [JsonProperty("cotacaoVenda")]
        public decimal Venda { get; set; }

        [JsonProperty("dataHoraCotacao")]
        public DateTime Data { get; set; }

        public override string ToString()
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine($"Cotação:");
            texto.AppendLine(Texto.Identar($"Compra: R$ {Compra}", 4));
            texto.AppendLine(Texto.Identar($"Venda: R$ {Venda}", 4));
            texto.Append(Texto.Identar($"Data e Hora da Cotação: {Data}", 4));
            return texto.ToString();
        }
    }
}