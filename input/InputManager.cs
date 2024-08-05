namespace CoelacanthEngine.input
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;

    public class InputManager
    {
        public Point MousePoint { get; private set; }
        private Dictionary<Keys, float> _keyPressDurations;
        private Dictionary<MouseButton, float> _mouseButtonDurations;
        

        public InputManager()
        {
            _keyPressDurations = new Dictionary<Keys, float>();
            _mouseButtonDurations = new Dictionary<MouseButton, float>();
        }

        public void Update(float deltaMs)
        {
            // Update keyboard states
            KeyboardState keyboardState = Keyboard.GetState();
            UpdateKeyDurations(keyboardState, deltaMs);

            // Update mouse button states
            MouseState mouseState = Mouse.GetState();
            UpdateMouseButtonDurations(mouseState, deltaMs);
            MousePoint = mouseState.Position;
        }

        private void UpdateKeyDurations(KeyboardState keyboardState, float deltaTime)
        {
            foreach (Keys key in new List<Keys>(_keyPressDurations.Keys))
            {
                if (keyboardState.IsKeyUp(key))
                {
                    _keyPressDurations.Remove(key);
                }
            }

            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (_keyPressDurations.ContainsKey(key))
                {
                    _keyPressDurations[key] += deltaTime;
                }
                else
                {
                    _keyPressDurations[key] = deltaTime;
                }
            }
        }

        private void UpdateMouseButtonDurations(MouseState mouseState, float deltaTime)
        {
            foreach (MouseButton button in new List<MouseButton>(_mouseButtonDurations.Keys))
            {
                if (IsMouseButtonUp(mouseState, button))
                {
                    _mouseButtonDurations.Remove(button);
                }
            }

            foreach (MouseButton button in Enum.GetValues(typeof(MouseButton)))
            {
                if (IsMouseButtonDown(mouseState, button))
                {
                    if (_mouseButtonDurations.ContainsKey(button))
                    {
                        _mouseButtonDurations[button] += deltaTime;
                    }
                    else
                    {
                        _mouseButtonDurations[button] = deltaTime;
                    }
                }
            }
        }

        private bool IsMouseButtonDown(MouseState mouseState, MouseButton button)
        {
            return button switch
            {
                MouseButton.LeftButton => mouseState.LeftButton == ButtonState.Pressed,
                MouseButton.RightButton => mouseState.RightButton == ButtonState.Pressed,
                MouseButton.MiddleButton => mouseState.MiddleButton == ButtonState.Pressed,
                MouseButton.XButton1 => mouseState.XButton1 == ButtonState.Pressed,
                MouseButton.XButton2 => mouseState.XButton2 == ButtonState.Pressed,
                _ => false,
            };
        }

        private bool IsMouseButtonUp(MouseState mouseState, MouseButton button)
        {
            return button switch
            {
                MouseButton.LeftButton => mouseState.LeftButton == ButtonState.Released,
                MouseButton.RightButton => mouseState.RightButton == ButtonState.Released,
                MouseButton.MiddleButton => mouseState.MiddleButton == ButtonState.Released,
                MouseButton.XButton1 => mouseState.XButton1 == ButtonState.Released,
                MouseButton.XButton2 => mouseState.XButton2 == ButtonState.Released,
                _ => true,
            };
        }

        public ClickType GetKeyButtonClickType(Keys key)
        {
            return _keyPressDurations.TryGetValue(key, out float duration) ? DetermineClickType(duration) : ClickType.None;
        }

        public ClickType GetMouseButtonClickType(MouseButton button)
        {
            return _mouseButtonDurations.TryGetValue(button, out float duration) ? DetermineClickType(duration) : ClickType.None;
        }

        private ClickType DetermineClickType(float duration)
        {
            if (duration < 200)
            {
                return ClickType.SingleClick;
            }
            else
            {
                return ClickType.LongClick;
            }
        }

        public enum MouseButton
        {
            LeftButton,
            RightButton,
            MiddleButton,
            XButton1,
            XButton2
        }

        public enum ClickType
        {
            None,
            SingleClick,
            LongClick
        }
    }
}
