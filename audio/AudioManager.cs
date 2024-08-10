using CoelacanthEngine.cache;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace CoelacanthEngine.audio
{
    public class AudioManager
    {
        private List<string> _playlist;
        private int _currentTrackIndex;
        private Song? _currentSong;
        private bool _isLoadingNextSong;

        public AudioManager(List<string> playlist)
        {
            _playlist = playlist;
            _currentTrackIndex = 0;
            _isLoadingNextSong = false;

            MediaPlayer.MediaStateChanged += OnMediaStateChanged; // Hook into the event for when the song ends
        }

        public void Play()
        {
            if (_playlist.Count > 0)
            {
                LoadAndPlaySong(_currentTrackIndex);
            }
        }

        private void LoadAndPlaySong(int index)
        {
            // Load the song synchronously
            _currentSong = ResourceCache.Instance.GetResource<Song>("Songs/" + _playlist[index]);
            MediaPlayer.Play(_currentSong);

            // Start loading the next song asynchronously
            if (index + 1 < _playlist.Count)
            {
                _isLoadingNextSong = true;
                Task.Run(() => LoadNextSong(index + 1));
            }
        }

        private void LoadNextSong(int index)
        {
            // Load the next song asynchronously
            string nextSongName = _playlist[index];
            Song nextSong = ResourceCache.Instance.GetResource<Song>("Songs/" + nextSongName);

            // Cache the loaded song for when the current song finishes
            if (_currentTrackIndex == index - 1)
            {
                _currentSong = nextSong;
                _isLoadingNextSong = false;
            }
        }

        private void OnMediaStateChanged(object sender, EventArgs e)
        {
            if (MediaPlayer.State == MediaState.Stopped && !_isLoadingNextSong)
            {
                // Move to the next song in the playlist
                _currentTrackIndex++;

                if (_currentTrackIndex < _playlist.Count)
                {
                    LoadAndPlaySong(_currentTrackIndex);
                }
                else
                {
                    _currentTrackIndex = 0; // Optionally, loop back to the start of the playlist
                    LoadAndPlaySong(_currentTrackIndex);
                }
            }
        }
    }

}
