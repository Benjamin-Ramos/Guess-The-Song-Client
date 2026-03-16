using NAudio.Wave;

namespace WpfAppClienteAdivinarCancion
{
    public class AudioService
    {
        private WaveOutEvent _outputDevice;
        private MediaFoundationReader _audioFile;
        private float _volumen = 0.5f;
        public void SetVolume(float volume)
        {
            _volumen = volume;
            if (_outputDevice != null)
            {
                _outputDevice.Volume = _volumen;
            }
        }
        public void Play(string url)
        {
            Stop();
            _outputDevice = new WaveOutEvent();
            _audioFile = new MediaFoundationReader(url);
            _outputDevice.Init(_audioFile);
            _outputDevice.Play();
        }

        public void Stop()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _audioFile?.Dispose();
        }
    }
}
