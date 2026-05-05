using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent(); 
            // Navigate to Home initially
            ContentFrame.Navigate(typeof(HomePage));
            NavView.SelectedItem = NavView.MenuItems[0];

            // Set up back button for older Windows 10 versions
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                e.Handled = true;
                ContentFrame.GoBack();
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                // We're not using standard settings, but we could handle it here
            }
            else
            {
                string tag = args.InvokedItemContainer.Tag?.ToString();
                switch (tag)
                {
                    case "Home":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case "MisPokemon":
                        ContentFrame.Navigate(typeof(MisPokemonPage));
                        break;
                    case "Pokedex":
                        ContentFrame.Navigate(typeof(PokedexPage));
                        break;
                    case "Combate":
                        ContentFrame.Navigate(typeof(CombatePage));
                        break;
                    case "AcercaDe":
                        ContentFrame.Navigate(typeof(AcercaDePage));
                        break;
                }
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ContentFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
