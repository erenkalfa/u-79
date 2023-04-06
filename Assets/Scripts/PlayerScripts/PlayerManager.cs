using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerFollowCM;
    
    private PlayerMovementController movementController;
    private PlayerInputManager inputManager;
    private PhotonView pView;

    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        movementController = GetComponent<PlayerMovementController>();
        inputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        if (!pView.IsMine) return;
        
        playerFollowCM.SetActive(true);
    }

    private void Update()
    {
        if (!pView.IsMine) return;
        
        inputManager.CheckInputs();
        
        movementController.Move();
        movementController.GroundedCheck();
        movementController.JumpAndGravity();
    }

    private void LateUpdate()
    {
        if (!pView.IsMine) return;
        
        movementController.CameraRotation();
    }
}
