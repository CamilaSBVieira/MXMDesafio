using Newtonsoft.Json;
using System.Text;

namespace MXMDesafio.WorkerService.Domain.Models
{
    public class Boletim
    {
        [JsonProperty("cotacaoCompra")]
        public decimal Compra { get; set; }

        [JsonProperty("cotacaoVenda")]
        public decimal Venda { get; set; }

        [JsonProperty("dataHoraCotacao")]
        public DateTime Data { get; set; }

        [JsonProperty("tipoBoletim")]
        public string TipoBoletim { get; set; } = null!;

        public override string ToString()
        {
            StringBuilder texto = new StringBuilder();
            texto.Append($" {Compra}\t\t  |");
            texto.Append($" {Venda}\t     |");
            texto.Append($" {Data}    |");
            texto.Append($" {DateTime.Now}        |");
            texto.Append($" {TipoBoletim}");
            return texto.ToString();
        }

        public string ToTexto()
        {
            string texto = $"{Compra.ToString().Replace(",", ".")}, {Venda.ToString().Replace(",", ".")}, {TipoBoletim}";
            return texto;
        }
    }
}
