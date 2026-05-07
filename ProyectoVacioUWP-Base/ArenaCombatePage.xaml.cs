using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ProyectoVacioUWP_Base
{
    public sealed partial class ArenaCombatePage : Page
    {
        private List<iPokemon> listaPokemons = new List<iPokemon>();

        private iPokemon pokemonP1;
        private iPokemon pokemonP2;

        private bool p1Defendiendo = false;
        private bool p2Defendiendo = false;

        private DispatcherTimer gameTimer;
        private bool combateTerminado = false;
        private bool accionEnCurso = false;

        // Constantes del combate
        private const int DANO_DEBIL = 15;
        private const int COSTE_ENERGIA_DEBIL = 25;
        private const int DANO_FUERTE = 30;
        private const int COSTE_ENERGIA_FUERTE = 50;
        private const int RECARGA_ENERGIA_TICK = 2;

        public ArenaCombatePage()
        {
            this.InitializeComponent();
            listaPokemons = PokemonCatalogo.CrearPokemons();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(500);
            gameTimer.Tick += GameTimer_Tick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is CombatePage.CombateParametros parametros)
            {
                var pokemonControlP1 = PokemonFactory.CrearControlPokemon(BuscarPokemonOriginalPorNombre(parametros.NombrePokemonP1));
                var pokemonControlP2 = PokemonFactory.CrearControlPokemon(BuscarPokemonOriginalPorNombre(parametros.NombrePokemonP2));

                pokemonP1 = pokemonControlP1 as iPokemon;
                pokemonP2 = pokemonControlP2 as iPokemon;

                if (pokemonP1 != null)
                {
                    txtNombreP1.Text = pokemonP1.Nombre;
                    MostrarPokemonEnContenedor(contenedorP1, pokemonControlP1);
                }

                if (pokemonP2 != null)
                {
                    txtNombreP2.Text = pokemonP2.Nombre;
                    MostrarPokemonEnContenedor(contenedorP2, pokemonControlP2);
                }

                if (pokemonP1 != null && pokemonP2 != null)
                {
                    InicializarBarras();
                    gameTimer.Start();
                }
            }
        }

        private iPokemon BuscarPokemonOriginalPorNombre(string nombre)
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

        private void MostrarPokemonEnContenedor(Grid contenedor, UserControl control)
        {
            contenedor.Children.Clear();

            if (control != null && control is iPokemon pokemonVisual)
            {
                pokemonVisual.verFondo(false);
                pokemonVisual.verFilaVida(false);
                pokemonVisual.verFilaEnergia(false);
                pokemonVisual.verPocionVida(false);
                pokemonVisual.verPocionEnergia(false);
                pokemonVisual.verNombre(false);
                pokemonVisual.verEscudo(false);
                pokemonVisual.activarAniIdle(true);

                Viewbox vb = new Viewbox
                {
                    Stretch = Stretch.Uniform,
                    Child = control
                };
                contenedor.Children.Add(vb);
            }
        }

        private void InicializarBarras()
        {
            pbVidaP1.Maximum = pokemonP1.Vida;
            pbVidaP2.Maximum = pokemonP2.Vida;
            pbEnergiaP1.Maximum = pokemonP1.Energia;
            pbEnergiaP2.Maximum = pokemonP2.Energia;
            ActualizarBarras();
        }

        private void ActualizarBarras()
        {
            pbVidaP1.Value = Math.Max(0, pokemonP1.Vida);
            pbVidaP2.Value = Math.Max(0, pokemonP2.Vida);
            pbEnergiaP1.Value = Math.Max(0, pokemonP1.Energia);
            pbEnergiaP2.Value = Math.Max(0, pokemonP2.Energia);
        }

        private void GameTimer_Tick(object sender, object e)
        {
            if (combateTerminado) return;

            // La energía se recarga sola con un DispatcherTimer.
            // No se puede comprobar si está dormido sin modificar la interfaz,
            // por lo que la energía se regenerará siempre.
            if (pokemonP1 != null)
                pokemonP1.Energia = Math.Min(100, pokemonP1.Energia + RECARGA_ENERGIA_TICK);

            if (pokemonP2 != null)
                pokemonP2.Energia = Math.Min(100, pokemonP2.Energia + RECARGA_ENERGIA_TICK);

            ActualizarBarras();
            EvaluarEstados();
        }

        private void EvaluarEstados()
        {
            if (pokemonP1.Vida < 30 && pokemonP1.Vida > 0) pokemonP1.animacionHerido();
            if (pokemonP2.Vida < 30 && pokemonP2.Vida > 0) pokemonP2.animacionHerido();

            if (pokemonP1.Vida <= 0) FinCombate(pokemonP2.Nombre);
            else if (pokemonP2.Vida <= 0) FinCombate(pokemonP1.Nombre);
        }

        private void FinCombate(string ganador)
        {
            if (combateTerminado) return;
            combateTerminado = true;
            gameTimer.Stop();

            txtFinCombate.Text = $"¡{ganador} GANA!";
            txtFinCombate.Visibility = Visibility.Visible;

            DesactivarBotonesAccion();
        }

        private void DesactivarBotonesAccion()
        {
            var botones = new List<Button> { btnDebilP1, btnFuerteP1, btnDormirP1, btnEscudoP1, btnDebilP2, btnFuerteP2, btnDormirP2, btnEscudoP2 };
            foreach (var btn in botones) btn.IsEnabled = false;
        }

        private void ActivarBotonesAccion()
        {
            var botones = new List<Button> { btnDebilP1, btnFuerteP1, btnDormirP1, btnEscudoP1, btnDebilP2, btnFuerteP2, btnDormirP2, btnEscudoP2 };
            foreach (var btn in botones) btn.IsEnabled = true;
        }

        private async void btnAtaqueDebilP1_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso || pokemonP1.Energia < COSTE_ENERGIA_DEBIL) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP1.Energia -= COSTE_ENERGIA_DEBIL;
            pokemonP1.animacionAtaqueFlojo();
            int dano = p2Defendiendo ? DANO_DEBIL / 2 : DANO_DEBIL;
            pokemonP2.Vida -= dano;
            p2Defendiendo = false;
            ActualizarBarras();
            EvaluarEstados();

            await Task.Delay(2000);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }

        private async void btnAtaqueFuerteP1_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso || pokemonP1.Energia < COSTE_ENERGIA_FUERTE) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP1.Energia -= COSTE_ENERGIA_FUERTE;
            pokemonP1.animacionAtaqueFuerte();
            int dano = p2Defendiendo ? DANO_FUERTE / 2 : DANO_FUERTE;
            pokemonP2.Vida -= dano;
            p2Defendiendo = false;
            ActualizarBarras();
            EvaluarEstados();

            await Task.Delay(3500);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }

        private async void btnDormirP1_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP1.animacionDescasar();

            await Task.Delay(3500);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }

        private async void btnEscudoP1_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP1.animacionDefensa();
            p1Defendiendo = true;

            await Task.Delay(1000);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }

        private async void btnAtaqueDebilP2_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso || pokemonP2.Energia < COSTE_ENERGIA_DEBIL) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP2.Energia -= COSTE_ENERGIA_DEBIL;
            pokemonP2.animacionAtaqueFlojo();
            int dano = p1Defendiendo ? DANO_DEBIL / 2 : DANO_DEBIL;
            pokemonP1.Vida -= dano;
            p1Defendiendo = false;
            ActualizarBarras();
            EvaluarEstados();

            await Task.Delay(2000);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }

        private async void btnAtaqueFuerteP2_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso || pokemonP2.Energia < COSTE_ENERGIA_FUERTE) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP2.Energia -= COSTE_ENERGIA_FUERTE;
            pokemonP2.animacionAtaqueFuerte();
            int dano = p1Defendiendo ? DANO_FUERTE / 2 : DANO_FUERTE;
            pokemonP1.Vida -= dano;
            p1Defendiendo = false;
            ActualizarBarras();
            EvaluarEstados();

            await Task.Delay(3500);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }

        private async void btnDormirP2_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP2.animacionDescasar();

            await Task.Delay(3500);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }

        private async void btnEscudoP2_Click(object sender, RoutedEventArgs e)
        {
            if (accionEnCurso) return;

            accionEnCurso = true;
            DesactivarBotonesAccion();

            pokemonP2.animacionDefensa();
            p2Defendiendo = true;

            await Task.Delay(1000);

            if (!combateTerminado)
            {
                accionEnCurso = false;
                ActivarBotonesAccion();
            }
        }
    }
}
