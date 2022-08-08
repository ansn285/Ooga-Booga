using System.Collections.Generic;
using UnityEngine;

namespace UI.InteractionPanel
{
    public class InteractionPanelPooler
    {
        private static List<InteractionPanel> _pool;
        private static InteractionPanel _panel;
        private static InteractionPanel _panelInUse;

        private static void Init()
        {
            if (_pool == null)
            {
                _pool = new List<InteractionPanel>();
            }

            if (_panel == null)
            {
                _panel = Resources.Load<InteractionPanel>("InteractionPanel");
            }
        }
        
        public static void ShowPanel(Vector3 position)
        {
            Init();
            if (_pool.Count > 0)
            {
                _panelInUse = _pool[0];
                _pool.RemoveAt(0);
            }

            else
            {
                _panelInUse = MonoBehaviour.Instantiate(_panel);
            }
            
            _panelInUse.Init(position);
        }

        public static void ClosePanel()
        {
            _panelInUse.HidePanel();
        }

        public static void AddToPool(InteractionPanel panel)
        {
            _pool.Add(panel);
        }
    }
}