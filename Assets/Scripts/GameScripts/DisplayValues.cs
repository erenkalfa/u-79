using System;
using System.Collections;
using TMPro;
using Photon.Pun;
using UnityEngine;

public class DisplayValues : MonoBehaviour
{
    [SerializeField] private TMP_Text pingTMPText;
    [SerializeField] private TMP_Text fpsTMPText;

    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }

    private void Start()
    {
        StartCoroutine(CallRepeatedly(1, SetFPSText));
        if (PhotonNetwork.IsConnected)
        {
            StartCoroutine(CallRepeatedly(1, SetPingText));
        }
    }

    private void Update()
    {
        HandleFPSCounter();
    }

    private void SetFPSText()
    {
        fpsTMPText.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    private void SetPingText()
    {
        pingTMPText.text = PhotonNetwork.GetPing().ToString();
    }

    private void HandleFPSCounter()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
    }
    
    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }

    private void OnConnectedToServer()
    {
        StartCoroutine(CallRepeatedly(1, SetPingText));
    }

    IEnumerator CallRepeatedly(float waitDuration, Action action)
    {
        while (true)
        {
            action();
            yield return new WaitForSeconds(waitDuration);
        }
    }

}
