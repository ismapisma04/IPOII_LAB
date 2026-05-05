using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class PokedexPage : Page
    {
        public PokedexPage()
        {
            this.InitializeComponent();
            CargarPokemons();
        }

        private void CargarPokemons()
        {
            Type[] tiposPokemon =
            {
                typeof(EmpoleonARS)
            };

            foreach (Type tipo in tiposPokemon)
            {
                if (Activator.CreateInstance(tipo) is UserControl control && control is iPokemon pokemon)
                {
                    PrepararPokemonParaPokedex(pokemon, control);

                    Border tarjeta = CrearTarjetaPokemon(control, pokemon);
                    spPokemons.Children.Add(tarjeta);
                }
            }
        }

        private void PrepararPokemonParaPokedex(iPokemon pokemon, UserControl control)
        {
            pokemon.verFondo(false);
            pokemon.verFilaVida(false);
            pokemon.verFilaEnergia(false);
            pokemon.verPocionVida(false);
            pokemon.verPocionEnergia(false);
            pokemon.verNombre(false);
            pokemon.verEscudo(false);
            pokemon.activarAniIdle(true);

            control.Width = 260;
            control.Height = 180;
            control.HorizontalAlignment = HorizontalAlignment.Left;
            control.VerticalAlignment = VerticalAlignment.Center;
            control.IsHitTestVisible = false;
        }

        private Border CrearTarjetaPokemon(UserControl control, iPokemon pokemon)
        {
            Grid contenido = new Grid
            {
                ColumnSpacing = 20
            };

            contenido.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(280) });
            contenido.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Border zonaClickPokemon = new Border
            {
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(10),
                MinHeight = 190,
                Child = new Viewbox
                {
                    Stretch = Stretch.Uniform,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Child = control
                }
            };

            Grid.SetColumn(zonaClickPokemon, 0);

            StackPanel panelTexto = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                Spacing = 10
            };

            TextBlock titulo = new TextBlock
            {
                Text = pokemon.Nombre,
                FontSize = 28,
                FontWeight = Windows.UI.Text.FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                TextWrapping = TextWrapping.WrapWholeWords
            };

            TextBlock subtitulo = new TextBlock
            {
                Text = "Descripción",
                FontSize = 16,
                FontWeight = Windows.UI.Text.FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromArgb(220, 191, 219, 254))
            };

            TextBlock descripcion = new TextBlock
            {
                Text = pokemon.Descripcion,
                FontSize = 15,
                Foreground = new SolidColorBrush(Color.FromArgb(235, 255, 255, 255)),
                TextWrapping = TextWrapping.WrapWholeWords,
                MaxWidth = 520
            };

            panelTexto.Children.Add(titulo);
            panelTexto.Children.Add(subtitulo);
            panelTexto.Children.Add(descripcion);

            Grid.SetColumn(panelTexto, 1);

            contenido.Children.Add(zonaClickPokemon);
            contenido.Children.Add(panelTexto);

            Border tarjetaExterior = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255)),
                BorderBrush = new SolidColorBrush(Color.FromArgb(90, 255, 255, 255)),
                BorderThickness = new Thickness(1.5),
                CornerRadius = new CornerRadius(18),
                Padding = new Thickness(18),
                Margin = new Thickness(0, 0, 0, 6),
                Child = contenido
            };

            tarjetaExterior.Tapped += (s, e) => IrADetallePokemon(pokemon);
            tarjetaExterior.PointerEntered += (s, e) =>
            {
                tarjetaExterior.Background = new SolidColorBrush(Color.FromArgb(65, 255, 255, 255));
            };
            tarjetaExterior.PointerExited += (s, e) =>
            {
                tarjetaExterior.Background = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255));
            };

            return tarjetaExterior;
        }

        private void IrADetallePokemon(iPokemon pokemon)
        {
            Frame.Navigate(typeof(MisPokemonPage), pokemon);
        }
    }
}