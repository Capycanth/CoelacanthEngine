using CoelacanthEngine.audio;
using CoelacanthEngine.cache;
using CoelacanthEngine.dexvision;
using CoelacanthEngine.input;
using CoelacanthEngine.model;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.state
{
    public abstract class BaseScene
    {
        public List<DxObject> DxObjects { get; protected set; }
        public List<BaseObject> GameObjects { get; protected set; }
        public InputManager Input { get; protected set; }
        public AudioManager Audio { get; protected set; }
        public ResourceManifest Manifest { get; protected set; }

        public BaseScene()
        {
            Manifest = SetManifest();
        }

        protected virtual void Initialize()
        {
            Input = new InputManager(64);
            SetDxObjects();
            SetGameObjects();
            Audio = new AudioManager(Manifest.Songs);
            Audio.Play();
        }

        public async Task LoadContentAsync()
        {
            await ResourceCache.Instance.LoadResourcesAsync(Manifest);
            Initialize();
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
        protected abstract List<DxObject> SetDxObjects();
        protected abstract List<BaseObject> SetGameObjects();
    }
}
