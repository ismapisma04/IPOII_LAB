using Windows.UI.Xaml.Controls;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class MisPokemonPage : Page
    {
        public MisPokemonPage()
        {
            this.InitializeComponent();
<<<<<<< HEAD

            // En lugar de ocultar manualmente cada Pokemon, ejecutamos un evento cuando termine de cargar la página
            this.Loaded += MisPokemonPage_Loaded;
        }

        private void MisPokemonPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Ocultamos los elementos empezando a buscar desde esta página
            OcultarBarrasYElementos(this);
        }

        private void OcultarBarrasYElementos(Windows.UI.Xaml.DependencyObject controlPadre)
        {
            // Conseguimos todos los elementos "hijos" que están dentro de este control
            int numHijos = Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(controlPadre);

            for (int i = 0; i < numHijos; i++)
            {
                var hijo = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(controlPadre, i);

                // Si el control hijo que estamos comprobando implementa la interfaz iPokemon
                if (hijo is iPokemon pokemon)
                {
                    pokemon.verPocionVida(false);
                    pokemon.verPocionEnergia(false);
                    pokemon.verFilaVida(false);
                    pokemon.verFilaEnergia(false);
                }

                // Volvemos a llamar a este mismo método (recursividad) para revisar dentro de contenedores (como el Grid o los Viewbox)
                OcultarBarrasYElementos(hijo);
            }
=======
>>>>>>> 9d834adeac127a597f8b9d0a1ebf7affb9a58167
        }
    }
}