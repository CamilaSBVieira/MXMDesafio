namespace MXMDesafio.WorkerService.FuncAuxiliares
{
    public static class Texto
    {
        public static string Identar(string txt, int numDeEspacos)
        {
            return new string(' ', numDeEspacos) + txt;
        }
    }
}
