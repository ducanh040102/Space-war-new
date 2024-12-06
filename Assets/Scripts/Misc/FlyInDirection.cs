using System.Collections;
using UnityEngine;

public class FlyInDirection : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float trackingSpeed = 2.0f; // Speed at which the enemy tracks the player
    public float waitTime = 2.0f; // Time to wait before moving towards the player
    private bool isWaiting = true;
    private bool isTracking = true;
    private Vector3 direction;

    void Start()
    {
        StartCoroutine(TrackPlayer());
    }

    private void Update()
    { 
       if (!isTracking)
        {
            transform.position += direction * Time.deltaTime * trackingSpeed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, trackingSpeed * Time.deltaTime);
        }
    }

    IEnumerator TrackPlayer()
    {
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        //StartCoroutine(StopTrackPlayer());
        while (isTracking)
        {
            if (player != null && !isWaiting)
            {
                direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, trackingSpeed * Time.deltaTime);

                //transform.position = Vector2.MoveTowards(transform.position, player.position, trackingSpeed * Time.deltaTime);
            }
            yield return null;
        }
    }

    IEnumerator StopTrackPlayer()
    {
        yield return new WaitForSeconds(5);
        isTracking = false;
    }
}
