using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BallController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Vector3 startPos;
    
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        transform.position = startPos;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
