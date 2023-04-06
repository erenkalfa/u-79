using UnityEngine;
using TMPro;

public class UsernameController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_Text menuUsername;

    public void CheckUsername()
    {
        if (PlayerPrefs.HasKey("Username"))
        {
            if (!menuPanel.activeInHierarchy)
            {
                menuPanel.SetActive(true);
            }
            if (registerPanel.activeInHierarchy)
            {
                registerPanel.SetActive(false);
            }
        }
        else
        {
            if (menuPanel.activeInHierarchy)
            {
                menuPanel.SetActive(false);
            }
            if (!registerPanel.activeInHierarchy)
            {
                registerPanel.SetActive(true);
            }
        }
        menuUsername.text = PlayerPrefs.GetString("Username");
    }

    public void SaveUserName()
    {
        if (usernameInputField.text == "") return;
        PlayerPrefs.SetString("Username",usernameInputField.text);
        PlayerPrefs.SetFloat("aimSensitivity", 3f);
        PlayerPrefs.SetFloat("normalSensitivity", 6f);
        CheckUsername();
    }

    
    
}