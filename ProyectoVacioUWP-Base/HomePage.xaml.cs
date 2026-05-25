<<<<<<< HEAD
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
=======
using Windows.UI.Xaml.Controls;
>>>>>>> 9d834adeac127a597f8b9d0a1ebf7affb9a58167

namespace ProyectoVacioUWP_Base
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }
<<<<<<< HEAD


        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Pokeball_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard giro = new Storyboard();

            DoubleAnimation animacion = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(System.TimeSpan.FromSeconds(8)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard.SetTarget(animacion, rotatePokeball);
            Storyboard.SetTargetProperty(animacion, "Angle");

            giro.Children.Add(animacion);

            giro.Begin();
        }
=======
>>>>>>> 9d834adeac127a597f8b9d0a1ebf7affb9a58167
    }
}