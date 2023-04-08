using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SpoonNetworkController : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks,IPunObservable

{
    public bool isSpoonActive;
    private PhotonView spoonPhotonView;
    public GameObject spoonVirtualCamera;
    public GameObject spoonButton;
    private bool isDeviceMobile;
    
    private void Awake()
    {
        spoonPhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        isDeviceMobile = GameManager.Instance.isDeviceMobile;
    }

    private void Update()
    {
        if (isDeviceMobile) return;
        if (!spoonVirtualCamera.activeInHierarchy)return;
        if (!spoonPhotonView.IsMine) return;
        isSpoonActive = Input.GetKey(KeyCode.E);
    }
    
    public void SetIsSpoonActive(bool state)
    {
        isSpoonActive = state;
    }
    
    public void RequestOwnership( )
    {
        spoonPhotonView.RequestOwnership();
        spoonVirtualCamera.SetActive(true);
        spoonButton.SetActive(true);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != spoonPhotonView) return;
        
        Debug.Log("OnOwnershipRequest");
        spoonPhotonView.TransferOwnership(requestingPlayer);
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
            stream.SendNext(isSpoonActive);
        }
        else if (stream.IsReading)
        {
            isSpoonActive = (bool) stream.ReceiveNext();
        }
    }
}
