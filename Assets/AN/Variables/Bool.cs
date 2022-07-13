using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/Bool")]
    public class Bool : ScriptableObject
    {
        [SerializeField] protected bool Value;
        [SerializeField] protected bool DefaultValue;
        [SerializeField] protected bool ResetToDefaultOnPlay = true;

        private void OnEnable()
        {
            if (ResetToDefaultOnPlay)
            {
                Value = DefaultValue;
            }
        }

        public virtual bool GetValue()
        {
            return Value;
        }

        public virtual void SetValue(bool value)
        {
            Value = value;
        }

        public virtual void SetValue(Bool value)
        {
            Value = value.Value;
        }

        public static implicit operator bool(Bool boolean)
        {
            return boolean.Value;
        }
    }
}