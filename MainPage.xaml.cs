using Microsoft.Maui.Controls;

namespace aho
{
    public partial class MainPage : ContentPage
    {
        Random rnd = new Random();
        int multiplicador;
        int multiplicando;
        int respuestaCorrecta;
        int intentosRestantes = 7;
        private int victorias = 0;


        public MainPage()
        {
            InitializeComponent();
            SeleccionarNuevaMultiplicacion();
            MostrarImagenAhorcado();
            labelVictorias.Text = "Victorias: " + victorias.ToString();
            labelIntentos.Text = "Intentos restantes: " + intentosRestantes.ToString();
        }

        private void SeleccionarNuevaMultiplicacion()
        {
            multiplicador = rnd.Next(2, 10);
            multiplicando = rnd.Next(1, 11);
            respuestaCorrecta = multiplicador * multiplicando;

            labelMultiplicacion.Text = $"{multiplicador} x {multiplicando}";
            textBoxRespuesta.Text = "";
        }

        private void MostrarImagenAhorcado()
        {
            string nombreImagen = "";

            switch (intentosRestantes)
            {
                case 7:
                    nombreImagen = "ahorcado_0.png";
                    break;
                case 6:
                    nombreImagen = "ahorcado_1.png";
                    break;
                case 5:
                    nombreImagen = "ahorcado_2.png";
                    break;
                case 4:
                    nombreImagen = "ahorcado_3.png";
                    break;
                case 3:
                    nombreImagen = "ahorcado_4.png";
                    break;
                case 2:
                    nombreImagen = "ahorcado_5.png";
                    break;
                case 1:
                    nombreImagen = "ahorcado_6.png";
                    break;
                case 0:
                    nombreImagen = "ahorcado_fin.png";
                    DisplayAlert("¡Incorrecto!", "La respuesta correcta era: " + respuestaCorrecta, "OK");
                    ReiniciarJuego();
                    return; 
                 
            }

            var imagen = ImageSource.FromResource($"aho.Resources.Images.{nombreImagen}");

         
            imagenAhorcado.Source = imagen;
        }



        private void ReiniciarJuego()
        {
            intentosRestantes = 7;
            SeleccionarNuevaMultiplicacion();
            MostrarImagenAhorcado();
        }

        private void ButtonComprobar_Clicked(object sender, EventArgs e)
        {
            string respuestaUsuarioTexto = textBoxRespuesta.Text;

           
            if (!int.TryParse(respuestaUsuarioTexto, out int respuestaUsuario))
            {
                DisplayAlert("Error", "Por favor, ingresa un número válido.", "OK");
                return; 
            }

           
            if (respuestaUsuario == respuestaCorrecta)
            {
                DisplayAlert("¡Correcto!", "Has acertado la respuesta.", "OK");
                
                victorias++;
                labelVictorias.Text = "Victorias: " + victorias.ToString(); 
                labelIntentos.Text = "Intentos restantes: " + intentosRestantes.ToString();
                ReiniciarJuego();
            }
            else
            {
                DisplayAlert("Respuesta incorrecta", "Inténtalo de nuevo.", "OK");
                intentosRestantes--; 
                labelIntentos.Text = "Intentos restantes: " + intentosRestantes.ToString();
                MostrarImagenAhorcado(); 

                if (intentosRestantes == 0)
                {
                    DisplayAlert("Perdiste", "La respuesta correcta era: " + respuestaCorrecta, "OK");
                    ReiniciarJuego();
                    intentosRestantes = 7;
                    MostrarImagenAhorcado();
                }
            }
        }
    }

}
