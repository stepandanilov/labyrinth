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
    public GameObject countdownDisplay;
    public TMPro.TMP_Text countdownText;
    private int countdownTextFontSize = 500;

    public GameController gameController;
    private void Start()
    {
        instance = this;

        StartCoroutine(CountdownToStart());     
    }
    public static MainUI GetInstance()
    {
        return instance;
    }
    public void FixedUpdate()
    {
        if (countdownDisplay.activeSelf) countdownText.fontSize -= 1f;
    }
    private IEnumerator CountdownToStart()
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
