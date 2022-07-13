using System;
using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/Vector3 Persistent")]
    public class Vector3Persistent : ScriptableObject
    {
        [SerializeField] protected Vector3 Value;
        [SerializeField] protected Vector3 DefaultValue;
        [SerializeField] protected bool ResetToDefaultOnPlay = true;
        [SerializeField] protected string Key;

        [NonSerialized] protected readonly string XKEY = "PlayerPositionX";
        [NonSerialized] protected readonly string YKEY = "PlayerPositionY";
        [NonSerialized] protected readonly string ZKEY = "PlayerPositionZ";

        private void OnEnable()
        {
            Load();
        }

        public Vector3 GetValue()
        {
            return Value;
        }

        public Vector3 GetDefaultValue()
        {
            return DefaultValue;
        }

        public void SetValue(Vector3 value)
        {
            Value = value;
            Save();
        }

        [ContextMenu("Save")]
        protected virtual void Save()
        {
            PlayerPrefs.SetFloat(XKEY, Value.x);
            PlayerPrefs.SetFloat(YKEY, Value.y);
            PlayerPrefs.SetFloat(ZKEY, Value.z);
            PlayerPrefs.SetInt(Key, 1);
        }

        [ContextMenu("Load")]
        protected virtual void Load()
        {
            if (Key != string.Empty)
            {
                if (ResetToDefaultOnPlay)
                {
                    if (PlayerPrefs.HasKey(Key))
                    {
                        float xValue = PlayerPrefs.GetFloat(XKEY);
                        float yValue = PlayerPrefs.GetFloat(YKEY);
                        float zValue = PlayerPrefs.GetFloat(ZKEY);

                        Value = new Vector3(xValue, yValue, zValue);
                    }

                    else
                    {
                        Value = DefaultValue;
                    }
                }

                else
                {
                    Value = Vector3.zero;
                }
            }
        }
    }
}