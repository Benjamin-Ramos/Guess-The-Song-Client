using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppClienteAdivinarCancion;
public partial class MainWindow : Window
{
    private MainViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        this.DataContext = _viewModel;
    }

    private async void BtnConectar_Click(object sender, RoutedEventArgs e)
    {
        string url = TxtUrl.Text;
        string sala = TxtSala.Text;
        string nick = TxtNick.Text;

        await _viewModel.Conectar(url, sala, nick);
    }

    private async void BtnEnviarChat_Click(object sender, RoutedEventArgs e)
    {
        await EnviarMensaje();
    }

    private async void TxtMensajeChat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) await EnviarMensaje();
    }

    private async Task EnviarMensaje()
    {
        if (!string.IsNullOrWhiteSpace(TxtMensajeChat.Text))
        {
            await _viewModel.EnviarMensajeChat(TxtSala.Text, TxtNick.Text, TxtMensajeChat.Text);
            TxtMensajeChat.Clear();
        }
    }
    private async void BtnReiniciarPuntos_Click(object sender, RoutedEventArgs e)
    {
        var resultado = MessageBox.Show(
            "¿Estás seguro de que quieres reiniciar las puntuaciones de todos los jugadores?",
            "Confirmar Reinicio",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (resultado == MessageBoxResult.Yes)
        {
            await _viewModel.ReiniciarPuntuacionesHost();
        }
    }
    private void BtnCerrarPodio_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.MostrarPodio = false;
    }
    private async void BtnRespuesta_Click(object sender, RoutedEventArgs e)
    {
        if (!_viewModel.PuedeResponder) return;

        _viewModel.PuedeResponder = false;

        var btn = (Button)sender;
        string respuesta = btn.Content.ToString();
        string sala = TxtSala.Text;

        try
        {
            await _viewModel.EnviarRespuesta(sala, respuesta);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al enviar: {ex.Message}");
        }
    }
    private async void BtnEmpezar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string artista = TxtArtistaBusqueda.Text;
            string sala = TxtSala.Text;

            if (string.IsNullOrWhiteSpace(artista))
            {
                MessageBox.Show("Por favor, escribe el nombre de un artista para buscar sus canciones.");
                return;
            }

            var btn = (Button)sender;
            btn.IsEnabled = false;

            await _viewModel.IniciarJuegoHost(artista, sala);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al iniciar la partida: {ex.Message}");
        }
        finally
        {
            ((Button)sender).IsEnabled = true;
        }
    }
}