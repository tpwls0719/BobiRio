using UnityEngine;
using System.Collections;

public class ObstacleZone : MonoBehaviour
{
    public GameObject[] obstacles;
    public float activeTime = 1.5f;

    private bool isRunning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Bobi") || collision.CompareTag("Rio")) && !isRunning)
        {
            StartCoroutine(ActivateObstacles());
        }
    }

    IEnumerator ActivateObstacles()
    {
        isRunning = true;

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(true);
        }

        yield return new WaitForSeconds(activeTime);

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }

        isRunning = false;
    }
}