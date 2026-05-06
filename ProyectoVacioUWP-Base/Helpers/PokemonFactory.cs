using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProyectoVacioUWP_Base
{
    public static class PokemonFactory
    {
        public static UserControl CrearControlPokemon(iPokemon pokemon)
        {
            if (pokemon is EmpoleonARS)
            {
                return ClonarPokemon(new EmpoleonARS(), pokemon);
            }
            //NOTE: Poner aqui los demas pokemons, siguiendo el mismo formato que el de arriba

            return null;
        }

        private static UserControl ClonarPokemon(UserControl vistaBase, iPokemon origen)
        {
            if (vistaBase is iPokemon destino)
            {
                destino.Nombre = origen.Nombre;
                destino.Vida = origen.Vida;
                destino.Energia = origen.Energia;
                destino.Categoría = origen.Categoría;
                destino.Tipo = origen.Tipo;
                destino.Altura = origen.Altura;
                destino.Peso = origen.Peso;
                destino.Evolucion = origen.Evolucion;
                destino.Descripcion = origen.Descripcion;
            }

            return vistaBase;
        }

        public static void PrepararParaPokedex(iPokemon pokemonVisual, UserControl control)
        {
            pokemonVisual.verFondo(false);
            pokemonVisual.verFilaVida(false);
            pokemonVisual.verFilaEnergia(false);
            pokemonVisual.verPocionVida(false);
            pokemonVisual.verPocionEnergia(false);
            pokemonVisual.verNombre(false);
            pokemonVisual.verEscudo(false);
            pokemonVisual.activarAniIdle(true);

            control.Width = 260;
            control.Height = 180;
            control.HorizontalAlignment = HorizontalAlignment.Left;
            control.VerticalAlignment = VerticalAlignment.Center;
            control.IsHitTestVisible = false;
        }
    }
}