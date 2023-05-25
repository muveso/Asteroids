using System;
using Root.Enemy;
using UnityEngine;

namespace Root.Player
{
    public class PhysicsCollision : MonoBehaviour, IDamageable
    {
        public Action OnCollision = () => { };

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IEnemy enemy)) return;
            enemy.Despawn();
            OnCollision.Invoke();
        }

        public void GetDamage()
        {
            OnCollision.Invoke();
        }
    }
}