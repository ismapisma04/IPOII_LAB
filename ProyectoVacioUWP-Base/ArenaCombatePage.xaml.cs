using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class ArenaCombatePage : Page
    {
        private List<iPokemon> listaPokemons = new List<iPokemon>();

        private iPokemon pokemonP1;
        private iPokemon pokemonP2;

        public ArenaCombatePage()
        {
            this.InitializeComponent();
            listaPokemons = PokemonCatalogo.CrearPokemons();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CombateParametros parametros = e.Parameter as CombateParametros;
            if (parametros == null)
            {
                return;
            }

            pokemonP1 = BuscarPokemonPorNombre(parametros.NombrePokemonP1);
            pokemonP2 = BuscarPokemonPorNombre(parametros.NombrePokemonP2);

            if (pokemonP1 != null)
            {
                txtNombreP1.Text = pokemonP1.Nombre;
                MostrarPokemonEnContenedor(contenedorP1, pokemonP1);
            }

            if (pokemonP2 != null)
            {
                txtNombreP2.Text = pokemonP2.Nombre;
                MostrarPokemonEnContenedor(contenedorP2, pokemonP2);
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

        private void MostrarPokemonEnContenedor(Grid contenedor, iPokemon pokemonOriginal)
        {
            contenedor.Children.Clear();

            UserControl control = PokemonFactory.CrearControlPokemon(pokemonOriginal);

            if (control is iPokemon pokemonVisual)
            {
                control.Width = 260;
                control.Height = 220;

                Viewbox vb = new Viewbox
                {
                    Stretch = Windows.UI.Xaml.Media.Stretch.Uniform,
                    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch,
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch,
                    Child = control
                };

                contenedor.Children.Add(vb);
            }
        }
    }

    public class CombateParametros
    {
        public string NombrePokemonP1 { get; set; }
        public string NombrePokemonP2 { get; set; }
    }
}