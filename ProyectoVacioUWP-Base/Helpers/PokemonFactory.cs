using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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

            // NOTE: Añade aquí el resto de Pokémon siguiendo este formato.
            // if (pokemon is InfernapeXXX)
            // {
            //     return ClonarPokemon(new InfernapeXXX(), pokemon);
            // }

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

        public static void PrepararParaSeleccion(iPokemon pokemonVisual, UserControl control)
        {
            pokemonVisual.verFondo(false);
            pokemonVisual.verFilaVida(false);
            pokemonVisual.verFilaEnergia(false);
            pokemonVisual.verPocionVida(false);
            pokemonVisual.verPocionEnergia(false);
            pokemonVisual.verNombre(false);
            pokemonVisual.verEscudo(false);
            pokemonVisual.activarAniIdle(true);

            control.Width = 150;
            control.Height = 150;
            control.HorizontalAlignment = HorizontalAlignment.Center;
            control.VerticalAlignment = VerticalAlignment.Center;
            control.IsHitTestVisible = false;
        }

        public static Viewbox CrearVistaCompactaSeleccion(iPokemon pokemonOriginal)
        {
            UserControl control = CrearControlPokemon(pokemonOriginal);

            if (control == null)
                return null;

            if (control is iPokemon pokemonVisual)
            {
                PrepararParaSeleccion(pokemonVisual, control);
            }

            return new Viewbox
            {
                Stretch = Stretch.UniformToFill,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Child = new Border
                {
                    Width = 140,
                    Height = 110,
                    Background = new SolidColorBrush(Colors.Transparent),
                    Child = control
                }
            };
        }
    }
}