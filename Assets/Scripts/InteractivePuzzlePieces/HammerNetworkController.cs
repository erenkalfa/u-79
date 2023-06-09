
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HammerNetworkController : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks,IPunObservable
{
    public bool isHammerActive;
    private PhotonView hammerPhotonView;
    public GameObject hammerVirtualCamera;
    public GameObject hammerButton;
    private bool isDeviceMobile;
    
    private void Awake()
    {
        hammerPhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        isDeviceMobile = GameManager.Instance.isDeviceMobile;
    }

    private void Update()
    {
        if (isDeviceMobile) return;
        if (!hammerVirtualCamera.activeInHierarchy)return;
        if (!hammerPhotonView.IsMine) return;
        isHammerActive = Input.GetKey(KeyCode.E);
    }
    
    public void SetIsHammerActive(bool state)
    {
        isHammerActive = state;
    }
    
    public void RequestOwnership( )
    {
        hammerPhotonView.RequestOwnership();
        hammerVirtualCamera.SetActive(true);
        hammerButton.SetActive(true);
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
