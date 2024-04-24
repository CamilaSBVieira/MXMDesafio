using Newtonsoft.Json.Linq;

namespace MXMDesafio.WorkerService.FuncAuxiliares
{
    public static class Desestruturador
    {
        public static List<T> ParaListaObjeto<T>(string json)
        {
            var resObj = JObject.Parse(json);
            var valueArray = resObj["value"];
            var obj = valueArray.ToObject<List<T>>();
            return obj;
        }
    }
}
