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

            // Asignar la imagen al control Image en la interfaz de usuario
            imagenAhorcado.Source = imagen;
        }



        private void ReiniciarJuego()
        {
            intentosRestantes = 7; // Reiniciar el número de intentos a 8
            SeleccionarNuevaMultiplicacion();
            MostrarImagenAhorcado();
        }

        private void ButtonComprobar_Clicked(object sender, EventArgs e)
        {
            // Obtener la respuesta ingresada por el usuario desde el TextBox
            string respuestaUsuarioTexto = textBoxRespuesta.Text;

            // Convertir la respuesta ingresada por el usuario a un número entero
            if (!int.TryParse(respuestaUsuarioTexto, out int respuestaUsuario))
            {
                DisplayAlert("Error", "Por favor, ingresa un número válido.", "OK");
                return; // Salir del método si la respuesta no es válida
            }

            // Verificar si la respuesta del usuario es correcta
            if (respuestaUsuario == respuestaCorrecta)
            {
                DisplayAlert("¡Correcto!", "Has acertado la respuesta.", "OK");
                
                victorias++;
                labelVictorias.Text = "Victorias: " + victorias.ToString(); // Actualizar
                labelIntentos.Text = "Intentos restantes: " + intentosRestantes.ToString();
                ReiniciarJuego();
            }
            else
            {
                DisplayAlert("Respuesta incorrecta", "Inténtalo de nuevo.", "OK");
                intentosRestantes--; // Restar un intento
                labelIntentos.Text = "Intentos restantes: " + intentosRestantes.ToString();
                MostrarImagenAhorcado(); // Mostrar la imagen actualizada del ahorcado

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
