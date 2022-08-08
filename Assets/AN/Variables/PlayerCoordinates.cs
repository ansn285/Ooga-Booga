using System;
using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/Vector3 Persistent")]
    public class PlayerCoordinates : ScriptableObject
    {
        [SerializeField] protected Vector3 Position;
        [SerializeField] protected Vector3 DefaultPosition;
        
        [SerializeField] protected Vector3 Rotation;
        [SerializeField] protected Vector3 DefaultRotation;
        
        [SerializeField] protected bool ResetToDefaultOnPlay = true;
        [SerializeField] protected string Key;

        [NonSerialized] protected readonly string XKEY = "PlayerPositionX";
        [NonSerialized] protected readonly string YKEY = "PlayerPositionY";
        [NonSerialized] protected readonly string ZKEY = "PlayerPositionZ";
        
        [NonSerialized] protected readonly string XROTKEY = "PlayerRotationX";
        [NonSerialized] protected readonly string YROTKEY = "PlayerRotationY";
        [NonSerialized] protected readonly string ZROTKEY = "PlayerRotationZ";

        private void OnEnable()
        {
            Load();
        }

        public Vector3 GetPosition()
        {
            return Position;
        }

        public Vector3 GetRotation()
        {
            return Rotation;
        }

        public (Vector3, Vector3) GetValueSet()
        {
            return (Position, Rotation);
        }

        public Vector3 GetDefaultPosition()
        {
            return DefaultPosition;
        }
        
        public Vector3 GetDefaultRotation()
        {
            return DefaultRotation;
        }

        public void SetValue(Vector3 position)
        {
            Position = position;
            Save();
        }

        public void SetValue(Vector3 position, Vector3 rotation)
        {
            SetValue(position);
            Rotation = rotation;
            Save();
        }

        [ContextMenu("Save")]
        protected virtual void Save()
        {
            PlayerPrefs.SetFloat(XKEY, Position.x);
            PlayerPrefs.SetFloat(YKEY, Position.y);
            PlayerPrefs.SetFloat(ZKEY, Position.z);
            
            PlayerPrefs.SetFloat(XROTKEY, Rotation.x);
            PlayerPrefs.SetFloat(YROTKEY, Rotation.y);
            PlayerPrefs.SetFloat(ZROTKEY, Rotation.z);
            
            PlayerPrefs.SetInt(Key, 1);
        }

        [ContextMenu("Load")]
        public virtual void Load()
        {
            if (Key != string.Empty)
            {
                if (ResetToDefaultOnPlay)
                {
                    if (PlayerPrefs.HasKey(Key))
                    {
                        float xPosition = PlayerPrefs.GetFloat(XKEY);
                        float yPosition = PlayerPrefs.GetFloat(YKEY);
                        float zPosition = PlayerPrefs.GetFloat(ZKEY);
                        
                        float xRotation = PlayerPrefs.GetFloat(XROTKEY);
                        float yRotation = PlayerPrefs.GetFloat(YROTKEY);
                        float zRotation = PlayerPrefs.GetFloat(ZROTKEY);

                        Position = new Vector3(xPosition, yPosition, zPosition);
                        Rotation = new Vector3(xRotation, yRotation, zRotation);
                    }

                    else
                    {
                        Position = DefaultPosition;
                        Rotation = DefaultRotation;
                    }
                }

                else
                {
                    Position = Vector3.zero;
                    Rotation = Vector3.zero;
                }
            }
            
            if (GetPosition().y < -50f)
            {
                ResetPlayerPosition();
            }
        }
        
        public void ResetPlayerPosition()
        {
            Vector3 newPosition = Position;
            newPosition.x = (int)newPosition.x;
            newPosition.y = DefaultPosition.y;
            newPosition.z = (int)newPosition.z;
            SetValue(newPosition);
        }
    }
}