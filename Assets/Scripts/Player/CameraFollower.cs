using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    GameObject camera;// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = camera.transform.position + offset;
        transform.rotation = Quaternion.AngleAxis(camera.transform.rotation.eulerAngles.y, Vector3.up);
    }
}
