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

    public GameObject gammaSettings;
    public GameObject deltaSettings;
    public GameObject thetaSettings;

    int width;
    int height;
    int length;
    int radius;

    string dropdownText = "gamma";
    private void Start()
    {

    }
    public void StartGame()
    {
        getInputs();
        if (inputsAreCorrect())
        {
            setGlobalVariables();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void getInputs()
    {
        if (dropdownText == "gamma")
        {
            if (widthInput.text != "")
            {
                width = int.Parse(widthInput.text);
            }
            else
            {
                width = Globals.mazeWidth;
            }

            if (heightInput.text != "")
            {
                height = int.Parse(heightInput.text);
            }
            else
            {
                height = Globals.mazeHeight;
            }
        }
        if (dropdownText == "delta")
        {
            if (lengthInput.text != "")
            {
                length = int.Parse(lengthInput.text);
            }
            else
            {
                length = Globals.triangleMazeLength;
            }
        }
        if (dropdownText == "theta")
        {
            if (radiusInput.text != "")
            {
                radius = int.Parse(radiusInput.text);
            }
            else
            {
                radius = Globals.triangleMazeLength;
            }
        }
    }
    public bool inputsAreCorrect()
    {
        bool flag = true;
        if (dropdownText == "gamma")
        {
            if (width <= 2) flag = false;
            if (height <= 2) flag = false;
        }
        if (dropdownText == "delta")
        {
            if (length <= 2) flag = false;
        }
        if (dropdownText == "theta")
        {
            if (radius <= 4) flag = false;
        }
        return flag;
    }
    public void setGlobalVariables()
    {
        // +1 because of invisible cells 
        if (dropdownText == "gamma")
        {
            Globals.mazeHeight = height + 1;
            Globals.mazeWidth = width + 1;

            Globals.mazeType = 1;
        }
        if (dropdownText == "delta")
        {
            Globals.triangleMazeLength = length;

            Globals.mazeType = 2;
        }
        if (dropdownText == "theta")
        {
            Globals.thetaMazeRadius = radius;

            Globals.mazeType = 3;
        }
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
        }
    }
}
