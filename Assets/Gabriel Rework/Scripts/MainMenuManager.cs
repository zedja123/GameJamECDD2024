using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public GameObject creditosImage;
    public GameObject tutorialObject;
    public GameObject voltarMenuObject;


    private void Start()
    {
        Cursor.visible = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenOptions()
    {
        // SceneManager.LoadScene("OptionsScene");
    }

    public void Creditos()
    {
        creditosImage.SetActive(true);
        voltarMenuObject.SetActive(true);

    }

    public void voltarMenu()
    {
        creditosImage.SetActive(false);
        tutorialObject.SetActive(false);
        voltarMenuObject.SetActive(false);


    }


    public void tutorial()
    {
        tutorialObject.SetActive(true);
        voltarMenuObject.SetActive(true);


    }


}
