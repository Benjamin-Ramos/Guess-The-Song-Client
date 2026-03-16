using Microsoft.AspNetCore.SignalR.Client;
using NAudio.CoreAudioApi;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfAppClienteAdivinarCancion;
public class MainViewModel : INotifyPropertyChanged
{
    private HubConnection _connection;
    private readonly AudioService _audioService = new AudioService();
    private readonly MusicService _musicService = new MusicService();

    public ObservableCollection<JugadorDto> ListaPodio { get; set; } = new ObservableCollection<JugadorDto>();
    public ObservableCollection<JugadorDto> ListaJugadores { get; set; } = new ObservableCollection<JugadorDto>();
    public ObservableCollection<string> MensajesChat { get; set; } = new ObservableCollection<string>();
    public ObservableCollection<string> OpcionesBotones { get; set; } = new ObservableCollection<string>();

    private string _portadaAlbumActual;
    public string PortadaAlbumActual
    {
        get => _portadaAlbumActual;
        set { _portadaAlbumActual = value; OnPropertyChanged(); }
    }

    private bool _mostrarPortadaReal;
    public bool MostrarPortadaReal
    {
        get => _mostrarPortadaReal;
        set { _mostrarPortadaReal = value; OnPropertyChanged(); }
    }
    private bool _puedeResponder;
    public bool PuedeResponder
    {
        get => _puedeResponder;
        set { _puedeResponder = value; OnPropertyChanged(); }
    }
    private string _nombreSalaActual;
    private bool _mostrandoConteo;
    public bool MostrandoConteo
    {
        get => _mostrandoConteo;
        set { _mostrandoConteo = value; OnPropertyChanged(); }
    }

    private string _segundosConteo;
    public string SegundosConteo
    {
        get => _segundosConteo;
        set { _segundosConteo = value; OnPropertyChanged(); }
    }

    private bool _estaConectado;
    public bool EstaConectado { get => _estaConectado; set { _estaConectado = value; OnPropertyChanged(); } }

    private bool _esHost;
    public bool EsHost { get => _esHost; set { _esHost = value; OnPropertyChanged(); } }

    private bool _enPartida;
    public bool EnPartida { get => _enPartida; set { _enPartida = value; OnPropertyChanged(); } }

    private string _fotoArtista;
    public string FotoArtista { get => _fotoArtista; set { _fotoArtista = value; OnPropertyChanged(); } }
    private bool _mostrarPodio = false;
    public bool MostrarPodio
    {
        get => _mostrarPodio;
        set { _mostrarPodio = value; OnPropertyChanged(); }
    }
    private string _mensajeJuego;
    public string MensajeJuego { get => _mensajeJuego; set { _mensajeJuego = value; OnPropertyChanged(); } }

