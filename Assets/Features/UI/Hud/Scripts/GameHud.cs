using System;
using System.Collections;

using AN.StateMachine;
using UnityEngine;

namespace UI.Views
{
    public class GameHud : MonoBehaviour
    {
        [NonSerialized] public GameState GameState;

        [SerializeField] private GameHudHeader Header;
        [SerializeField] private GameHudFooter Footer;

        public IEnumerator Show()
        {
            yield return Header.Show();
            yield return Footer.Show();
        }

        public IEnumerator Hide()
        {
            yield return Header.Hide();
            yield return Footer.Hide();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameState.OpenSettings();
            }
        }
    }
}