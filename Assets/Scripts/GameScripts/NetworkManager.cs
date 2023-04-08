using System;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks,IPunObservable
{
    private PhotonView networkPhotonView;
    [SerializeField] private HammerNetworkController hammerNetworkController;

    private void Awake()
    {
        networkPhotonView = GetComponent<PhotonView>();
    }

    public bool hammerState;

    private void FixedUpdate()
    {
        if (!networkPhotonView.IsMine) return;
        
        hammerState = hammerNetworkController.isHammerActive;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(hammerState);
        }
        else if (stream.IsReading)
        {
            hammerState = (bool) stream.ReceiveNext();
        }
    }
}
