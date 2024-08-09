using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.state
{
    public class SceneManager
    {
        private Dictionary<int, BaseScene> _scenes;
        private Dictionary<int, BasePermScene> _permScenes;
        private BaseScene _currentScene;
        private bool _isLoading;

        public SceneManager()
        {
            _scenes = new Dictionary<int, BaseScene>();
            _permScenes = new Dictionary<int, BasePermScene>();  
        }

        public void AddScene(int sceneId, BaseScene scene)
        {
            _scenes[sceneId] = scene;
        }

        public void LoadPermScene(int sceneId, BasePermScene scene)
        {
            _permScenes[sceneId] = scene;
            _permScenes[sceneId].LoadContent();
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
                _permScenes[0]?.Update(deltaMs);
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
                _permScenes[0]?.Draw(spriteBatch);
            }
            else
            {
                _currentScene?.Draw(spriteBatch);
            }
        }
    }

}
