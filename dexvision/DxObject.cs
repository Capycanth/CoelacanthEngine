using CoelacanthEngine.dexvision.format;
using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.dexvision
{
    public abstract class DxObject
    {
        public Rectangle Rectangle { get; set; }
        public Texture2D MainTexture { get; protected set; }
        public DxObjectInfo Info { get; protected set; }
        protected bool IsHovering { get; set; }
        protected DxObjectInfo ParentInfo { get; set; }

        public DxObject(Rectangle rectangle, Texture2D mainTexture, DxObjectInfo parentInfo)
        {
            Rectangle = rectangle;
            MainTexture = mainTexture;
            Info = new DxObjectInfo(rectangle);
            IsHovering = false;
            ParentInfo = parentInfo;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MainTexture, Rectangle, Color.White);
        }

        public virtual void Update(InputManager input, float deltaMs)
        {
            IsHovering = Rectangle.Contains(input.MousePoint);
        }

        public void ApplyFormatter(IFormat formatter)
        {
            Rectangle rec = Rectangle;
            formatter.Apply(ref rec, ParentInfo);
            Rectangle = rec;
        }
    }
}
