using CoelacanthEngine.log;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CoelacanthEngine.cache
{
    public sealed class ResourceCache
    {
        private static readonly Lazy<ResourceCache> lazy = new Lazy<ResourceCache>(() => new ResourceCache());
        public static ResourceCache Instance => lazy.Value;

        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, Song> _songs;
        private Dictionary<string, SoundEffect> _soundEffects;
        private ContentManager _contentManager;
        private readonly Logger _logger = new Logger(typeof(ResourceCache));

        private ResourceCache()
        {
            _textures = new Dictionary<string, Texture2D>();
            _songs = new Dictionary<string, Song>();
            _soundEffects = new Dictionary<string, SoundEffect>();
        }

        public void Initialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        // Load and get Texture2D
        public Texture2D LoadTexture(string key, string assetName)
        {
            if (!_textures.ContainsKey(key))
            {
                Texture2D texture = _contentManager.Load<Texture2D>(assetName);
                _textures[key] = texture;
                return texture;
            }
            return _textures[key];
        }

        public Texture2D GetTexture(string key)
        {
            if (_textures.TryGetValue(key, out Texture2D texture))
            {
                return texture;
            }
            _logger.Error($"Texture with key {key} not found in cache.");
            throw new Exception();
        }

        // Load and get Song
        public Song LoadSong(string key, string assetName)
        {
            if (!_songs.ContainsKey(key))
            {
                Song song = _contentManager.Load<Song>(assetName);
                _songs[key] = song;
                return song;
            }
                return _songs[key];
        }

        public Song GetSong(string key)
        {
            if (_songs.TryGetValue(key, out Song song))
            {
                return song;
            }

            _logger.Error($"Song with key {key} not found in cache.");
            throw new Exception();
        }

        // Load and get SoundEffect
        public SoundEffect LoadSoundEffect(string key, string assetName)
        {
            if (!_soundEffects.ContainsKey(key))
            {
                SoundEffect soundEffect = _contentManager.Load<SoundEffect>(assetName);
                _soundEffects[key] = soundEffect;
                return soundEffect;
            }
            return _soundEffects[key];
        }

        public SoundEffect GetSoundEffect(string key)
        {
            if (_soundEffects.TryGetValue(key, out SoundEffect soundEffect))
            {
                return soundEffect;
            }

            _logger.Error($"SoundEffect with key {key} not found in cache.");
            throw new Exception();
        }

        // Clear all caches
        public void Clear()
        {
            _textures.Clear();
            _songs.Clear();
            _soundEffects.Clear();
            _contentManager.Unload();
        }
    }
}