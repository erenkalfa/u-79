using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ServerController : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    public bool isPlayerDisconnect;

    [SerializeField] private TMP_Text pingTMPText;
    [SerializeField] private TMP_Text serverStatusTMPText;

    private void Start()
    {
        GetComponent<UsernameController>().CheckUsername();
        PhotonNetwork.ConnectUsingSettings();
        serverStatusTMPText.text = "connecting to server";
    }

    private void Update()
    {
        DisplayPing();
    }
    
    #region Methods

    private void DisplayPing()
    {
        pingTMPText.text = PhotonNetwork.GetPing() + " ms";
    }

    public void JoinRandomRoom(int sceneIndex)
    {
        if (!PhotonNetwork.InLobby) return;
        PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
        PhotonNetwork.LoadLevel(sceneIndex);
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
        isPlayerDisconnect = true;
    }
    #endregion
    

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (isPlayerDisconnect)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            serverStatusTMPText.text = "disconnected";
            PhotonNetwork.ReconnectAndRejoin();
            serverStatusTMPText.text = "trying to reconnected";
        }
    }

    public override void OnConnectedToMaster()
    {
        serverStatusTMPText.text = "connected to server";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        serverStatusTMPText.text = "ready to play";
    }
    
    public void ClearPlayerData()
    {
        PlayerPrefs.DeleteAll();
        Disconnect();
    }
}