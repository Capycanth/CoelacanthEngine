using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.dexvision
{
    public abstract class DxActionObject : DxObject
    {
        private readonly Action action;
        protected DxActionObject(Rectangle rectangle, Texture2D mainTexture, Action action) : base(rectangle, mainTexture)
        {
            this.action = action;
        }

        protected override void Update(InputManager input, float deltaMs)
        {
            base.Update(input, deltaMs);
            if (IsHovering && input.GetMouseButtonClickType(InputManager.MouseButton.LeftButton) == InputManager.ClickType.SingleClick)
            {
                this.action();
            }
        }
    }
}
