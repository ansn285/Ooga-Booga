using System;
using System.Collections;
using AN.StateMachine;
using UnityEngine;

namespace UI.Views
{
    public class NewUIViewState : NewState
    {
        [SerializeField] public UIViewConfig UIViewConfig;
        
        [SerializeField] protected string ViewName;
        
        [NonSerialized] protected NewUIView _view;
        [NonSerialized] protected bool? hidden = null;

        public override void Init(INewState listener)
        {
            base.Init(listener);
            DestroyView();

            _view = Instantiate(Resources.Load<NewUIView>(ViewName));
            _view.UIViewState = this;
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