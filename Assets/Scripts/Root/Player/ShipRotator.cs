using Root.Settings;
using UnityEngine;

namespace Root.Player
{
    public class ShipRotator
    {
        private readonly Transform _ship;

        public readonly float AngleVelocity;

        public ShipRotator(GameSettings gameSettings, Transform player)
        {
            AngleVelocity = gameSettings.ShipAngleVelocity;
            _ship = player;
        }

        public void Rotate(float direction)
        {
            direction = Mathf.Clamp(direction, -AngleVelocity, AngleVelocity);
            _ship.Rotate(Vector3.forward * (-direction * Mathf.Rad2Deg * Time.deltaTime));
        }
    }
}