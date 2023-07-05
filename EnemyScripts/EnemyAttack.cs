using UnityEngine;

/// <summary>
/// Allows the enemy ghosts to attack the player when they are in range.
/// </summary>
public class EnemyAttack : MonoBehaviour
{
	// The amount of damage this ghost will do to the player.
    protected float damage;

	// Start is called before the first frame update.
	void Start()
    {
		// Grab the Enemy script off the parent game object.
		// The parent game object is the actual top-level ghost object.
		Enemy enemyScript = GetComponentInParent<Enemy>();

		// Grab how much damage the ghost does and store it in the local variable.
		// Doing things this way lets us define the damage in each of the Ghost classes.
		// This saves us from having to make 3 different Attack scripts or setting things up in the editor.
		damage = enemyScript.GetDamage();
    }

	/// <summary>
	/// This method runs whenever a trigger is active.
	/// </summary>
	/// <param name="other">The collider of the game object in the trigger.</param>
    private void OnTriggerStay(Collider other)
    {
		// If the other game object is named "Player", we found the player!
		if (other.gameObject.name == "Player")
		{
			// Grab the Player script off the player game object.
			Player player = other.gameObject.GetComponent<Player>();

			// Tell the Player script that it took damage.
			// Use the stored amount custom to this ghost.
			player.TakeDamage(damage);
		}
	}
}
