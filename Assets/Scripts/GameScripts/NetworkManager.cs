using System;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks,IPunObservable
{
    private PhotonView networkPhotonView;
    [SerializeField] private HammerNetworkController hammerNetworkController;
    [SerializeField] private FlipperNetworkController flipperNetworkController;
    [SerializeField] private FlipperNetworkController spoonNetworkController;
    [SerializeField] private FlipperNetworkController spoon2NetworkController;
    public bool[] puzzleStates;
    

    private void Awake()
    {
        networkPhotonView = GetComponent<PhotonView>();
    }
    // 0 hammer, 1 flipper, 2 spoon, 3 spoom
    
    private void FixedUpdate()
    {
        if (!networkPhotonView.IsMine) return;
        
        puzzleStates[0] = hammerNetworkController.isHammerActive;
        puzzleStates[1] = flipperNetworkController.isFlipperActive;
        puzzleStates[2] = spoonNetworkController.isFlipperActive;
        puzzleStates[3] = spoon2NetworkController.isFlipperActive;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(puzzleStates[0]);
            stream.SendNext(puzzleStates[1]);
            stream.SendNext(puzzleStates[2]);
            stream.SendNext(puzzleStates[3]);
        }
        else if (stream.IsReading)
        {
            puzzleStates[0] = (bool) stream.ReceiveNext();
            puzzleStates[1] = (bool) stream.ReceiveNext();
            puzzleStates[2] = (bool) stream.ReceiveNext();
            puzzleStates[3] = (bool) stream.ReceiveNext();
        }
    }
}
