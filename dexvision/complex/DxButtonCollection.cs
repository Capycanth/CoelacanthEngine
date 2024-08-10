using CoelacanthEngine.dexvision.format;
using CoelacanthEngine.dexvision.primitive;
using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.dexvision.complex
{
    public class DxButtonCollection
    {
        private List<DxButton> buttons;
        private ButtonCollectionDirection direction;

        public DxButtonCollection(GraphicsDevice graphics, ButtonCollectionDirection dir, int width, int height, int gap, params ButtonManifest[] manifests) 
        { 
            direction = dir;
            buttons = new List<DxButton>(manifests.Length);

            for(int i = 0; i < manifests.Length; i++)
            {
                DxButton button = new DxButton(
                        new Rectangle(
                            DetermineX(width, gap, i),
                            DetermineY(height, gap, i),
                            width,
                            height
                            ),
                        manifests[i].MainTexture,
                        manifests[i].Parent,
                        manifests[i].Action,
                        manifests[i].HoverTexture,
                        manifests[i].ClickDownTexture
                    );
                button.SetText(graphics, manifests[i].Font, manifests[i].Text, manifests[i].TextColor);
                buttons.Add(button);    
            }
        }

        private int DetermineX(int width, int gap, int i)
        {
            if (direction == ButtonCollectionDirection.HORIZONTAL)
                return i * (width + gap);
            else
                return 0;
        }

        private int DetermineY(int height, int gap, int i)
        {
            if (direction == ButtonCollectionDirection.VERTICAL)
                return i * (height + gap);
            else
                return 0;
        }

        // Currently does not handle Center Vertically/Horizontally
        public void ApplyFormat(ShiftFormatter format)
        {
            foreach(DxButton button in buttons)
            {
                button.ApplyFormatter(format);
            }
        }

        public void Update(InputManager input, float deltaMs)
        {
            foreach(DxButton button in buttons)
            {
                button.Update(input, deltaMs);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(DxButton button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }

    public enum ButtonCollectionDirection : byte
    {
        VERTICAL =      0b00000000,
        HORIZONTAL =    0b00000001,
    }

    public class ButtonManifest
    {
        public Action Action { get; private set; }
        public string Text { get; private set; }
        public Texture2D MainTexture { get; private set; }
        public Texture2D? HoverTexture { get; private set; }
        public Texture2D? ClickDownTexture { get; private set; }
        public DxObjectInfo Parent { get; private set; }
        public SpriteFont Font { get; private set; }
        public Color TextColor { get; private set; }

        public ButtonManifest(Texture2D mainTexture, DxObjectInfo parentInfo, Action action, string text, SpriteFont font, Color color, Texture2D? hoverTexture = null, Texture2D? clickDownTexture = null)
        {
            Action = action;
            Text = text;
            MainTexture = mainTexture;
            Parent = parentInfo;
            Font = font;
            HoverTexture = hoverTexture;
            ClickDownTexture = clickDownTexture;
            TextColor = color;
        }
    }
}
