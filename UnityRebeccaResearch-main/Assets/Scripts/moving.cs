using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform[] startMarker;
    public Transform[] endMarker;

    public int startIndex;
    public int endIndex;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        //startIndex = Random.Range(0, startMarker.Length);
        //endIndex = Random.Range(0, endMarker.Length);
        //initalize random point on the size of thin
        // Calculate the journey length.
        //journeyLength = Vector3.Distance(startMarker[startIndex].position, endMarker[endIndex].position);
    }

    // Move to the target end position.
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker[startIndex].position, endMarker[endIndex].position, fractionOfJourney);
        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }

        if (transform.position == endMarker[endIndex].position)
        {
            startIndex = endIndex;
            endIndex = Random.Range(0, endMarker.Length);
            transform.position = startMarker[startIndex].position;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker[startIndex].position, endMarker[endIndex].position);
        }
    }
}