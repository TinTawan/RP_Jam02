using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_Button : MonoBehaviour
{
    public GameObject Panel;

    public void Desac_Act_Panel()
    {
        if (Panel.activeInHierarchy == true)
        {
            Panel.SetActive (false);
        }
        else
            Panel.SetActive  (true);

    }


}
