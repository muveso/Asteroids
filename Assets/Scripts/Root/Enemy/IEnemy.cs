using Root.Player;

namespace Root.Enemy
{
    public interface IEnemy : IDamageable
    {
        void Despawn();
    }
}