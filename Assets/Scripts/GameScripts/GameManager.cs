using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject mainCamera;
    public NetworkManager networkManager;
    
    private void Awake()
    {
        Instance = this;
    }
}
