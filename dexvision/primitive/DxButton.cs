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
        public DxButton(Rectangle rectangle, Texture2D mainTexture, Texture2D? hoverTexture, Texture2D? clickDownTexture, DxObjectInfo parentInfo, Action action) : base(rectangle, mainTexture, parentInfo, action)
        {
            HoverTexture = hoverTexture;
            ClickDownTexture = clickDownTexture;
        }

        public override void Update(InputManager input, float deltaMs)
        {
            base.Update(input, deltaMs);
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
        }
    }
}