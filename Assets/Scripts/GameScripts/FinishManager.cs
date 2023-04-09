using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private GameObject finishEffects;
    [SerializeField] private GameObject gameCanvas;
    public bool isFinished;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            finishEffects.SetActive(true);
            gameCanvas.SetActive(false);
            isFinished = true;
        }
    }
}
