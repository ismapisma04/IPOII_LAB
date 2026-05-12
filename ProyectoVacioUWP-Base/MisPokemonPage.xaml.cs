using Windows.UI.Xaml.Controls;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class MisPokemonPage : Page
    {
        public MisPokemonPage()
        {
            this.InitializeComponent();

            // quitar la pocima de vida y energia
            

            snorlax.verPocionVida(false);
            snorlax.verPocionEnergia(false);
            gengar.verPocionVida(false);
            gengar.verPocionEnergia(false);
            
        }
       
    }
}