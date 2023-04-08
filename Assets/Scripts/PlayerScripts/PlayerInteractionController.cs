using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private GameObject hammerButton;
    [SerializeField] private GameObject flipperButton;
    [SerializeField] private GameObject leaveButton;
    
    private PhotonView interactionPhotonView;
    private HammerNetworkController hammerNetworkController;
    private FlipperNetworkController flipperNetworkController;
    
    private bool isDeviceMobile;
    

    private void Awake()
    {
        interactionPhotonView = GetComponent<PhotonView>();
        isDeviceMobile = GameManager.Instance.isDeviceMobile;
    }

    private void Update()
    {
        if (!interactionPhotonView.IsMine) return;
        if (isDeviceMobile) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LeaveHammer();
            LeaveFlipper();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hammerNetworkController)
            {
                RequestHammerOwnership();
            }

            if (flipperNetworkController)
            {
                RequestFlipperOwnership();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!interactionPhotonView.IsMine) return;
        
        if (other.CompareTag("Hammer"))
        {
            hammerButton.SetActive(true);
            hammerNetworkController = other.GetComponent<HammerNetworkController>();
        }
        else if (other.CompareTag("Flipper"))
        {
            flipperButton.SetActive(true);
            flipperNetworkController = other.GetComponent<FlipperNetworkController>();
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!interactionPhotonView.IsMine) return;
        
        if (other.CompareTag("Hammer"))
        {
            LeaveHammer();
        }
        else if (other.CompareTag("Flipper"))
        {
            LeaveFlipper();
        }
    }
    
    public void RequestFlipperOwnership()
    {
        flipperNetworkController.RequestOwnership();
        flipperButton.SetActive(false);
        leaveButton.SetActive(true);
    }
    
    public void RequestHammerOwnership()
    {
        hammerNetworkController.RequestOwnership();
        hammerButton.SetActive(false);
        leaveButton.SetActive(true);
    }

    public void ExitPuzzlePiece()
    {
        LeaveHammer();
        LeaveFlipper();
    }

    public void LeaveHammer()
    {
        if (hammerNetworkController)
        {
            hammerNetworkController.hammerVirtualCamera.SetActive(false);
            hammerNetworkController.hammerButton.SetActive(false);
            leaveButton.SetActive(false);
            hammerButton.SetActive(false);
            hammerNetworkController = null;
        }
    }
    
    public void LeaveFlipper()
    {
        if (flipperNetworkController)
        {
            leaveButton.SetActive(false);
            flipperButton.SetActive(false);
            flipperNetworkController.flipperVirtualCamera.SetActive(false);
            flipperNetworkController.flipperButton.SetActive(false);
            flipperNetworkController = null;
        }
    }
}
