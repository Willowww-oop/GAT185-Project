using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        // Checking if its the player
        if (other.CompareTag("Player")) 
        {
            // Respawning the player
            other.transform.position = new Vector3(-1, 1, -141);
            --gameManager.lives;
        }

        else
        {
            // Destroys other objects that fall into the death plane
            Destroy(other.gameObject);
        }
    }
}
