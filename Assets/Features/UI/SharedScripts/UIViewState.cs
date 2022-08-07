using System;
using AN.StateMachine;
using UnityEngine;

namespace UI.Views
{
    public class UIViewState : State
    {
        [SerializeField] public UIViewConfig UIViewConfig;
        
        [SerializeField] protected string ViewName;
        
        [NonSerialized] protected UIView _view;
        [NonSerialized] protected bool? hidden = null;

        public override void Init(IState listener)
        {
            base.Init(listener);
            DestroyView();

            _view = Instantiate(Resources.Load<UIView>(ViewName));
            _view.Init(this);
        }

        public override void Execute()
        {
            base.Execute();
            _view.Show();
        }

        public override void Pause(bool hideView)
        {
            base.Pause(hideView);

            if (hideView)
            {
                _view.Hide();
            }

            hidden = true;
        }

        public override void Resume()
        {
            base.Resume();

            if (hidden.HasValue && hidden.Value)
            {
                _view.Show();
            }
        }

        public override void Exit()
        {
            if (_view != null)
            {
                _view.Hide();
                DestroyView();
            }

            base.Exit();
        }

        public override void Cleanup()
        {
            DestroyView();
            base.Cleanup();
        }

        protected virtual void DestroyView()
        {
            if (_view != null)
            {
                Destroy(_view.gameObject, UIViewConfig.ViewOutDuration);
            }

            _view = null;
        }
    }
}