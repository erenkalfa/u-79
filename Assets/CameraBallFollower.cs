using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBallFollower : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, ball.position + offset, ref velocity, smoothTime);
    }
}
