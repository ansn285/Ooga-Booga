using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/Int Persistent")]
    public class DBInt : Int
    {
        [SerializeField] protected string Key;

        private void OnEnable()
        {
            Load();
        }

        public override void SetValue(int value)
        {
            base.SetValue(value);
            Save();
        }

        public override void SetValue(Int value)
        {
            base.SetValue(value);
            Save();
        }

        public override void ApplyChange(int amount)
        {
            base.ApplyChange(amount);
            Save();
        }

        public override void ApplyChange(Int amount)
        {
            base.ApplyChange(amount);
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetInt(Key, Value);
        }

        private void Load()
        {
            if (!string.IsNullOrEmpty(Key) && PlayerPrefs.HasKey(Key))
            {
                Value = PlayerPrefs.GetInt(Key);
            }
            else
            {
                if (ResetToDefaultOnPlay)
                {
                    Value = DefaultValue;
                }
                else
                {
                    Value = 0;
                }
            }
        }
    }
}