using UnityEngine;

namespace Gameplay.Player
{
    public static class SpawnPlayer
    {
        private static Player _player;
        
        public static void Init(Player playerPrefab, Vector3 position)
        {
            if (_player == null)
            {
                _player = MonoBehaviour.Instantiate(playerPrefab, position, Quaternion.identity);
            }
        }

        public static void Init(Player playerPrefab, Vector3 position, Vector3 rotation)
        {
            if (_player == null)
            {
                _player = MonoBehaviour.Instantiate(playerPrefab, position, Quaternion.Euler(rotation));
            }
        }

        public static Player GetPlayer()
        {
            return _player;
        }
    }
}