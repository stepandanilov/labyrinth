using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    private static WinMenu instance;
    public GameObject WinMenuUI;

    private void Start()
    {
        instance = this;
    }
    public static WinMenu getInstance()
    {
        return instance;
    }
    public void Victory()
    {
        WinMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadSettingMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
