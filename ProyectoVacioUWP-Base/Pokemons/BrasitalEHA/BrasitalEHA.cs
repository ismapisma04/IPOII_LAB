using System;
using System.Collections.Generic;
using ProyectoVacioUWP_Base;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProyectoUWPenBlanco
{
    public sealed partial class BrasitalEHA : UserControl, iPokemon
    {
        // =============================================
        // PROPIEDADES iPokemon
        // =============================================

        private double _vida = 100;
        public double Vida
        {
            get { return _vida; }
            set { _vida = value; pbVida.Value = value; }
        }

        private double _energia = 100;
        public double Energia
        {
            get { return _energia; }
            set { _energia = value; pbEnergia.Value = value; }
        }

        public string Nombre      { get; set; } = "Brasita Frostito";
        public string Categoría   { get; set; } = "Dragón de Hielo";
        public string Tipo        { get; set; } = "Hielo / Dragón";
        public double Altura      { get; set; } = 0.6;
        public double Peso        { get; set; } = 12.0;
        public string Evolucion   { get; set; } = "Brasita → Frostivern";

        private string _descripcion = "Brasita Frostito es un Pokémon de tipo Hielo y Dragón. Sus " +
            "grandes alas membranosas le permiten volar a gran velocidad. " +
            "Sus cuernos naranjas almacenan energía glacial que libera en " +
            "forma de ventiscas. Sus enormes ojos azules pueden ver a través " +
            "de las tormentas de nieve más densas.";

        public string Descripcion 
        {
            get
            {
                var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                string val = loader.GetString("DescBrasital/Text");
                return string.IsNullOrEmpty(val) ? _descripcion : val;
            }
            set => _descripcion = value; 
        }

        public BrasitalEHA()
        {
            this.InitializeComponent();
            tbNombre.Text = Nombre + " · " + Tipo;

        }

        // =============================================
        // HELPERS: cambiar cara
        // 
        // =============================================

        private void CaraNormal()
        {
            ojoNormalIzq.Visibility  = Visibility.Visible;
            pupilaIzq.Visibility     = Visibility.Visible;
            brilloIzq.Visibility     = Visibility.Visible;
            ojoNormalDer.Visibility  = Visibility.Visible;
            pupilaDer.Visibility     = Visibility.Visible;
            brilloDer.Visibility     = Visibility.Visible;
            ojoCerradoIzq.Visibility = Visibility.Collapsed;
            ojoCerradoDer.Visibility = Visibility.Collapsed;
            ojoHeridoIzq.Visibility  = Visibility.Collapsed;
            ojoHeridoDer.Visibility  = Visibility.Collapsed;
            boca.Visibility          = Visibility.Visible;
            bocaTriste.Visibility    = Visibility.Collapsed;
            bocaDormida.Visibility   = Visibility.Collapsed;
            zzzText.Visibility       = Visibility.Collapsed;
        }

        private void CaraCansada()
        {
            ojoNormalIzq.Visibility  = Visibility.Collapsed;
            pupilaIzq.Visibility     = Visibility.Collapsed;
            brilloIzq.Visibility     = Visibility.Collapsed;
            ojoNormalDer.Visibility  = Visibility.Collapsed;
            pupilaDer.Visibility     = Visibility.Collapsed;
            brilloDer.Visibility     = Visibility.Collapsed;
            ojoCerradoIzq.Visibility = Visibility.Visible;
            ojoCerradoDer.Visibility = Visibility.Visible;
            ojoHeridoIzq.Visibility  = Visibility.Collapsed;
            ojoHeridoDer.Visibility  = Visibility.Collapsed;
            boca.Visibility          = Visibility.Collapsed;
            bocaTriste.Visibility    = Visibility.Visible;
            bocaDormida.Visibility   = Visibility.Collapsed;
            zzzText.Visibility       = Visibility.Collapsed;
        }

        private void CaraHerida()
        {
            ojoNormalIzq.Visibility  = Visibility.Collapsed;
            pupilaIzq.Visibility     = Visibility.Collapsed;
            brilloIzq.Visibility     = Visibility.Collapsed;
            ojoNormalDer.Visibility  = Visibility.Collapsed;
            pupilaDer.Visibility     = Visibility.Collapsed;
            brilloDer.Visibility     = Visibility.Collapsed;
            ojoCerradoIzq.Visibility = Visibility.Collapsed;
            ojoCerradoDer.Visibility = Visibility.Collapsed;
            ojoHeridoIzq.Visibility  = Visibility.Visible;
            ojoHeridoDer.Visibility  = Visibility.Visible;
            boca.Visibility          = Visibility.Collapsed;
            bocaTriste.Visibility    = Visibility.Visible;
            bocaDormida.Visibility   = Visibility.Collapsed;
            zzzText.Visibility       = Visibility.Collapsed;
        }

        private void CaraDormida()
        {
            ojoNormalIzq.Visibility  = Visibility.Collapsed;
            pupilaIzq.Visibility     = Visibility.Collapsed;
            brilloIzq.Visibility     = Visibility.Collapsed;
            ojoNormalDer.Visibility  = Visibility.Collapsed;
            pupilaDer.Visibility     = Visibility.Collapsed;
            brilloDer.Visibility     = Visibility.Collapsed;
            ojoCerradoIzq.Visibility = Visibility.Visible;
            ojoCerradoDer.Visibility = Visibility.Visible;
            ojoHeridoIzq.Visibility  = Visibility.Collapsed;
            ojoHeridoDer.Visibility  = Visibility.Collapsed;
            boca.Visibility          = Visibility.Collapsed;
            bocaTriste.Visibility    = Visibility.Collapsed;
            bocaDormida.Visibility   = Visibility.Visible;
            zzzText.Visibility       = Visibility.Visible;
        }

        // =============================================
        // VISIBILIDAD
        // =============================================

        public void verFondo(bool ver)
        {
            rectFondo.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verFilaVida(bool ver)
        {
            filaVida.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            panelBarras.Visibility = (filaVida.Visibility == Visibility.Visible || filaEnergia.Visibility == Visibility.Visible)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public void verFilaEnergia(bool ver)
        {
            filaEnergia.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            panelBarras.Visibility = (filaVida.Visibility == Visibility.Visible || filaEnergia.Visibility == Visibility.Visible)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public void verPocionVida(bool ver)
        {
            pocimaVida.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verPocionEnergia(bool ver)
        {
            pocimaEnergia.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verNombre(bool ver)
        {
            borderNombre.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verEscudo(bool ver)
        {
            if (ver)
            {
                aniEscudoActivo.Begin(); // activa animación escudo + cola
            }
            else
            {
                aniEscudoActivo.Stop();  // detiene animación
                escudo.Opacity = 0;
                escudoScale.ScaleX = 1;
                escudoScale.ScaleY = 1;
            }
        }

        // =============================================
        // ANIMACIONES
        // =============================================

        public void activarAniIdle(bool activar)
        {
            if (activar) { aniIdle.Begin(); aniAlas.Begin(); aniColaIdle.Begin(); // para mover la cola
            }
            else         { aniIdle.Stop();  aniAlas.Stop(); aniColaIdle.Stop(); }
        }

        public void animacionAtaqueFuerte()
        {
            CaraNormal();
            aniAtaqueFuerte.Begin();
        }

        public void animacionAtaqueFlojo()
        {
            CaraNormal();
            aniAtaqueFlojo.Begin();
        }

        public void animacionDefensa()
        {
            aniDefensaShake.Begin();
            aniDefensaEscudo.Begin();
            aniDefensaFlash.Begin();
            aniDefensaRetroceso.Begin();
        }

        public void animacionDescasar()
        {
            aniAlas.Stop();
            aniIdle.Stop();
            CaraDormida();
            aniDescansar.Begin();
        }

        public void animacionCansado()
        {
            CaraCansada();
            aniCansado.Begin();
        }

        public void animacionNoCansado()
        {
            CaraNormal();
            aniNoCansado.Begin();
        }

        public void animacionHerido()
        {
            CaraHerida();
            overlayHerido.Opacity = 0.5;
            aniHerido.Begin();
        }

        public void animacionNoHerido()
        {
            CaraNormal();
            aniHerido.Stop();
            overlayHerido.Opacity = 0;

        }

        public void animacionDerrota()
        {
            aniAlas.Stop();
            aniIdle.Stop();
            CaraHerida();
            aniDerrota.Begin();
        }
    }
}
