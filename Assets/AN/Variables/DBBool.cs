using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/Bool Persistent")]
    public class DBBool : Bool
    {
        [SerializeField] protected string Key;

        private void OnEnable()
        {
            Load();
        }

        public override void SetValue(bool value)
        {
            base.SetValue(value);
            Save();
        }

        public override void SetValue(Bool value)
        {
            base.SetValue(value);
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetInt(Key, Value ? 1 : 0);
        }

        private void Load()
        {
            if (!string.IsNullOrEmpty(Key) && PlayerPrefs.HasKey(Key))
            {
                Value = PlayerPrefs.GetInt(Key) == 1 ? true : false;
            }

            else
            {
                if (ResetToDefaultOnPlay)
                {
                    Value = DefaultValue;
                }
                else
                {
                    Value = false;
                }
            }
        }
    }
}