    private double _volumenActual = 50;
    public double VolumenActual
    {
        get => _volumenActual;
        set
        {
            _volumenActual = value;
            OnPropertyChanged();
            _audioService.SetVolume((float)(_volumenActual / 100.0));
        }
    }
    public async Task Conectar(string url, string salaId, string nick)
    {
        if (!url.EndsWith("/gamehub")) url += "/gamehub";

        _connection = new HubConnectionBuilder()
            .WithUrl(url)
            .WithAutomaticReconnect()
            .Build();

        _connection.On("IniciarConteo", async () =>
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                MostrandoConteo = true;

                SegundosConteo = "3";
                await Task.Delay(1000);

                SegundosConteo = "2";
                await Task.Delay(1000);

                SegundosConteo = "1";
                await Task.Delay(1000);

                SegundosConteo = "¡LISTO!";
                await Task.Delay(800);

                MostrandoConteo = false;
            });
        });

        _connection.On<NuevaRondaDto>("NuevaRonda", async (datos) =>
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                EnPartida = true;
                PuedeResponder = false;
                MostrarPortadaReal = false;

                string progreso = $"Ronda {datos.NumeroRonda}/{datos.TotalRondas}";

                OpcionesBotones.Clear();
                foreach (var opt in datos.Opciones) { OpcionesBotones.Add(opt); }

                _audioService.Play(datos.PreviewUrl);

                MensajeJuego = $"{progreso} - Escucha con atención...";

                await Task.Delay(3000);

                PuedeResponder = true;
                MensajeJuego = $"{progreso} - ¡YA PUEDES RESPONDER!";
            });
        });

        _connection.On("RespuestaIncorrecta", () =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PuedeResponder = false;
                MensajeJuego = "❌ ¡Has fallado! Espera a la siguiente ronda.";
            });
        });

        _connection.On<string, string, string>("RondaGanada", (ganador, cancion, urlPortada) =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _audioService.Stop();
                PuedeResponder = false;
                MostrarPortadaReal = true;
                PortadaAlbumActual = urlPortada.Replace("100x100", "400x400");

                if (ganador == "Nadie" || string.IsNullOrEmpty(ganador))
                {
                    MensajeJuego = $"⌛ ¡Tiempo agotado! Nadie acertó. Era: {cancion}";
                }
                else
                {
                    MensajeJuego = $"¡{ganador} acertó! Era: {cancion}";
                }
            });
        });

        _connection.On("PararAudio", () => _audioService.Stop());

        _connection.On<string>("CargarImagenArtista", (urlFoto) =>
        {
            Application.Current.Dispatcher.Invoke(() => FotoArtista = urlFoto);
        });

        _connection.On<string, string>("TiempoAgotado", (cancion, urlPortada) =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _audioService.Stop();
                PuedeResponder = false;

                PortadaAlbumActual = urlPortada.Replace("100x100", "400x400");
                MostrarPortadaReal = true;

                MensajeJuego = $"⌛ ¡Tiempo agotado! Nadie acertó. Era: {cancion}";
            });
        });

        _connection.On<List<JugadorDto>>("ActualizarListaJugadores", (jugadores) =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ListaJugadores.Clear();
                foreach (var p in jugadores) ListaJugadores.Add(p);
            });
        });

        _connection.On<string, string>("RecibirMensaje", (usuario, mensaje) =>
        {
            Application.Current.Dispatcher.Invoke(() => MensajesChat.Add($"{usuario}: {mensaje}"));
        });

        _connection.On<bool>("AsignarHost", (esHost) =>
        {
            Application.Current.Dispatcher.Invoke(() => EsHost = esHost);
        });

        _connection.On("LoginExitoso", () =>
        {
            Application.Current.Dispatcher.Invoke(() => EstaConectado = true);
        });

        _connection.On<string>("ErrorDeConexion", (msg) =>
        {
            Application.Current.Dispatcher.Invoke(() => MessageBox.Show(msg));
        });

        try
        {
            await _connection.StartAsync();
            await _connection.InvokeAsync("UnirseASala", salaId, nick);
            _nombreSalaActual = salaId;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }

        _connection.On<List<JugadorDto>>("FinDelJuego", (podio) =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _audioService.Stop();
                EnPartida = false;

                ListaPodio.Clear();
                foreach (var j in podio)
                {
                    ListaPodio.Add(j);
                }

                MostrarPodio = true;

            });
        });
    }

    public async Task IniciarJuegoHost(string nombreArtista, string salaId)
    {
        var canciones = await _musicService.GetSongsAsync(nombreArtista);

        if (canciones.Count < 5)
        {
            MessageBox.Show("No hay suficientes canciones.");
            return;
        }

        await _connection.InvokeAsync("IniciarPartida", salaId, canciones, nombreArtista);
    }

    public async Task EnviarRespuesta(string salaId, string respuesta)
    {
        await _connection.InvokeAsync("ValidarRespuesta", salaId, respuesta);
    }

    public async Task EnviarMensajeChat(string salaId, string nick, string mensaje)
    {
        if (string.IsNullOrWhiteSpace(mensaje)) return;
        await _connection.InvokeAsync("EnviarMensaje", salaId, nick, mensaje);
    }
    public async Task ReiniciarPuntuacionesHost()
    {
        if (EsHost && _connection != null)
        {
            try
            {
                await _connection.InvokeAsync("ReiniciarPuntos", _nombreSalaActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reiniciar: {ex.Message}");
            }
        }
    }
    private ICommand _cerrarPodioCommand;
    public ICommand CerrarPodioCommand => _cerrarPodioCommand ??= new RelayCommand(() =>
    {
        MostrarPodio = false;

        MensajesChat.Add("Sistema: Los resultados se han cerrado.");
    });

    public void CerrarPodio()
    {
        MostrarPodio = false;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    public RelayCommand(Action execute) => _execute = execute;
    public bool CanExecute(object parameter) => true;
    public void Execute(object parameter) => _execute();
    public event EventHandler CanExecuteChanged;
}