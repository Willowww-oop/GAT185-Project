using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    public float timer = 1.5f;
    
    void Update()
    {
        timer -= Time.deltaTime; ;
        if (timer <= 0)
        {
            SpawnObject();
            timer = 1.5f;
        }
    }

    public void SpawnObject()
    {
        Instantiate(gameObject, transform.position, Quaternion.identity);
    }
}
