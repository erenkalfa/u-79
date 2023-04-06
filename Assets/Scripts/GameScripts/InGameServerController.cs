using UnityEngine;
using Photon.Pun;

public class InGameServerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform startTransform;

    private void Start()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(prefab.name,startTransform.position,Quaternion.identity);
    }

}