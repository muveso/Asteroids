using Root.Settings;
using UnityEngine;

namespace Root.BorderCrosser
{
    /// <summary>
    /// Class for checking if a position is within the boundaries of the game.
    /// </summary>
    public class BorderCrosser : IBorderCrosser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorderCrosser"/> class.
        /// </summary>
        /// <param name="boundarySettings">The boundary settings.</param>
        public BorderCrosser(BoundarySettings boundarySettings)
        {
            boundarySettings.Initialize();
            Boundaries = boundarySettings.CornerPosition;
        }

        /// <summary>
        /// Checks if a position is within the game boundaries and returns the corrected position if not.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>The corrected position if it was outside the boundaries, otherwise the original position.</returns>
        public Vector2 BoundariesCheck(Vector2 position)
        {
            if (position.x < Boundaries[0].x || position.x > Boundaries[2].x)
                position.x = position.x < Boundaries[0].x ? Boundaries[2].x : Boundaries[0].x;

            if (position.y < Boundaries[0].y || position.y > Boundaries[1].y)
                position.y = position.y < Boundaries[0].y ? Boundaries[1].y : Boundaries[0].y;

            return position;
        }

        /// <summary>
        /// Gets the game boundaries as an array of corner positions.
        /// </summary>
        public Vector3[] Boundaries { get; }
    }
}