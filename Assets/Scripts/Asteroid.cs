using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Crash();
        }
    }
    private void Update()
    {
        DestroyOnOffScreen();
    }

    private void DestroyOnOffScreen()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x > 1 || viewportPosition.x < 0 || viewportPosition.y > 1 || viewportPosition.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
