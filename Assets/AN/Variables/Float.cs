using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "Variables/Float")]
    public class Float : ScriptableObject
    {
        [SerializeField] protected float Value;
        [SerializeField] protected float DefaultValue;
        [SerializeField] protected bool ResetToDefaultOnPlay = true;

        private void OnEnable()
        {
            if (ResetToDefaultOnPlay)
            {
                Value = DefaultValue;
            }
        }

        public virtual float GetValue()
        {
            return Value;
        }

        public virtual void SetValue(float value)
        {
            Value = value;
        }

        public virtual void SetValue(Float value)
        {
            Value = value.Value;
        }

        public virtual void ApplyChange(float amount)
        {
            Value += amount;
        }

        public virtual void ApplyChange(Float amount)
        {
            Value += amount.Value;
        }

        public static implicit operator float(Float integer)
        {
            return integer.Value;
        }
    }
}