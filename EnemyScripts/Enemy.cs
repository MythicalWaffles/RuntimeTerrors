using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The parent enemy class that all ghost classes descend from.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    // The NavMeshAgent, accessible by children.
    protected NavMeshAgent agent;

    // A private boolean that tracks whether the enemy has been hit by a bullet.
    private bool destroyed;

    // Start is called before the first frame update.
    void Start()
    {
        // Create a new NavMeshAgent.
        agent = GetComponent<NavMeshAgent>();

        // Set the agent's speed.
        agent.speed = 10f;

        // The enemy is not destroyed to start with :)
        destroyed = false;
    }

    /// <summary>
    /// An abstract method that moves the enemy.
    /// Must be filled in by children classes.
    /// </summary>
    /// <param name="goal">Pac-Man's location.</param>
    public abstract void Move(Transform goal);

    /// <summary>
    /// Called by the bullet when the enemy is destroyed.
    /// </summary>
    public virtual void Destroy()
    {
        // Toggles the destroyed boolean to be true.
        destroyed = true;
    }

    /// <summary>
    /// Allows outside classes to detect whether the enemy has been destroyed.
    /// </summary>
    /// <returns>The private destroyed boolean.</returns>
    public virtual bool Destroyed()
    {
        // Returning the boolean in this way keeps control of the variable in this class.
        return destroyed;
    }

    /// <summary>
    /// Returns the amount of damage the enemy does when in range of the player.
    /// This is a virtual method, so it provides a default value that can be overridden by children.
    /// </summary>
    /// <returns></returns>
    public virtual float GetDamage()
    {
        // The default damage value is 0.25.
        return .25f;
    }
}
