using UnityEngine;

namespace Gameplay.Player
{
    public static class PlayerInputMapping
    {
        private static KeyCode _upKey = KeyCode.W;
        private static KeyCode _leftKey = KeyCode.A;
        private static KeyCode _downKey = KeyCode.S;
        private static KeyCode _rightKey = KeyCode.D;

        #region Getter Methods

        public static KeyCode UpKey => _upKey;
        public static KeyCode DownKey => _downKey;
        public static KeyCode LeftKey => _leftKey;
        public static KeyCode RightKey => _rightKey;

        #endregion

        #region Public Methods

        public static void SetLeftKey(KeyCode newKey)
        {
            _leftKey = newKey;
        }

        public static void SetRighttKey(KeyCode newKey)
        {
            _rightKey = newKey;
        }

        public static void SetUpKey(KeyCode newKey)
        {
            _upKey = newKey;
        }

        public static void SetDownKey(KeyCode newKey)
        {
            _downKey = newKey;
        }

        #endregion
    }
}