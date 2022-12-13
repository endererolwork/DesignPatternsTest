using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Script Shoud be placed on Camera.
    [SerializeField] private Transform target; // Target
    [SerializeField] private Vector3 offset; // Vector3 offset
    [SerializeField][Range(0, 1)] private float smooth = 0.125f; // Smoothing
    [SerializeField][Range(0, 10)] private float Speed = 10f; // Follow Speed

    private void FixedUpdate()
    {
        Vector3 vector = this.target.position + this.offset;
        Vector3 position = Vector3.Lerp(base.transform.position, vector, (this.smooth + this.Speed) * Time.deltaTime);
        base.transform.position = position;
    }
}
