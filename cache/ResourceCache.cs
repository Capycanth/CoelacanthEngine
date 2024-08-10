using CoelacanthEngine.state;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CoelacanthEngine.cache
{
    public class ResourceCache
    {
        // ContentManager instance.
        private ContentManager _contentManager;

        // Singleton instance.
        private static ResourceCache _instance;

        private static readonly string TEXTURES = "Textures/";
        private static readonly string FONTS = "Fonts/";
        private static readonly string SONGS = "Songs/";
        private static readonly string SOUNDS = "Sounds/";

        // Private constructor to prevent instantiation from outside.
        private ResourceCache(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        // Method to initialize the singleton with the ContentManager.
        public static void Initialize(ContentManager contentManager)
        {
            if (_instance == null)
            {
                _instance = new ResourceCache(contentManager);
            }
        }

        // Property to get the singleton instance.
        public static ResourceCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("ResourceCache is not initialized. Call Initialize() with a valid ContentManager first.");
                }
                return _instance;
            }
        }

        // Generic method to get a resource.
        public T GetResource<T>(string assetName)
        {
            return _contentManager.Load<T>(assetName);
        }

        // Method to load resources asynchronously from a ResourceManifest.
        public async Task LoadResourcesAsync(ResourceManifest manifest)
        {
            var tasks = new List<Task>();

            foreach (var texture in manifest.Textures)
            {
                tasks.Add(Task.Run(() => GetResource<Texture2D>(TEXTURES + texture)));
            }

            foreach (var font in manifest.Fonts)
            {
                tasks.Add(Task.Run(() => GetResource<SpriteFont>(FONTS + font)));
            }

            foreach (var sound in manifest.Sounds)
            {
                tasks.Add(Task.Run(() => GetResource<SoundEffect>(SOUNDS + sound)));
            }

            /*  Ignore Songs -> Now handled by AudioManager
            foreach (var song in manifest.Songs)
            {
                tasks.Add(Task.Run(() => GetResource<Song>(SONGS + song)));
            }*/

            await Task.WhenAll(tasks);
        }

        // Method to unload all resources.
        public void Unload()
        {
            _contentManager.Unload();
        }
    }
}