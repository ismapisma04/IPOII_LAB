using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class paginaConfiguracion : Page
    {
        public paginaConfiguracion()
        {
            this.InitializeComponent();
        }

        private void Cambiar_Idioma_Loaded(object sender, RoutedEventArgs e)
        {
            // Recuperamos el idioma activo actualmente; si está vacío, asumimos que es el español por defecto
            string idiomaActual = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;
            if (string.IsNullOrEmpty(idiomaActual))
            {
                idiomaActual = "es";
            }

            // Marcamos el elemento correcto en el ComboBox
            foreach (ComboBoxItem item in Cambiar_Idioma.Items)
            {
                if (item.Tag?.ToString() == idiomaActual)
                {
                    Cambiar_Idioma.SelectedItem = item;
                    break;
                }
            }
        }

        private void Cambiar_Idioma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cambiar_Idioma.SelectedItem is ComboBoxItem selectedItem)
            {
                string idIdioma = selectedItem.Tag?.ToString();
                string idiomaActual = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;

                // Solo recargamos todo si el idioma seleccionado es diferente al que ya está activo
                if (!string.IsNullOrEmpty(idIdioma) && idIdioma != idiomaActual)
                {
                    // Cambiar el idioma principal de forma global para toda la aplicación UWP
                    Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = idIdioma;

                    // Limpiar la caché de recursos para que el sistema empiece a usar el nuevo idioma
                    Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
                    Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();

                    // Forzar que el Grid de MainPage recargue navegando nuevamente a ella para repintarla entera 
                    // y devolviéndonos a la paginaConfiguración para no perder el contexto donde estábamos
                    if (Window.Current.Content is Frame rootFrame)
                    {
                        rootFrame.Navigate(typeof(MainPage));

                        // Una vez recargada la MainPage, le decimos que directamente muestre la pagina de configuración 
                        // utilizando su propiedad pública fmMain
                                                                    if (rootFrame.Content is MainPage mainPage)
                                                                    {
                                                                        mainPage.MostrarConfiguracion();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                    private void Gestion_Notificaciones_Loaded(object sender, RoutedEventArgs e)
                                                    {
                                                        // Lógica al cargar las notificaciones
                                                    }

                                                    private void Gestion_Notificaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
                                                    {
                                                        // Lógica al cambiar la selección de notificaciones
                                                    }
                                                }
                                            }