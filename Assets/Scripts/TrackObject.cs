using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public Transform target;
    public float trackingSpeed = 10.0f;
    public Vector3 offset;

    private void Update()
    {
        if (!target)
            return;

        // Need to discard the Z axis
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = target.position + offset;

        Vector3 difference = targetPosition - currentPosition;

        transform.position += difference * (trackingSpeed * Time.deltaTime);

    }
}
