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
        public class CombateParametros
        {
            public string NombrePokemonP1 { get; set; }
            public string NombrePokemonP2 { get; set; }
        }

        public class PokemonSeleccionItem
        {
            public iPokemon PokemonReal { get; set; }
            public Viewbox VistaCompacta { get; set; }
        }

        private List<iPokemon> listaPokemons = new List<iPokemon>();

        private iPokemon pokemonSeleccionadoP1;
        private iPokemon pokemonSeleccionadoP2;

        private int turnoSeleccion = 1;
        private CombateParametros parametrosPendientes;

        public CombatePage()
        {
            this.InitializeComponent();

            CargarPokemonsEnGrid();
            ActualizarIndicadoresTurno();
            FadeNegroStoryboard.Completed += FadeNegroStoryboard_Completed;
        }

        private void FadeNegroStoryboard_Completed(object sender, object e)
        {
            Frame.Navigate(typeof(ArenaCombatePage), parametrosPendientes);
        }

        private void CargarPokemonsEnGrid()
        {
            listaPokemons = PokemonCatalogo.CrearPokemons();

            var itemsSeleccion = new List<PokemonSeleccionItem>();

            foreach (iPokemon pokemon in listaPokemons)
            {
                Viewbox vistaCompacta = PokemonFactory.CrearVistaCompactaSeleccion(pokemon);

                if (vistaCompacta != null)
                {
                    itemsSeleccion.Add(new PokemonSeleccionItem
                    {
                        PokemonReal = pokemon,
                        VistaCompacta = vistaCompacta
                    });
                }
            }

            gvPokemons.ItemsSource = itemsSeleccion;
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
            if (!(e.ClickedItem is PokemonSeleccionItem itemPulsado))
            {
                return;
            }

            iPokemon pokemonBase = itemPulsado.PokemonReal;

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

        private void btnVersus_Click(object sender, RoutedEventArgs e)
        {
            if (pokemonSeleccionadoP1 == null || pokemonSeleccionadoP2 == null)
            {
                return;
            }

            parametrosPendientes = new CombateParametros
            {
                NombrePokemonP1 = pokemonSeleccionadoP1.Nombre,
                NombrePokemonP2 = pokemonSeleccionadoP2.Nombre
            };

            FadeNegroStoryboard.Begin();
        }
    }
}
