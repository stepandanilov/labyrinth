using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MazeSettings : MonoBehaviour
{
    //delta settings
    public TMP_InputField widthFieldInput;
    public TMP_InputField heightFieldInput;
    //gamma settings
    public TMP_InputField lengthFieldInput;

    public Dropdown dropdown;

    public GameObject gammaSettings;
    public GameObject deltaSettings;

    int width;
    int height;
    int length;

    string dropdownText = "gamma";
    private void Start()
    {
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
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
    public void getInputs()
    {
        if (dropdownText == "gamma")
        {
            if (widthFieldInput.text != "")
            {
                width = int.Parse(widthFieldInput.text);
            }
            else
            {
                width = Globals.mazeWidth;
            }

            if (heightFieldInput.text != "")
            {
                height = int.Parse(heightFieldInput.text);
            }
            else
            {
                height = Globals.mazeHeight;
            }
            Debug.Log("getinputs");
        }
        if (dropdownText == "delta")
        {
            if (lengthFieldInput.text != "")
            {
                length = int.Parse(lengthFieldInput.text);
            }
            else
            {
                length = Globals.triangleMazeLength;
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

            Debug.Log("globalinput");
        }
        if (dropdownText == "delta")
        {
            Globals.triangleMazeLength = length;

            Globals.mazeType = 2;
        }
    }
    public void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        dropdownText = dropdown.options[index].text;
        dropdownText = dropdownText.ToLower();
        // may change later when more types are added
        if (dropdownText == "gamma")
        {
            gammaSettings.SetActive(true);
            deltaSettings.SetActive(false);
        }
        else if (dropdownText == "delta")
        {
            deltaSettings.SetActive(true);
            gammaSettings.SetActive(false);
        }    
    }
}
