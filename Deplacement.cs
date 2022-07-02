using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Deplacement : MonoBehaviour
{
    public void HOME()
    {
        SceneManager.LoadScene(1);
    }

    public static void FishingGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Restaurant()
    {
        SceneManager.LoadScene(3);
    }

    public void Hospital()
    {
        SceneManager.LoadScene(4);
    }
    public void Map()
    {
        SceneManager.LoadScene(5);
    }
    public void POMME()
    {
        SceneManager.LoadScene(6);
    }
}
