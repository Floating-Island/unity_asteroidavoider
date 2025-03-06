using System;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CorrectOffScreenPlayer();
    }

    private void CorrectOffScreenPlayer()
    {
        Vector3 playerViewportPosition = mainCamera.WorldToViewportPoint(player.transform.position);

        if (playerViewportPosition.x > 1 || playerViewportPosition.x < 0)
        {
            playerViewportPosition.x = Math.Clamp(1 - playerViewportPosition.x, 0, 1);
            player.transform.position = mainCamera.ViewportToWorldPoint(playerViewportPosition);
        }

        if (playerViewportPosition.y > 1 || playerViewportPosition.y < 0)
        {
            playerViewportPosition.y = Math.Clamp(1 - playerViewportPosition.y, 0, 1);
            player.transform.position = mainCamera.ViewportToWorldPoint(playerViewportPosition);
        }
    }
}
