using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    protected float damage;
    protected float speed;

    private Rigidbody2D projectileRigidbody;
    private Collider2D projectileCollider;

    // A list of the enemy prefabs, linked in the editor.
    public List<GameObject> projectilePrefabs;

    private List<Projectiles> projectileScripts;

    // Start is called before the first frame update
    void Start()
    {
        projectileScripts = new List<Projectiles>();

        projectileCollider = gameObject.GetComponent<Collider2D>();

        projectileRigidbody = gameObject.GetComponent<Rigidbody2D>();

        damage = GetDamage();
        speed = GetSpeed();
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		// If the other game object is named "Player", we found the player!
		if (other.gameObject.name == "Dummy")
		{
			// Grab the Player script off the player game object.
			DummyCode dummy = other.gameObject.GetComponent<DummyCode>();

			// Tell the Player script that it took damage.
			// Use the stored amount custom to this ghost.
			dummy.TakeDamage(damage);
            Destroy(gameObject);
		}
	}

    public virtual float GetDamage()
    {
        // The default damage value is 0.25.
        return 10f;
    }
    public virtual float GetSpeed()
    {
        // The default damage value is 0.25.
        return 10f;
    }
}
