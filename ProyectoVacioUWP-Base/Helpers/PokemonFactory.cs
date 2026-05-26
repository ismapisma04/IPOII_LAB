using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ProyectoVacioUWP_Base
{
    public static class PokemonFactory
    {
        public static UserControl CrearControlPokemon(iPokemon pokemon)
        {
            if (pokemon == null)
                return null;

            Type tipoControl = pokemon.GetType();

            if (tipoControl == null)
                return null;

            UserControl control = Activator.CreateInstance(tipoControl) as UserControl;

            if (control == null)
                return null;

            control = ClonarPokemon(control, pokemon);

            string nombreTipo = tipoControl.Name;

            string carpetaAssets;
            if (nombreTipo == "RotomPVA")
            {
                carpetaAssets = $"Pokemons/{nombreTipo}/AssetsPVA";
            }
            else if (nombreTipo == "ComputerrorDLM")
            {
                carpetaAssets = $"AssetsComputerrorDLM";
            }
            else
            {
                carpetaAssets = $"Pokemons/{nombreTipo}/Assets{nombreTipo}";
            }

            CorregirRutasImagenes(control, carpetaAssets);

            return control;
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

        private static void CorregirRutasImagenes(DependencyObject raiz, string carpetaAssets)
        {
            foreach (Image imagen in BuscarElementosVisuales<Image>(raiz))
            {
                if (imagen.Source is BitmapImage bitmap && bitmap.UriSource != null)
                {
                    string rutaOriginal = bitmap.UriSource.ToString();
                    string nombreArchivo = ObtenerNombreArchivo(rutaOriginal);

                    if (!string.IsNullOrWhiteSpace(nombreArchivo))
                    {
                        imagen.Source = new BitmapImage(
                            new Uri($"ms-appx:///{carpetaAssets}/{nombreArchivo}")
                        );
                    }
                }
            }
        }

        private static string ObtenerNombreArchivo(string rutaOriginal)
        {
            if (string.IsNullOrWhiteSpace(rutaOriginal))
                return null;

            string rutaNormalizada = rutaOriginal.Replace("\\", "/");

            int indiceQuery = rutaNormalizada.IndexOf("?");
            if (indiceQuery >= 0)
            {
                rutaNormalizada = rutaNormalizada.Substring(0, indiceQuery);
            }

            string[] partes = rutaNormalizada.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length == 0)
                return null;

            return partes[partes.Length - 1];
        }

        private static IEnumerable<T> BuscarElementosVisuales<T>(DependencyObject raiz)
            where T : DependencyObject
        {
            if (raiz == null)
                yield break;

            int totalHijos = Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(raiz);

            for (int i = 0; i < totalHijos; i++)
            {
                DependencyObject hijo = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(raiz, i);

                if (hijo is T elemento)
                    yield return elemento;

                foreach (T descendiente in BuscarElementosVisuales<T>(hijo))
                    yield return descendiente;
            }
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

            control.Width = double.NaN;
            control.Height = double.NaN;
            control.HorizontalAlignment = HorizontalAlignment.Center;
            control.VerticalAlignment = VerticalAlignment.Center;
            control.IsHitTestVisible = false;

            control.Loaded += (s, e) =>
            {
                pokemonVisual.activarAniIdle(true);
            };
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

            control.Width = 150;
            control.Height = 150;
            control.HorizontalAlignment = HorizontalAlignment.Center;
            control.VerticalAlignment = VerticalAlignment.Center;
            control.IsHitTestVisible = false;

            control.Loaded += (s, e) =>
            {
                pokemonVisual.activarAniIdle(true);
            };
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