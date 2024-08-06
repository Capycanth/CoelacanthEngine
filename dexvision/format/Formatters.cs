using Microsoft.Xna.Framework;

namespace CoelacanthEngine.dexvision.format
{
    public class CenterVertically : IFormat
    {
        public void Apply(ref Rectangle child, DxObjectInfo parent)
        {
            child.Y = parent.BaseY + ((parent.Height - parent.Height) >> 1);
        }
    }

    public class CenterHorizontally : IFormat
    {
        public void Apply(ref Rectangle child, DxObjectInfo parent)
        {
            child.X = parent.BaseX + ((parent.Width - parent.Width) >> 1);
        }
    }

    public class ShiftFormatter : IFormat
    {
        private readonly float _xShiftPercentage;
        private readonly float _yShiftPercentage;

        public ShiftFormatter(float xShift, float yShift)
        {
            _xShiftPercentage = xShift;
            _yShiftPercentage = yShift;
        }

        public void Apply(ref Rectangle child, DxObjectInfo parent)
        {
            child.X += (int)(parent.Width * _xShiftPercentage);
            child.Y += (int)(parent.Height * _yShiftPercentage);
        }
    }
}
