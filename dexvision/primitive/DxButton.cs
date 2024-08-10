using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.dexvision.primitive
{
    public class DxButton : DxActionObject
    {
        public bool IsClickDown { get; set; }
        public Texture2D? HoverTexture { get; protected set; }
        public Texture2D? ClickDownTexture { get; protected set; }
        public DxStaticString? DxStaticString { get; set; }

        public DxButton(Rectangle rectangle, Texture2D mainTexture, DxObjectInfo parentInfo, Action action, Texture2D? hoverTexture = null, Texture2D? clickDownTexture = null) : base(rectangle, mainTexture, parentInfo, action)
        {
            HoverTexture = hoverTexture;
            ClickDownTexture = clickDownTexture;
        }

        public void SetText(GraphicsDevice device, SpriteFont font, string text, Color? color)
        {
            DxStaticString = new DxStaticString(device, font, text, Rectangle, color ?? Color.White);
        }

        public override void Update(InputManager input, float deltaMs)
        {
            base.Update(input, deltaMs);

            DxStaticString?.Update(input, deltaMs);

            IsClickDown = IsHovering && input.GetPressDuration(MouseButton.LeftButton) > 0f;
            if (IsHovering && input.IsSingleClick(MouseButton.LeftButton))
            {
                this.action();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Make sure to go down the tree of possibility here
            if (IsClickDown)
                spriteBatch.Draw(ClickDownTexture ?? MainTexture, Rectangle, Color.White);
            else if (IsHovering)
                spriteBatch.Draw(HoverTexture ?? MainTexture, Rectangle, Color.White);
            else
                spriteBatch.Draw(MainTexture, Rectangle, Color.White);

            DxStaticString?.Draw(spriteBatch);
        }
    }
}