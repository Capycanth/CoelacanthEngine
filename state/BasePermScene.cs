using CoelacanthEngine.input;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.state
{
    public abstract class BasePermScene
    {
        public InputManager Input { get; protected set; }

        public BasePermScene()
        {
            Input = new InputManager(64);
        }

        public virtual void Update(float deltaMs)
        {
            Input.Update(deltaMs);
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        // Need to store all content in class
        public abstract void LoadContent();
    }
}
