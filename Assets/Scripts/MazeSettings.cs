using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MazeSettings : MonoBehaviour
{
    public TMP_InputField widthFieldInput;
    public TMP_InputField heightFieldInput;

    int width;
    int height;
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
    }
    public bool inputsAreCorrect()
    {
        bool flag = true;

        if (width <= 2) flag = false;
        if (height <= 2) flag = false;

        return flag;
    }
    public void setGlobalVariables()
    {
        // +1 because of invisible cells 
        Globals.mazeHeight = height + 1;
        Globals.mazeWidth = width + 1;
    }
}
