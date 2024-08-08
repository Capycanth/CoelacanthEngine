using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.state
{
    public class SceneManager
    {
        private Dictionary<int, BaseScene> _scenes;
        private BaseScene _currentScene;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private SpriteFont _loadingFont;
        private bool _isLoading;
        private string _loadingMessage;

        public SceneManager(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteFont loadingFont)
        {
            _scenes = new Dictionary<int, BaseScene>();
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
            _loadingFont = loadingFont;
            _loadingMessage = "Loading...";
        }

        public void AddScene(int sceneId, BaseScene scene)
        {
            _scenes[sceneId] = scene;
        }

        public async Task LoadSceneAsync(int sceneId)
        {
            if (_scenes.TryGetValue(sceneId, out var scene))
            {
                _isLoading = true;

                await scene.LoadContentAsync();
                scene.Initialize();

                _currentScene = scene;
                _isLoading = false;
            }
        }

        public void Update(float deltaMs)
        {
            if (_isLoading)
            {
                // Display loading screen or perform loading screen updates
            }
            else
            {
                _currentScene?.Update(deltaMs);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isLoading)
            {
                _spriteBatch.DrawString(_loadingFont, _loadingMessage, new Vector2(100, 100), Color.White);
            }
            else
            {
                _currentScene?.Draw(spriteBatch);
            }
        }
    }

}
