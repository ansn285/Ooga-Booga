using System;
using System.Collections;

using UnityEngine;

using AN.StateMachine;

namespace UI.Views
{
    [CreateAssetMenu(fileName = "UIViewState", menuName = "State Machine/States/UI View State")]
    public class UIViewState : State
    {
        [SerializeField] protected string ViewName;

        [NonSerialized] protected UIView _view;
        [NonSerialized] protected bool? hidden = null;

        public override IEnumerator Init(IState listener)
        {
            yield return base.Init(listener);
            DestroyView();

            _view = Instantiate(Resources.Load<UIView>(ViewName));
            _view.UIViewState = this;

            yield return new WaitUntil(() => _view != null);
        }

        public override IEnumerator Execute()
        {
            yield return base.Execute();
            yield return _view.Show();
        }

        public override IEnumerator Pause(bool hideView)
        {
            yield return base.Pause(hideView);

            if (hideView)
            {
                _view.Hide();
            }

            hidden = true;
        }

        public override IEnumerator Resume()
        {
            yield return base.Resume();

            if (hidden.HasValue && hidden.Value)
            {
                _view.Show();
            }
        }

        public override IEnumerator Exit()
        {
            if (_view != null)
            {
                yield return _view.Hide();
                DestroyView();
            }

            yield return base.Exit();
        }

        public override IEnumerator Cleanup()
        {
            DestroyView();
            yield return base.Cleanup();
        }

        protected virtual void DestroyView()
        {
            if (_view != null)
            {
                Destroy(_view.gameObject);
            }

            _view = null;
        }
    }
}