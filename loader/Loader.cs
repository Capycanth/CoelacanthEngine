namespace CoelacanthEngine.loader
{
    public class Loader
    {
        private Dictionary<int, Dictionary<string, string>> _sceneAssets;

        public Loader()
        {
            _sceneAssets = new Dictionary<int, Dictionary<string, string>>();
        }

        public void AddSceneAssets(int sceneId, Dictionary<string, string> assets)
        {
            _sceneAssets[sceneId] = assets;
        }

        public Dictionary<string, string> GetAssetsForScene(int sceneId)
        {
            return _sceneAssets.TryGetValue(sceneId, out var assets) ? assets : null;
        }
    }

}
