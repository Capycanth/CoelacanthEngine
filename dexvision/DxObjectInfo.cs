using Microsoft.Xna.Framework;

namespace CoelacanthEngine.dexvision
{
    public class DxObjectInfo
    {
        public int BaseX { get; private set; }
        public int BaseY { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public DxObjectInfo(Rectangle rec)
        {
            BaseX = rec.X;
            BaseY = rec.Y;
            Width = rec.Width;
            Height = rec.Height;
        }
    }
}
