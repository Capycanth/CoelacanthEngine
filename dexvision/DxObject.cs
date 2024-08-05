using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.dexvision
{
    public abstract class DxObject
    {
        public Rectangle Rectangle { get; protected set; }
        public Texture2D MainTexture { get; protected set; }
        public DxObjectInfo Info { get; protected set; }
        protected bool IsHovering { get; set; }

        protected DxObject(Rectangle rectangle, Texture2D mainTexture)
        {
            Rectangle = rectangle;
            MainTexture = mainTexture;
            Info = new DxObjectInfo(rectangle);
            IsHovering = false;
        }

        protected void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MainTexture, Rectangle, Color.White);
        }

        protected virtual void Update(InputManager input, float deltaMs)
        {
            IsHovering = Rectangle.Contains(input.MousePoint);
        }
    }
}
