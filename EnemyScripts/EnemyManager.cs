using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages enemy spawns and behavior.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    // A link to the player's game object, linked in the editor.
    public GameObject player;

    // A list of the enemy prefabs, linked in the editor.
    public List<GameObject> enemyPrefabs;

    // The distance from the edge things will spawn.
    public float buffer = 10f;

    // A list of the scripts attached to enemies currently active in the scene.
    private List<Enemy> enemyScripts;
    private int score = 0;
	public Text ScoreCount;

    // Start is called before the first frame update.
    void Start()
    {
        // Initialize the enemy script list.
        enemyScripts = new List<Enemy>();

        // Start the game by spawning one of each type of enemy.
        SpawnEachEnemy();
    }

    /// <summary>
    /// Spawns one of each type of enemy at random places around the map.
    /// </summary>
    private void SpawnEachEnemy()
    {
        // Loop through each enemy prefab.
        foreach (GameObject enemy in enemyPrefabs)
        {
            // Choose a random x and z position on the map, within the bounds of the buffer zone.
            Vector3 position = new Vector3(Random.Range(buffer, 200f - buffer), 0.5f, Random.Range(buffer, 200f - buffer));

            // Instantiate the current prefab at that position and save the game object.
            GameObject go = Instantiate(enemy, position, Quaternion.identity);

            // Pull the Enemy script off the newly instantiated game object.
            enemyScripts.Add(go.GetComponentInChildren<Enemy>());

            // Set the parent of the newly instantiated game object to be the manager.
            // This just keeps the hierarchy tidy.
            go.transform.parent = gameObject.transform;
        }
    }

    /// <summary>
    /// Spawns a random number of random enemy types at random places around the map.
    /// </summary>
    private void SpawnRandomEnemies()
    {
        // Choose a number of enemies to spawn, between 1 and 3.
        int numberOfEnemies = Random.Range(1, 3);

        // Loop for each enemy.
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Choose a random x and z position on the map, within the bounds of the buffer zone.
            Vector3 position = new Vector3(Random.Range(buffer, 200f - buffer), 0.5f, Random.Range(buffer, 200f - buffer));

            // Randomly choose one of the enemy prefabs to instantiate at the generated position and save the game object.
            GameObject go = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], position, Quaternion.identity);

            // Pull the Enemy script off the newly instantiated game object.
            enemyScripts.Add(go.GetComponentInChildren<Enemy>());

            // Set the parent of the newly instantiated game object to be the manager.
            // This just keeps the hierarchy tidy.
            go.transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame.
    void Update()
    {
        // When enemies are deleted from the hierarchy after a bullet collision, their scripts remain as null objects in the script list.
        // We want to remove these null objects, but C# doesn't allow lists to be modified while looping over the list.
        // We will get around this by creating a new list and adding each of the still existing scripts to it.
        // Then replacing the old list that includes null objects with the new list.
        List<Enemy> newScripts = new List<Enemy>();

        // Loop through each script in the list of enemy scripts.
        foreach(Enemy enemy in enemyScripts)
        {
            // If the script object is not null, its game object is still in the scene.
            if (enemy != null)
            {
                // Tell the enemy to perform its movement behavior.
                enemy.Move(player.transform);

                // Add the current script to the new script list.
                newScripts.Add(enemy);
            }
        }

        // Now we are going to compare the size of the two lists to see if any enemies have been hit by bullets.
        // If so, we will spawn new enemies.
        // The problem is, we are also going to replace the enemy script list with the updated one.
        // So we have to use a boolean to remember whether we have to spawn new enemies after we swap the lists.
        bool spawnEnemies = false;

        // If there are more objects in our original list compared to the new one...
        // It means we have taken null objects out, so an enemy has been removed from the scene.
        if (enemyScripts.Count > newScripts.Count)
        {
            // Since an enemy has been shot, let's spawn more enemies.
            spawnEnemies = true;
        }

        // Swap the new list with no null objects for the old list.
        enemyScripts = newScripts;

        // If an enemy was removed from the scene...
        if (spawnEnemies)
        {
            // Spawn new enemies.
            score++;
            ScoreCount.text = score.ToString();
            SpawnRandomEnemies();
        }
    }
}
