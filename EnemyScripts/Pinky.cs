using UnityEngine;

/// <summary>
/// The controller for Pinky, the strategic ghost.
/// Its attacks are the weakest but have the greatest range.
/// </summary>
public class Pinky : Enemy
{
    /// <summary>
    /// Pinky moves to a position 10 units in front of Pac-Man's location.
    /// </summary>
    /// <param name="goal">Pac-Man's current location.</param>
    public override void Move(Transform goal)
    {
        if ((goal.position - agent.destination).magnitude < 20)
        {
            // Set the destination of the agent to be 10 units in front of Pac-Man's current position.
            agent.destination = goal.position + (10 * goal.transform.forward);
        }
    }

    /// <summary>
    /// This method is used to determine how much damage to do to Pac-Man.
    /// </summary>
    /// <returns>The amount of damage to do per frame to Pac-Man.</returns>
    public override float GetDamage()
    {
        // Pinky does 0.1 damage, the least of the ghosts.
        return .1f;
    }
}
