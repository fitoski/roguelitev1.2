using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private Transform mainCamera;
    public float topBound = 50f;
    public float bottomBound = -30f;
    public float leftBound = -32f;
    public float rightBound = 32f;


    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (transform.position.z > mainCamera.position.z + topBound ||
            transform.position.z < mainCamera.position.z + bottomBound ||
            transform.position.x < mainCamera.position.x + leftBound ||
            transform.position.x > mainCamera.position.x + rightBound)
        {
            Destroy(gameObject);
        }
    }
}
