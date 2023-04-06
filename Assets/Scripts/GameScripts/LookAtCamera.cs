using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
    }

    private void FixedUpdate()
    {
        transform.LookAt(mainCamera.transform);
    }
}