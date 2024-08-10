using CoelacanthEngine.input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoelacanthEngine.dexvision.primitive
{
    public class DxStaticString
    {
        private SpriteFont _font;
        private string _text;
        private Rectangle _bounds;
        private RenderTarget2D _renderTarget;
        private GraphicsDevice _graphicsDevice;
        public Color Color { get; set; }
        private Vector2 _position;

        public DxStaticString(GraphicsDevice graphicsDevice, SpriteFont font, string text, Rectangle bounds, Color color)
        {
            _graphicsDevice = graphicsDevice;
            _font = font;
            _text = text;
            _bounds = bounds;
            Color = color;

            // Create a RenderTarget2D with the size of the bounds
            _renderTarget = new RenderTarget2D(
                _graphicsDevice,
                bounds.Width,
                bounds.Height
            );
            _position = new Vector2(bounds.X, bounds.Y);

            RenderTextToTexture();
        }

        private void RenderTextToTexture()
        {
            // Set the render target to our RenderTarget2D
            _graphicsDevice.SetRenderTarget(_renderTarget);
            _graphicsDevice.Clear(Color.Transparent);

            // Measure the size of the string to center it within the bounds
            Vector2 textSize = _font.MeasureString(_text);

            // Calculate the position to center the text within the render target
            Vector2 textPosition = new Vector2(
                (_renderTarget.Width - textSize.X) / 2,
                (_renderTarget.Height - textSize.Y) / 2
            );

            // Begin a new SpriteBatch to draw the text
            SpriteBatch spriteBatch = new SpriteBatch(_graphicsDevice);
            spriteBatch.Begin();

            // Draw the string onto the render target
            spriteBatch.DrawString(_font, _text, textPosition, Color.White);

            spriteBatch.End();

            // Reset the render target to the back buffer
            _graphicsDevice.SetRenderTarget(null);
        }

        public void Update(InputManager input, float deltaMs)
        {
            // No update by default
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the RenderTarget2D to the screen
            spriteBatch.Draw(_renderTarget, _position, Color);
        }

        public void Dispose()
        {
            // Clean up the RenderTarget2D when no longer needed
            _renderTarget.Dispose();
        }
    }
}
