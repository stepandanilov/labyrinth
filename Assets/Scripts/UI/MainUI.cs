using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    private static MainUI instance;
    public GameObject WinMenuUI;
    public GameObject LoseMenuUI;
    public int countdownTime;
    private int countdownTextFontSize = 500;
    public GameObject countdownDisplay;
    public TMPro.TMP_Text countdownText;

    public GameController gameController;
    private void Start()
    {
        instance = this;

        StartCoroutine(countdownToStart());     
    }
    public static MainUI getInstance()
    {
        return instance;
    }
    public void FixedUpdate()
    {
        if (countdownDisplay.activeSelf) countdownText.fontSize -= 1f;
    }
    IEnumerator countdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            countdownText.fontSize = countdownTextFontSize;

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownText.fontSize = countdownTextFontSize;

        countdownText.text = "GO";

        yield return new WaitForSeconds(1f);

        countdownDisplay.SetActive(false);

        gameController.StartGame();
    }
    public void Victory()
    {
        WinMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Lose()
    {
        LoseMenuUI.SetActive(true);
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
