using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject spawnPoint;
    /// <summary>
    /// Only collides with the player.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<DamageableEntity>().SetCheckpoint(spawnPoint);
        }
    }
}
