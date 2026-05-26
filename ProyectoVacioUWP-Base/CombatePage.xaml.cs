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
            public bool EsContraPC { get; set; }
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
                NombrePokemonP2 = pokemonSeleccionadoP2.Nombre,
                EsContraPC = swContraPC.IsOn
            };

            FadeNegroStoryboard.Begin();
        }

        private void gvPokemons_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            if (e.Items.Count > 0)
            {
                PokemonSeleccionItem item = e.Items[0] as PokemonSeleccionItem;

                if (item != null)
                {
                    e.Data.SetText(item.PokemonReal.Nombre);
                }
            }
        }

        private void Contenedor_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        }

        private async void ContenedorP1_Drop(object sender, DragEventArgs e)
        {
            string nombrePokemon = await e.DataView.GetTextAsync();

            iPokemon pokemon = BuscarPokemonPorNombre(nombrePokemon);

            if (pokemon != null)
            {
                pokemonSeleccionadoP1 = pokemon;

                MostrarPokemonEnCaja(contenedorP1, pokemon, true);

                turnoSeleccion = 2;
                ActualizarIndicadoresTurno();
            }
        }

        private async void ContenedorP2_Drop(object sender, DragEventArgs e)
        {
            string nombrePokemon = await e.DataView.GetTextAsync();

            iPokemon pokemon = BuscarPokemonPorNombre(nombrePokemon);

            if (pokemon != null)
            {
                pokemonSeleccionadoP2 = pokemon;

                MostrarPokemonEnCaja(contenedorP2, pokemon, false);

                turnoSeleccion = 0;
                ActualizarIndicadoresTurno();

                ActivarBotonVersus();
            }
        }

        private iPokemon BuscarPokemonPorNombre(string nombre)
        {
            foreach (iPokemon pokemon in listaPokemons)
            {
                if (pokemon.Nombre == nombre)
                {
                    return pokemon;
                }
            }

            return null;
        }

    }
}
