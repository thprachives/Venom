using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign the player's Transform component to this field in the Inspector
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Adjust the offset as needed

    private void Update()
    {
        if (target != null)
        {
            // Calculate the desired camera position based on the player's position and offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate between the current camera position and the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
        }
    }
}


