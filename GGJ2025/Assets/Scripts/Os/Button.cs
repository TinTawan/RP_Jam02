using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Os");
        Debug.Log("Wuw");
    } 

}
