using UnityEngine;

namespace Root.BorderCrosser
{
    /// <summary>
    /// Interface for checking if a position is within the boundaries of the game.
    /// </summary>
    public interface IBorderCrosser
    {
        /// <summary>
        /// Gets the game boundaries as an array of corner positions.
        /// </summary>
        Vector3[] Boundaries { get; }

        /// <summary>
        /// Checks if a position is within the game boundaries and returns the corrected position if not.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>The corrected position if it was outside the boundaries, otherwise the original position.</returns>
        Vector2 BoundariesCheck(Vector2 position);
    }
}