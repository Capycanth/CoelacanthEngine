using CoelacanthEngine.cache;
using CoelacanthEngine.dexvision;
using CoelacanthEngine.input;
using CoelacanthEngine.model;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.state
{
    public abstract class BaseScene
    {
        public List<DxObject> DxObjects { get; private set; }
        public List<BaseObject> GameObjects { get; private set; }
        public InputManager Input { get; private set; }
        public ResourceManifest Manifest { get; private set; }

        public BaseScene(List<DxObject> dxObjects, List<BaseObject> gameObjects)
        {
            DxObjects = dxObjects;
            GameObjects = gameObjects;
            Input = new InputManager(64);
            Manifest = new ResourceManifest();
        }

        public abstract void Initialize();

        public Task LoadContentAsync()
        {
            return ResourceCache.Instance.LoadResourcesAsync(Manifest);
        }

        public virtual void Update(float deltaMs)
        {
            Input.Update(deltaMs);
            foreach (DxObject dxObject in DxObjects)
                dxObject.Update(Input, deltaMs);
            foreach (BaseObject baseObject in GameObjects)
                baseObject.Update(Input, deltaMs);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(DxObject dxObject in DxObjects)
                dxObject.Draw(spriteBatch);
            foreach(BaseObject baseObject in GameObjects)
                baseObject.Draw(spriteBatch);
        }

        protected abstract ResourceManifest SetManifest();
    }
}
