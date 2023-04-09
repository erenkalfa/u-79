using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerFollowCM;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject mobileInputUI;
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Material[] playerMaterials;
    
    private PlayerMovementController movementController;
    private PlayerInputManager inputManager;
    private PhotonView pView;
    private Animator animator;
    
    private FinishManager finishManager;
    private Hashtable playerProperties;

    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        movementController = GetComponent<PlayerMovementController>();
        inputManager = GetComponent<PlayerInputManager>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        finishManager = GameManager.Instance.finishManager;
        usernameText.text = pView.Owner.NickName;
        
        if (!pView.IsMine) return;
        
        playerUI.SetActive(true);
        playerFollowCM.SetActive(true);
        
        mobileInputUI.SetActive(GameManager.Instance.isDeviceMobile);
        
        playerProperties = new Hashtable
        {
            { "skin", 0 }
        };
        
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        
    }

    private void Update()
    {
        if (!pView.IsMine) return;
        
        inputManager.CheckInputs();
        
        movementController.Move();
        movementController.GroundedCheck();
        movementController.JumpAndGravity();

        if (finishManager.isFinished)
        {
            animator.SetLayerWeight(1,Mathf.Lerp(animator.GetLayerWeight(1),1,Time.deltaTime * 5f));
        }
        
        SelectColor();
    }

    private void LateUpdate()
    {
        if (!pView.IsMine) return;
        
        movementController.CameraRotation();
    }

    private void SelectColor()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerProperties["skin"] = 0;
            playerRenderer.material = playerMaterials[(int) playerProperties["skin"]];
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerProperties["skin"] = 1;
            playerRenderer.material = playerMaterials[(int) playerProperties["skin"]];
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerProperties["skin"] = 2;
            playerRenderer.material = playerMaterials[(int) playerProperties["skin"]];
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerProperties["skin"] = 3;
            playerRenderer.material = playerMaterials[(int) playerProperties["skin"]];
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerProperties["skin"] = 4;
            playerRenderer.material = playerMaterials[(int) playerProperties["skin"]];
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!pView.IsMine && targetPlayer == pView.Owner)
        {
            playerRenderer.material = playerMaterials[(int) changedProps["skin"]];
        }
    }
}
