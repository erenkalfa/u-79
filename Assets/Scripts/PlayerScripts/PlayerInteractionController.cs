using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public GameObject hammerButton;
    public GameObject leaveButton;
    
    private PhotonView interactionPhotonView;
    private HammerNetworkController hammerNetworkController;

    private void Awake()
    {
        interactionPhotonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!interactionPhotonView.IsMine) return;
        
        if (other.CompareTag("Hammer"))
        {
            hammerButton.SetActive(true);
            hammerNetworkController = other.GetComponent<HammerNetworkController>();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!interactionPhotonView.IsMine) return;
        
        if (other.CompareTag("Hammer"))
        {
            leaveButton.SetActive(false);
            hammerButton.SetActive(false);
            LeaveHammer();
        }
    }
    
    public void RequestHammerOwnership()
    {
        hammerNetworkController.RequestOwnership();
        leaveButton.SetActive(true);
    }

    public void LeaveHammer()
    {
        if (hammerNetworkController)
        {
            hammerNetworkController.hammerVirtualCamera.SetActive(false);
            hammerNetworkController = null;
        }
    }

}
