namespace Gameplay.NPC
{
    public interface INPCInteraction
    {
        void StartedInteraction(Player.Player player);
        void EndedInteraction(Player.Player player);
    }
}