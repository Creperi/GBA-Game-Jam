using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Barrel;
    public float spawnInterval = 5f;


    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        // Infinite loop to keep spawning objects
        while (true)
        {
            // Calculate the new rotation by adding 90 degrees to the current rotation on the x-axis
            Quaternion rotation = transform.rotation * Quaternion.Euler(90, 0, 0);

            // Spawn the object at the spawner's position and the new rotation
            Instantiate(Barrel, transform.position, rotation);

            // Wait for the spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
