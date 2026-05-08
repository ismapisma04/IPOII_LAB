using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProyectoVacioUWP_Base
{
    public sealed partial class SnorlaxISU : UserControl, iPokemon
    {
        public double Vida
        {
            get { return pbHealth.Value; }
            set 
            { 
                pbHealth.Value = value; 
            }
        }

        public double Energia
        {
            get { return pbEnergy.Value; }
            set { pbEnergy.Value = value; }
        }

        public string Nombre { get => "Snorlax"; set { } }
        public string Categoría { get => "Dormir"; set { } }
        public string Tipo { get => "Normal"; set { } }
        public double Altura { get => 2.1; set { } }
        public double Peso { get => 460.0; set { } }
        public string Evolucion { get => "Ninguna"; set { } }
        public string Descripcion { get => "Un Pokémon muy perezoso. Solo se despierta para comer."; set { } }

        public void verFondo(bool ver) { }
        public void verFilaVida(bool ver) 
        { 
            var vis = ver ? Visibility.Visible : Visibility.Collapsed;
            pbHealth.Visibility = vis; 
            if (imgCorazon != null) imgCorazon.Visibility = vis; 
        }
        public void verFilaEnergia(bool ver) 
        { 
            var vis = ver ? Visibility.Visible : Visibility.Collapsed;
            pbEnergy.Visibility = vis; 
            if (imgRayo != null) imgRayo.Visibility = vis; 
        }
        public void verPocionVida(bool ver) { imRedPotion.Visibility = ver ? Visibility.Visible : Visibility.Collapsed; }
        public void verPocionEnergia(bool ver) { imYellowPotion.Visibility = ver ? Visibility.Visible : Visibility.Collapsed; }
        public void verNombre(bool ver) { borderNombre.Visibility = ver ? Visibility.Visible : Visibility.Collapsed; }
        public void verEscudo(bool ver) { if(ver) activarEscudo(null, null); else haloEscudo.Opacity = 0; hasEscudo = ver; }

        public void activarAniIdle(bool activar) { if (activar) saludar(null, null); else { Saludo_BrazoDerech.Stop(); Saludo_BrazoIzq.Stop(); } }
        public void animacionAtaqueFlojo() { DetenerTodasLasAnimaciones(); saludar(null, null); }
        public void animacionAtaqueFuerte() { DetenerTodasLasAnimaciones(); probarTambor(null, null); }
        public void animacionDefensa() { DetenerTodasLasAnimaciones(); activarEscudo(null, null); }
        public void animacionDescasar() { DetenerTodasLasAnimaciones(); activarDescanso(null, null); }
        public void animacionCansado() { DetenerTodasLasAnimaciones(); Snorlax_Triste.Begin(); }
        public void animacionNoCansado() { DetenerTodasLasAnimaciones(); Snorlax_Feliz.Begin(); }
        public void animacionHerido() { DetenerTodasLasAnimaciones(); Snorlax_Herido.Begin(); }
        public void animacionNoHerido() { DetenerTodasLasAnimaciones(); Snorlax_NoHerido.Begin(); }
        public void animacionDerrota() { DetenerTodasLasAnimaciones(); Snorlax_Triste.Begin(); }

        private void DetenerTodasLasAnimaciones()
        {
            Saludo_BrazoDerech.Stop();
            Saludo_BrazoIzq.Stop();
            Snorlax_Triste.Stop();
            Snorlax_Feliz.Stop();
            Snorlax_Herido.Stop();
            Snorlax_NoHerido.Stop();
            Tambor_Z.Stop();
            Descanso_Snorlax.Stop();
            if (sbEscudo != null) sbEscudo.Stop();
            haloEscudo.Opacity = 0;
            hasEscudo = false;
        }

        public SnorlaxISU()
        {
            InitializeComponent();
            this.InitializeComponent();
        }

        bool hasEscudo = false;
        Storyboard sbEscudo;

        // --- LÓGICA DE LA POCIÓN ---
        private void useRedPotion(object sender, PointerRoutedEventArgs e)
        {
        }

        private void useYellowPotion(object sender, PointerRoutedEventArgs e)
        {
        }

        // --- ANIMACIONES DE LOS OJOS (CON CONTROL DE SOLAPAMIENTO) ---

        // Método para enfadar el ojo izquierdo
        private void enfadarOjoI(object sender, PointerRoutedEventArgs e)
        {
            ptOjoIzq.IsHitTestVisible = false;

            Storyboard sb = (Storyboard)this.ptOjoIzq.Resources["ojoIzqRojoKey"];
            sb.Completed += finOjoIzq;
            sb.Begin();
        }

        private void finOjoIzq(object sender, object e)
        {
            ptOjoIzq.IsHitTestVisible = true;
            Storyboard sb = (Storyboard)this.ptOjoIzq.Resources["ojoIzqRojoKey"];
            sb.Completed -= finOjoIzq;
        }

        // Método para enfadar el ojo derecho
        private void enfadarOjoD(object sender, PointerRoutedEventArgs e)
        {
            ptOjoDer.IsHitTestVisible = false;

            Storyboard sb = (Storyboard)this.ptOjoDer.Resources["ojoDerRojoKey"];
            sb.Completed += finOjoDer;
            sb.Begin();
        }

        private void finOjoDer(object sender, object e)
        {
            ptOjoDer.IsHitTestVisible = true;
            Storyboard sb = (Storyboard)this.ptOjoDer.Resources["ojoDerRojoKey"];
            sb.Completed -= finOjoDer;
        }

        private void saludar(object sender, PointerRoutedEventArgs e)
        {
            Saludo_BrazoDerech.Begin();
            Saludo_BrazoIzq.Begin();
        }

        private void probarTambor(object sender, PointerRoutedEventArgs e)
        {
            Tambor_Z.Begin();
        }

        // --- ANIMACIÓN ESCUDO ---
        private void activarEscudo(object sender, PointerRoutedEventArgs e)
        {
            hasEscudo = true;

            sbEscudo = new Storyboard();
            DoubleAnimation animOpacidad = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            Storyboard.SetTarget(animOpacidad, haloEscudo);
            Storyboard.SetTargetProperty(animOpacidad, "Opacity");

            DoubleAnimation animScaleX = new DoubleAnimation
            {
                From = 0.5,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new BounceEase { Bounces = 2, EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(animScaleX, haloEscudo);
            Storyboard.SetTargetProperty(animScaleX, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");

            DoubleAnimation animScaleY = new DoubleAnimation
            {
                From = 0.5,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new BounceEase { Bounces = 2, EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(animScaleY, haloEscudo);
            Storyboard.SetTargetProperty(animScaleY, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            sbEscudo.Children.Add(animOpacidad);
            sbEscudo.Children.Add(animScaleX);
            sbEscudo.Children.Add(animScaleY);

            sbEscudo.Begin();
        }

        private void activarDescanso(object sender, PointerRoutedEventArgs e)
        {
            Descanso_Snorlax.Begin();
        }

    }
}
