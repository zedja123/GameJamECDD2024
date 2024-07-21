using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
