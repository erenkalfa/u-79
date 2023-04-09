using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject mainCamera;
    public NetworkManager networkManager;
    public bool isDeviceMobile;
    public FinishManager finishManager;
    
    private void Awake()
    {
        Instance = this;
    }
}
