using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HammerNetworkController : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks,IPunObservable
{
    public bool isHammerActive;
    private PhotonView hammerPhotonView;
    public GameObject hammerVirtualCamera;

    private void Awake()
    {
        hammerPhotonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!hammerPhotonView.IsMine) return;
        
        isHammerActive = Input.GetKey(KeyCode.X);
    }
    
    public void RequestOwnership( )
    {
        hammerPhotonView.RequestOwnership();
        hammerVirtualCamera.SetActive(true);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != hammerPhotonView) return;
        
        Debug.Log("OnOwnershipRequest");
        hammerPhotonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("OnOwnershipTransfered");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        Debug.Log("OnOwnershipTransferFailed");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isHammerActive);
        }
        else if (stream.IsReading)
        {
            isHammerActive = (bool) stream.ReceiveNext();
        }
    }
}
