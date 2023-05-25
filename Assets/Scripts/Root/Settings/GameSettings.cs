using System;
using System.Collections.Generic;
using Root.Enemy;
using Root.Utils;
using UnityEngine;

namespace Root.Settings
{
    [Serializable]
    public class GameSettings
    {
        [field: SerializeField]
        [field: Header("Ship Settings")]
        public float ShipAcceleration { get; private set; }

        [field: SerializeField] public float ShipMaxSpeed { get; private set; }

        [field: SerializeField] public float ShipDeceleration { get; private set; }

        [field: SerializeField] public float ShipAngleVelocity { get; private set; }

        [field: SerializeField] public int BulletsPerSecond { get; private set; }

        [field: SerializeField]
        [field: Header("Asteroids Settings")]
        public int AsteroidsSplitAngle { get; private set; }

        [field: SerializeField] public int AsteroidsStartCount { get; private set; }

        [field: SerializeField]
        [field: Header("UFO Settings")]
        public int MinTimeSpawnUfo { get; private set; }

        [field: SerializeField] public int MaxTimeSpawnUfo { get; private set; }

        [field: SerializeField] public float MinShootDelayUfo { get; private set; }

        [field: SerializeField] public float MaxShootDelayUfo { get; private set; }

        [field: SerializeField]
        [field: Header("Score Settings")]
        public List<Pair<EnemyType, int>> ScoreList { get; private set; }
    }
}