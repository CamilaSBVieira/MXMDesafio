using Humanizer;

namespace MXMDesafio.WorkerService.FuncAuxiliares
{
    public static class Data
    {
        public static bool EFinalDeSemana()
        {
            if ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday) || (DateTime.Now.DayOfWeek == DayOfWeek.Sunday))
            {
                return true;
            }
            return false;
        }

        public static bool AntesDasDez()
        {
            DateTime agora = DateTime.Now;
            if (agora.Hour < 10) return true;
            return false;
        }

        public static bool DepoisDasCinco()
        {
            DateTime agora = DateTime.Now;
            if (agora.Hour >= 17) return true;
            return false;
        }

        public static bool MercadoFechado()
        {
            if (EFinalDeSemana()) return true;
            if (AntesDasDez()) return true;
            if (DepoisDasCinco()) return true;
            return false;
        }

        public static int QuandoMercadoAbre()
        {
            DateTime agora = DateTime.Now;

            if (EFinalDeSemana())
            {
                int diasAteSegunda = ((int)DayOfWeek.Monday - (int)agora.DayOfWeek + 7) % 7;

                DateTime proximaSegunda = agora.AddDays(diasAteSegunda).Date;

                TimeSpan tempoAteProximaSEgunda = proximaSegunda - agora;

                return (int)tempoAteProximaSEgunda.TotalMilliseconds;
            }
            if (AntesDasDez())
            {
                DateTime asDez = new DateTime(agora.Year, agora.Month, agora.Day, 10, 0, 0);
                TimeSpan tempoAteAsDez = asDez - agora;
                return (int)tempoAteAsDez.TotalMilliseconds;
            }
            if (DepoisDasCinco())
            {
                DateTime asDezAmanha = new DateTime(agora.Year, agora.Month, agora.Day, 10, 0, 0).AddDays(1);
                TimeSpan tempoAteAsDez = asDezAmanha - agora;

                return (int)tempoAteAsDez.TotalMilliseconds;
            }
            return 0;
        }

        public static string QuandoMercadoAbreAmigavel()
        {
            var milisegundos = QuandoMercadoAbre();
            return TimeSpan.FromMilliseconds(milisegundos).Humanize(3);
        }
        public static int DeAgoraAteCinco()
        {
            DateTime agora = DateTime.Now;
            DateTime asCincoAmanha = new DateTime(agora.Year, agora.Month, agora.Day, 17, 0, 0).AddDays(1);
            TimeSpan tempoAteAsCinco = asCincoAmanha - agora;

            return (int)tempoAteAsCinco.TotalMilliseconds;
        }
    }
}
