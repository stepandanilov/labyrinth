using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MazeSettings : MonoBehaviour
{
    //delta settings
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    //gamma settings
    public TMP_InputField lengthInput;
    //theta settings
    public TMP_InputField radiusInput;
    public TMP_Dropdown dropdown;
    public TMP_Dropdown diffDropdown;

    public TMP_Text widthPlaceholder;
    public TMP_Text heightPlaceholder;
    public TMP_Text lengthPlaceholder;
    public TMP_Text radiusPlaceholder;

    public GameObject gammaSettings;
    public GameObject deltaSettings;
    public GameObject thetaSettings;

    public JsonHandler.DataCollection data;

    string dropdownText;
    int difficulty;

    private void Start()
    {
        dropdownText = "gamma";
        difficulty = 0;
        JsonHandler.getInstance().LoadField();
        data = new JsonHandler.DataCollection
        {
            type = PlayerPrefs.GetInt("type"),
            gammaHeight = PlayerPrefs.GetInt("height"),
            gammaWidth = PlayerPrefs.GetInt("width"),
            deltaLength = PlayerPrefs.GetInt("length"),
            thetaRadius = PlayerPrefs.GetInt("radius")
        };
        widthPlaceholder.text = data.gammaWidth.ToString();
        heightPlaceholder.text = data.gammaHeight.ToString();
        lengthPlaceholder.text = data.deltaLength.ToString();
        radiusPlaceholder.text = data.thetaRadius.ToString();
    }
    public void StartGame()
    {
        GetInputs();
        if (InputsAreCorrect())
        {
            SetGlobalVariables();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void GetInputs()
    {
        switch(dropdownText)
        {
            case "gamma":
                if (widthInput.text != "")
                {
                    data.gammaWidth = int.Parse(widthInput.text);
                }

                if (heightInput.text != "")
                {
                    data.gammaHeight = int.Parse(heightInput.text);
                }
                data.type = 1;
                break;
            case "delta":
                if (lengthInput.text != "")
                {
                    data.deltaLength = int.Parse(lengthInput.text);
                }
                data.type = 2;
                break;
            case "theta":
                if (radiusInput.text != "")
                {
                    data.thetaRadius = int.Parse(radiusInput.text);
                }
                data.type = 3;
                break;
            case "nefu":
                data.type = 4;
                break;
        }
    }
    public bool InputsAreCorrect()
    {
        bool flag = true;
        switch(dropdownText)
        {
            case "gamma":
                if (data.gammaWidth <= 1) flag = false;
                if (data.gammaHeight <= 1) flag = false;
                break;
            case "delta":
                if (data.deltaLength <= 3) flag = false;
                break;
            case "theta":
                if (data.thetaRadius <= 0) flag = false;
                break;
        }
        return flag;
    }
    public void SetGlobalVariables()
    {
        PlayerPrefs.SetInt("type", data.type);
        PlayerPrefs.SetInt("height", data.gammaHeight);
        PlayerPrefs.SetInt("width", data.gammaWidth);
        PlayerPrefs.SetInt("length", data.deltaLength);
        PlayerPrefs.SetInt("radius", data.thetaRadius);
        PlayerPrefs.SetInt("diff", difficulty);
    }
    public void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        dropdownText = dropdown.options[index].text.ToLower();

        switch (dropdownText)
        {
            case "gamma":
                gammaSettings.SetActive(true);
                deltaSettings.SetActive(false);
                thetaSettings.SetActive(false);
                break;
            case "delta":
                deltaSettings.SetActive(true);
                gammaSettings.SetActive(false);
                thetaSettings.SetActive(false);
                break;
            case "theta":
                thetaSettings.SetActive(true);
                gammaSettings.SetActive(false);
                deltaSettings.SetActive(false);
                break;
            case "nefu":
                thetaSettings.SetActive(false);
                gammaSettings.SetActive(false);
                deltaSettings.SetActive(false);
                break;
        }
    }
    public void DiffucultyDropdownItemSelected(TMP_Dropdown dropdown)
    {
        difficulty = dropdown.value;
    }
}
