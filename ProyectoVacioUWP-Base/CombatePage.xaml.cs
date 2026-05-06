using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class CombatePage : Page
    {
        private List<iPokemon> listaPokemons = new List<iPokemon>();

        private iPokemon pokemonSeleccionadoP1;
        private iPokemon pokemonSeleccionadoP2;

        private int turnoSeleccion = 1;

        private DispatcherTimer temporizadorInicioCombate;

        public CombatePage()
        {
            this.InitializeComponent();

            CargarPokemonsEnGrid();
            ActualizarIndicadoresTurno();
            PrepararTemporizador();
        }

        private void CargarPokemonsEnGrid()
        {
            listaPokemons = PokemonCatalogo.CrearPokemons();
            gvPokemons.Items.Clear();

            foreach (iPokemon pokemon in listaPokemons)
            {
                UserControl control = PokemonFactory.CrearControlPokemon(pokemon);

                if (control is iPokemon pokemonVisual)
                {
                    PokemonFactory.PrepararParaSeleccion(pokemonVisual, control);
                    gvPokemons.Items.Add(control);
                }
            }
        }

        private void ActualizarIndicadoresTurno()
        {
            txtTurnoP1.Foreground = new SolidColorBrush(turnoSeleccion == 1 ? Colors.DodgerBlue : Colors.Black);
            txtTurnoP2.Foreground = new SolidColorBrush(turnoSeleccion == 2 ? Colors.Red : Colors.Black);

            scaleP1.ScaleX = turnoSeleccion == 1 ? 1.28 : 1.0;
            scaleP1.ScaleY = turnoSeleccion == 1 ? 1.28 : 1.0;

            scaleP2.ScaleX = turnoSeleccion == 2 ? 1.28 : 1.0;
            scaleP2.ScaleY = turnoSeleccion == 2 ? 1.28 : 1.0;
        }
        private void gvPokemons_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(e.ClickedItem is UserControl controlPulsado))
            {
                return;
            }

            iPokemon pokemonBase = ObtenerPokemonDesdeControl(controlPulsado);

            if (pokemonBase == null)
            {
                return;
            }

            if (turnoSeleccion == 1)
            {
                pokemonSeleccionadoP1 = pokemonBase;
                MostrarPokemonEnCaja(contenedorP1, pokemonBase, true);
                turnoSeleccion = 2;
                ActualizarIndicadoresTurno();
            }
            else if (turnoSeleccion == 2)
            {
                pokemonSeleccionadoP2 = pokemonBase;
                MostrarPokemonEnCaja(contenedorP2, pokemonBase, false);
                turnoSeleccion = 0;
                ActualizarIndicadoresTurno();
                ActivarBotonVersus();
            }
        }

        private iPokemon ObtenerPokemonDesdeControl(UserControl control)
        {
            foreach (iPokemon pokemon in listaPokemons)
            {
                if (control is iPokemon pokemonVisual &&
                    pokemonVisual.Nombre == pokemon.Nombre &&
                    pokemonVisual.Tipo == pokemon.Tipo)
                {
                    return pokemon;
                }
            }

            return null;
        }

        private void MostrarPokemonEnCaja(Grid contenedor, iPokemon pokemonOriginal, bool esPlayer1)
        {
            contenedor.Children.Clear();

            UserControl control = PokemonFactory.CrearControlPokemon(pokemonOriginal);

            if (control is iPokemon pokemonVisual)
            {
                PokemonFactory.PrepararParaSeleccion(pokemonVisual, control);

                control.Width = 230;
                control.Height = 180;

                Viewbox vb = new Viewbox
                {
                    Stretch = Stretch.Uniform,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Child = control
                };

                contenedor.Children.Add(vb);
            }
        }


        private void ActivarBotonVersus()
        {
            btnVersus.IsEnabled = true;
            MostrarVsStoryboard.Begin();
            RespiracionVsStoryboard.Begin();
        }
        private void PrepararTemporizador()
        {
            temporizadorInicioCombate = new DispatcherTimer();
            temporizadorInicioCombate.Interval = TimeSpan.FromSeconds(2);
            temporizadorInicioCombate.Tick += TemporizadorInicioCombate_Tick;
        }

        private void btnVersus_Click(object sender, RoutedEventArgs e)
        {
            if (pokemonSeleccionadoP1 == null || pokemonSeleccionadoP2 == null)
            {
                return;
            }

            btnVersus.IsEnabled = false;
            gvPokemons.IsEnabled = false;

            temporizadorInicioCombate.Start();
        }

        private void TemporizadorInicioCombate_Tick(object sender, object e)
        {
            temporizadorInicioCombate.Stop();
            FadeNegroStoryboard.Begin();
        }
    }
}