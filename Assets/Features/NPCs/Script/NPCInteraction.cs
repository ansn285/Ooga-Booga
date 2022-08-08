using UnityEngine;

using AN.Events;
using UI.InteractionPanel;

namespace Gameplay.NPC 
{
    public class NPCInteraction : MonoBehaviour, INPCInteraction
    {
        [SerializeField] private GameEvent NPCCollisionEntered;
        [SerializeField] private GameEvent NPCCollisionExit;
        [SerializeField] private Animator InteractionButtonAnimator;

        #region INPCInteraction Implementation

        void INPCInteraction.StartedInteraction(Player.Player player)
        {
            HideInteractionButton();
            StartInteractionBullshit();
        }

        void INPCInteraction.EndedInteraction(Player.Player player)
        {
            EndInteractionBullshit();
            ShowInteractionButton();
        }
        
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                ShowInteractionButton();
                NPCCollisionEntered.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                HideInteractionButton();
                NPCCollisionExit.Invoke();
            }
        }

        private void ShowInteractionButton()
        {
            InteractionPanelPooler.ShowPanel(transform.position);
        }

        private void HideInteractionButton()
        {
            InteractionPanelPooler.ClosePanel();
        }

        private void StartInteractionBullshit()
        {
            
        }

        private void EndInteractionBullshit()
        {
            
        }
    }
}