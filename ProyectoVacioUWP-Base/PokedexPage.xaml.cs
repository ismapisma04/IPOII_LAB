using System;
using Windows.UI.Xaml.Controls;

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
                if (Activator.CreateInstance(tipo) is UserControl control)
                {
                    if (control is iPokemon pokemon)
                    {
                        pokemon.verFondo(false);
                        pokemon.verFilaVida(false);
                        pokemon.verFilaEnergia(false);
                        pokemon.verPocionVida(false);
                        pokemon.verPocionEnergia(false);
                        pokemon.verNombre(false);
                        pokemon.verEscudo(false);
                        pokemon.activarAniIdle(true);
                    }

                    control.Width = 280;
                    control.Height = 220;
                    spPokemons.Children.Add(control);
                }
            }
        }
    }
}