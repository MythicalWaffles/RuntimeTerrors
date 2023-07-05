using UnityEngine;

/// <summary>
/// The controller for Inky, the "patrolling" ghost.
/// It uses the default attack strength.
/// </summary>
public class Inky : Enemy
{
    /// <summary>
    /// Inky patrols around Pac-Man's location.
    /// </summary>
    /// <param name="goal">Pac-Man's current location.</param>
    public override void Move(Transform goal)
    {
        if ((goal.position - agent.destination).magnitude < 20)
        {
            // If Inky is very close to Pac-Man or has stopped moving...
            if ((transform.position - agent.destination).magnitude < 2 || agent.velocity.magnitude == 0f)
            {
                // Create a new random number generator.
                System.Random rng = new System.Random();

                // Create a new vector with random numbers in the x and z directions.
                Vector3 rand = new Vector3(rng.Next(5), 0, rng.Next(5));

                // Add the random number vector to Pac-Man's location and move to this position.
                agent.destination = goal.position + rand;
            }
        }
    }
}
