using System;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField] private GameObject ball;
    private PhotonView networkPhotonView;
    [SerializeField] private HammerNetworkController hammerNetworkController;
    [SerializeField] private FlipperNetworkController flipperNetworkController;
    //[SerializeField] private FlipperNetworkController spoonNetworkController;
    public bool[] puzzleStates;
    
    private Transform ballStartTransform;

    private void Awake()
    {
        networkPhotonView = GetComponent<PhotonView>();
        ballStartTransform = ball.transform;
    }
    // 0 hammer, 1 flipper, 2 spoon

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ball.transform.position = ballStartTransform.position;
            ball.transform.rotation = ballStartTransform.rotation;
        }
    }


    private void FixedUpdate()
    {
        if (!networkPhotonView.IsMine) return;
        
        puzzleStates[0] = hammerNetworkController.isHammerActive;
        puzzleStates[1] = flipperNetworkController.isFlipperActive;
        //puzzleStates[2] = spoonNetworkController.isFlipperActive;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(puzzleStates[0]);
            stream.SendNext(puzzleStates[1]);
            //stream.SendNext(puzzleStates[2]);
        }
        else if (stream.IsReading)
        {
            puzzleStates[0] = (bool) stream.ReceiveNext();
            puzzleStates[1] = (bool) stream.ReceiveNext();
            //puzzleStates[2] = (bool) stream.ReceiveNext();
        }
    }
}
