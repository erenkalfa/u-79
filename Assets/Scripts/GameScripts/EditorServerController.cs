using Photon.Pun;

public class EditorServerController : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
}