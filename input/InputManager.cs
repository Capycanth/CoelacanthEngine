using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace CoelacanthEngine.input
{

    public sealed class InputManager
    {
        public Point MousePoint { get; private set; }
        public int ScrollWheelValue { get; private set; }

        private Dictionary<Keys, float> _keyPressDurations;
        private Dictionary<Keys, ClickState> _keyClickStates;
        private Dictionary<MouseButton, float> _mousePressDurations;
        private Dictionary<MouseButton, ClickState> _mouseClickStates;

        private const float LONG_PRESS_DURATION = 350f;

        public InputManager(int keyInputs)
        {
            _keyPressDurations = new Dictionary<Keys, float>(keyInputs);
            _keyClickStates = new Dictionary<Keys, ClickState>(keyInputs);
            _mousePressDurations = new Dictionary<MouseButton, float>(5);
            _mouseClickStates = new Dictionary<MouseButton, ClickState>(5);
        }

        public ClickState GetClickState(Keys key)
        {
            return _keyClickStates.TryGetValue(key, out ClickState state) ? state : ClickState.None;
        }

        public ClickState GetClickState(MouseButton mouseButton)
        {
            return _mouseClickStates.TryGetValue(mouseButton, out ClickState state) ? state : ClickState.None;
        }

        public bool IsSingleClick(Keys key)
        {
            return _keyClickStates.TryGetValue(key, out ClickState state) && state == ClickState.Single;
        }

        public bool IsSingleClick(MouseButton mouseButton)
        {
            return _mouseClickStates.TryGetValue(mouseButton, out ClickState state) && state == ClickState.Single;
        }

        public bool IsLongClick(Keys key)
        {
            return _keyClickStates.TryGetValue(key, out ClickState state) && state == ClickState.Long;
        }

        public bool IsLongClick(MouseButton mouseButton)
        {
            return _mouseClickStates.TryGetValue(mouseButton, out ClickState state) && state == ClickState.Long;
        }

        public float GetPressDuration(Keys key)
        {
            return _keyPressDurations.TryGetValue(key, out float duration) ? duration : 0f;
        }

        public float GetPressDuration(MouseButton mouseButton)
        {
            return _mousePressDurations.TryGetValue(mouseButton, out float duration) ? duration : 0f;
        }

        public void Update(float deltaMs)
        {
            FlushClickStates();

            MouseState currMouseState = Mouse.GetState();
            KeyboardState currKeyboardState = Keyboard.GetState();

            //region Mouse Update
            MousePoint = currMouseState.Position;
            ScrollWheelValue = currMouseState.ScrollWheelValue;

            UpdateMouseButtonInfo(MouseButton.LeftButton, currMouseState.LeftButton, deltaMs);
            UpdateMouseButtonInfo(MouseButton.RightButton, currMouseState.RightButton, deltaMs);
            UpdateMouseButtonInfo(MouseButton.MiddleButton, currMouseState.MiddleButton, deltaMs);
            UpdateMouseButtonInfo(MouseButton.XButton1, currMouseState.XButton1, deltaMs);
            UpdateMouseButtonInfo(MouseButton.XButton2, currMouseState.XButton2, deltaMs);
            //endregion

            //region Keyboard Update
            Keys[] pressedKeys = currKeyboardState.GetPressedKeys();
            foreach(Keys key in _keyPressDurations.Keys.ToList())
            {
                if(pressedKeys.Contains(key))
                    // Was pressed before and is pressed now
                    _keyPressDurations[key] += deltaMs;     
                else
                {
                    // Was pressed before and is not pressed now
                    _keyClickStates.Add(key, _keyPressDurations[key] > LONG_PRESS_DURATION ? ClickState.Long : ClickState.Single);
                    _keyPressDurations.Remove(key);
                }
            }
            foreach(Keys key in pressedKeys.Except(_keyPressDurations.Keys).ToList())
                // Was not pressed before and is pressed now
                _keyPressDurations.Add(key, deltaMs);
            //endregion
        }

        private void UpdateMouseButtonInfo(MouseButton mouseButton, ButtonState state, float deltaMs)
        {
            if (state == ButtonState.Pressed)
            {
                if (_mousePressDurations.ContainsKey(mouseButton))
                    _mousePressDurations[mouseButton] += deltaMs;   // Is pressed now and was pressed before
                else
                    _mousePressDurations.Add(mouseButton, deltaMs); // Is pressed now and was not pressed before
            }
            else
            {
                if (_mousePressDurations.ContainsKey(mouseButton))
                {
                    // Is not pressed now but was pressed before
                    _mouseClickStates.Add(mouseButton, _mousePressDurations[mouseButton] > LONG_PRESS_DURATION ? ClickState.Long : ClickState.Single);
                    _mousePressDurations.Remove(mouseButton);
                }
                // Is not pressed now and was not pressed before: Do Nothing
            }
        }

        private void FlushClickStates()
        {
            _keyClickStates.Clear();
            _mouseClickStates.Clear();
        }
    }

    public enum MouseButton
    {
        LeftButton,
        RightButton,
        MiddleButton,
        XButton1,
        XButton2,
    }

    public enum ClickState
    {
        None,
        Single,
        Long,
    }
}
