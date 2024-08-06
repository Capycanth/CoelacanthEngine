using Microsoft.Xna.Framework;

namespace CoelacanthEngine.dexvision.format
{
    public interface IFormat
    {
        public void Apply(ref Rectangle child, DxObjectInfo parent);
    }
}
