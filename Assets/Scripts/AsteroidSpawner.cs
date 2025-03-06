using System;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnTarget;
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float spawnSpeedMagnitudeMin = 0f;
    [SerializeField] private float spawnSpeedMagnitudeMax = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ValidateForceMagnitudeRange();
        ContinuallySpawnAsteroids();
    }

    public void StopSpawningAsteroids()
    {
        CancelInvoke(nameof(SpawnAsteroid));
    }

    internal void Pause()
    {
        StopSpawningAsteroids();
        gameObject.SetActive(true);
    }

    public void Resume()
    {
        gameObject.SetActive(true);
        ContinuallySpawnAsteroids();
    }

    private void ContinuallySpawnAsteroids()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0, spawnRate);
    }

    private void ValidateForceMagnitudeRange()
    {
        if (spawnSpeedMagnitudeMax < spawnSpeedMagnitudeMin)
        {
            Debug.LogError("spawnSpeedMagnitudeMax must be greater than spawnSpeedMagnitudeMin");
            throw new System.ArgumentOutOfRangeException("spawnSpeedMagnitudeMax", "spawnSpeedMagnitudeMax must be greater than spawnSpeedMagnitudeMin");
        }
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
        Vector3 speedDirection = (spawnTarget.transform.position - asteroid.transform.position).normalized;
        float speedMagnitude = UnityEngine.Random.Range(spawnSpeedMagnitudeMin, spawnSpeedMagnitudeMax);
        Vector3 impulseVelocity = speedDirection * speedMagnitude;
        asteroid.GetComponent<Rigidbody>().linearVelocity = impulseVelocity;
    }
}
