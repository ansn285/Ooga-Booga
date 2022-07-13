using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/String Persistent")]
    public class DBString : String
    {
        [SerializeField] protected string Key;

        private void OnEnable()
        {
            Load();
        }

        public override void SetValue(string value)
        {
            base.SetValue(value);
            Save();
        }

        public override void SetValue(String value)
        {
            base.SetValue(value);
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetString(Key, Value);
        }
        
        private void Load()
        {
            if (!string.IsNullOrEmpty(Key) && PlayerPrefs.HasKey(Key))
            {
                Value = PlayerPrefs.GetString(Key);
            }
            else
            {
                if (ResetToDefaultOnPlay)
                {
                    Value = DefaultValue;
                }
                else
                {
                    Value = string.Empty;
                }
            }
        }

    }
}