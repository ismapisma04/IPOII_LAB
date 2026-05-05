using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class PokemonDetallePage : Page
    {
        private iPokemon pokemonActual;

        public PokemonDetallePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is iPokemon pokemon)
            {
                pokemonActual = pokemon;
                CargarDatosPokemon(pokemonActual);
                CargarVistaPokemon(pokemonActual);
            }
        }

        private void CargarDatosPokemon(iPokemon pokemon)
        {
            txtTituloPrincipal.Text = $"Detalle de {pokemon.Nombre}";
            txtNombre.Text = pokemon.Nombre;
            txtSubtitulo.Text = $"{pokemon.Categoría} · {pokemon.Tipo}";

            /*
            pbVidaDetalle.Value = pokemon.Vida;
            txtVidaValor.Text = $"{pokemon.Vida:0} / 100";

            pbEnergiaDetalle.Value = pokemon.Energia;
            txtEnergiaValor.Text = $"{pokemon.Energia:0} / 100";
            */


            txtCategoria.Text = pokemon.Categoría;
            txtTipo.Text = pokemon.Tipo;
            txtAltura.Text = $"{pokemon.Altura:0.##} m";
            txtPeso.Text = $"{pokemon.Peso:0.##} kg";
            txtEvolucion.Text = pokemon.Evolucion;
            txtDescripcion.Text = pokemon.Descripcion;
        }

        private void CargarVistaPokemon(iPokemon pokemon)
        {
            if (pokemon is EmpoleonARS)
            {
                EmpoleonARS vista = new EmpoleonARS();

                vista.Nombre = pokemon.Nombre;
                vista.Vida = pokemon.Vida;
                vista.Energia = pokemon.Energia;
                vista.Categoría = pokemon.Categoría;
                vista.Tipo = pokemon.Tipo;
                vista.Altura = pokemon.Altura;
                vista.Peso = pokemon.Peso;
                vista.Evolucion = pokemon.Evolucion;
                vista.Descripcion = pokemon.Descripcion;

                vista.verNombre(false);
                vista.verFilaVida(false);
                vista.verFilaEnergia(false);
                vista.verPocionVida(false);
                vista.verPocionEnergia(false);
                vista.verEscudo(false);
                vista.activarAniIdle(true);

                vbPokemon.Child = vista;
            }
        }
    }
}