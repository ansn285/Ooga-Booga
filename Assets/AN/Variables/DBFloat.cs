using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/Float Persistent")]
    public class DBFloat : Float
    {
        [SerializeField] protected string Key;

        private void OnEnable()
        {
            Load();
        }

        public override void SetValue(float value)
        {
            base.SetValue(value);
            Save();
        }

        public override void SetValue(Float value)
        {
            base.SetValue(value);
            Save();
        }

        public override void ApplyChange(float amount)
        {
            base.ApplyChange(amount);
            Save();
        }

        public override void ApplyChange(Float amount)
        {
            base.ApplyChange(amount);
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(Key, Value);
        }

        private void Load()
        {
            if (!string.IsNullOrEmpty(Key) && PlayerPrefs.HasKey(Key))
            {
                Value = PlayerPrefs.GetFloat(Key);
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