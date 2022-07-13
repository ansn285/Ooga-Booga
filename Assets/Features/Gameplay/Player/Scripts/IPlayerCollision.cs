namespace Gameplay.Player
{
    public interface IPlayerCollision
    {
        public void CollidedWithNPC();
        public void CollidedWithEnemy();
        public void CollidedWithCollectible();
    }
}