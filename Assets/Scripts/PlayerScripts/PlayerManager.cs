using Photon.Pun;
using TMPro;
using UnityEngine;

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
        playerRenderer.material = playerMaterials[PhotonNetwork.CurrentRoom.PlayerCount - 1];
        
        if (!pView.IsMine) return;
        
        playerUI.SetActive(true);
        playerFollowCM.SetActive(true);
        
        mobileInputUI.SetActive(GameManager.Instance.isDeviceMobile);
        
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
    }

    private void LateUpdate()
    {
        if (!pView.IsMine) return;
        
        movementController.CameraRotation();
    }
}
