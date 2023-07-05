using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// The controller for Blinky, the "shadow" ghost.
/// Its attacks are the strongest but have the shortest range.
/// </summary>
public class Blinky : Enemy
{
    /// <summary>
    /// Blinky moves directly to Pac-Man's location.
    /// </summary>
    /// <param name="goal">Pac-Man's current location.</param>
    public override void Move(Transform goal)
    {
//        public Transform other;
//        float dist = Vector3.Distance(other.position, transform.position);
        if ((goal.position - agent.destination).magnitude < 20)
        {
            /*// Create a new random number generator.
            System.Random rng = new System.Random();

            // Create a new vector with random numbers in the x and z directions.
            Vector3 rand = new Vector3(rng.Next(5), 0, rng.Next(5));*/

            // Add the random number vector to Pac-Man's location and move to this position.
            agent.destination = goal.position;
        }
    }

    /// <summary>
    /// This method is used to determine how much damage to do to Pac-Man.
    /// </summary>
    /// <returns>The amount of damage to do per frame to Pac-Man.</returns>
    public override float GetDamage()
    {
        // Blinky does 0.5 damage, the most of the ghosts.
        return .5f;
    }
}
