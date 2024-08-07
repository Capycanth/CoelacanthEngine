using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.model
{
    public abstract class BaseObject
    {
        public string Name { get; set; }
        public Rectangle Rectangle { get; protected set; }
        public Texture2D Texture { get; protected set; }

        public BaseObject(string name, Rectangle rectangle, Texture2D texture)
        {
            Name = name;
            Rectangle = rectangle;
            Texture = texture;
        }

        public abstract void Update(InputManager input, float deltaMs);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
