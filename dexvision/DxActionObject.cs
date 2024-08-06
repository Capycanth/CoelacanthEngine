using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.dexvision
{
    public abstract class DxActionObject : DxObject
    {
        protected readonly Action action;
        protected DxActionObject(Rectangle rectangle, Texture2D mainTexture, DxObjectInfo parentInfo, Action action) : base(rectangle, mainTexture, parentInfo)
        {
            this.action = action;
        }
    }
}
