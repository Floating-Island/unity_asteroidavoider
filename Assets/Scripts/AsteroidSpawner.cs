using System;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnTarget;
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float spawnForceMagnitudeMin = 0f;
    [SerializeField] private float spawnForceMagnitudeMax = 1f;

    private float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ValidateForceMagnitudeRange();
        InvokeRepeating(nameof(SpawnAsteroid), 0, spawnRate);
    }

    private void ValidateForceMagnitudeRange()
    {
        if (spawnForceMagnitudeMax < spawnForceMagnitudeMin)
        {
            Debug.LogError("spawnForceMagnitudeMax must be greater than spawnForceMagnitudeMin");
            throw new System.ArgumentOutOfRangeException("spawnForceMagnitudeMax", "spawnForceMagnitudeMax must be greater than spawnForceMagnitudeMin");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnAsteroid()
    {
        Vector3 spawnLocation = FindSpawnLocation();

        GameObject asteroid = SpawnAsteroid(spawnLocation);
        ImpulseAsteroid(asteroid);
    }

    private Vector3 FindSpawnLocation()
    {
        float sideX = UnityEngine.Random.Range(0f, 1f);
        float sideY = UnityEngine.Random.Range(0f, 1f);
        Vector2 viewportSpawnLocation = new Vector2(sideX, sideY);
        Vector3 worldSpawnLocation = Camera.main.ViewportToWorldPoint(viewportSpawnLocation);
        worldSpawnLocation.z = spawnTarget.transform.position.z;
        return worldSpawnLocation;
    }

    private GameObject SpawnAsteroid(Vector3 spawnLocation)
    {
        int asteroidPrefabIndex = UnityEngine.Random.Range(0, asteroidPrefabs.Length);
        GameObject asteroidPrefab = asteroidPrefabs[asteroidPrefabIndex];
        Quaternion asteroidRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
        GameObject asteroid = Instantiate(asteroidPrefab, spawnLocation, asteroidRotation);
        return asteroid;
    }

    private void ImpulseAsteroid(GameObject asteroid)
    {
        Vector3 forceDirection = (spawnTarget.transform.position - asteroid.transform.position).normalized;
        float forceMagnitude = UnityEngine.Random.Range(spawnForceMagnitudeMin, spawnForceMagnitudeMax);
        Vector3 force = forceDirection * forceMagnitude;
        asteroid.GetComponent<Rigidbody>().linearVelocity = force;
    }
}
