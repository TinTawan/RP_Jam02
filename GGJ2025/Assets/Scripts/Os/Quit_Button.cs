using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit_Button : MonoBehaviour
{
    public void Quit_B()
    {

        Application.Quit();
        Debug.Log("Game Close");

    }

}
