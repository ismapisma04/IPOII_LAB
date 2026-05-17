using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class MainPage : Page
    {
        private readonly SolidColorBrush fondoNormal = new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
        private readonly SolidColorBrush fondoSeleccionado = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 55, 55, 55));

        public MainPage()
        {
            this.InitializeComponent();

            fmMain.Navigate(typeof(HomePage));
            marcarSeleccionado(btnInicio);

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            actualizarBotonAtras();
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (fmMain.CanGoBack)
            {
                e.Handled = true;
                fmMain.GoBack();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (fmMain.CanGoBack)
            {
                fmMain.GoBack();
            }
        }

        private void fmMain_Navigated(object sender, NavigationEventArgs e)
        {
            actualizarBotonAtras();
        }

        private void actualizarBotonAtras()
        {
            btnBack.Visibility = fmMain.CanGoBack ? Visibility.Visible : Visibility.Collapsed;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                fmMain.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        private void limpiarSeleccion()
        {
            btnInicio.Background = fondoNormal;
            btnMisPokemon.Background = fondoNormal;
            btnPokedex.Background = fondoNormal;
            btnCombate.Background = fondoNormal;
            btnAcercaDe.Background = fondoNormal;
            btnConfiguracion.Background = fondoNormal;
        }

        private void marcarSeleccionado(Button boton)
        {
            limpiarSeleccion();
            boton.Background = fondoSeleccionado;
        }

        private void irInicio(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(HomePage));
            marcarSeleccionado(btnInicio);
        }

        private void irMisPokemon(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(MisPokemonPage));
            marcarSeleccionado(btnMisPokemon);
        }

        private void irPokedex(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(PokedexPage));
            marcarSeleccionado(btnPokedex);
        }

        private void irCombate(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(CombatePage));
            marcarSeleccionado(btnCombate);
        }

        private void irAcercaDe(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(AcercaDePage));
            marcarSeleccionado(btnAcercaDe);
        }

        private void irConfiguracion(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(paginaConfiguracion));
            marcarSeleccionado(btnConfiguracion);
        }

        public void MostrarConfiguracion()
        {
            // Navegación forzada a config
            fmMain.Navigate(typeof(paginaConfiguracion));
            marcarSeleccionado(btnConfiguracion);
        }
    }
}