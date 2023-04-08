using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class FlipperNetworkController : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks,IPunObservable

{
    public bool isFlipperActive;
    private PhotonView flipperPhotonView;
    public GameObject flipperVirtualCamera;
    public GameObject flipperButton;
    private bool isDeviceMobile;
    
    private void Awake()
    {
        flipperPhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        isDeviceMobile = GameManager.Instance.isDeviceMobile;
    }

    private void Update()
    {
        if (isDeviceMobile) return;
        if (!flipperVirtualCamera.activeInHierarchy)return;
        if (!flipperPhotonView.IsMine) return;
        isFlipperActive = Input.GetKey(KeyCode.E);
    }
    
    public void SetIsFlipperActive(bool state)
    {
        isFlipperActive = state;
    }
    
    public void RequestOwnership( )
    {
        flipperPhotonView.RequestOwnership();
        flipperVirtualCamera.SetActive(true);
        flipperButton.SetActive(true);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != flipperPhotonView) return;
        
        Debug.Log("OnOwnershipRequest");
        flipperPhotonView.TransferOwnership(requestingPlayer);
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
            stream.SendNext(isFlipperActive);
        }
        else if (stream.IsReading)
        {
            isFlipperActive = (bool) stream.ReceiveNext();
        }
    }
}
