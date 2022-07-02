using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnmouseOver : MonoBehaviour
{

    public GameObject SurRestaurant;

   

    public void OnMouseEnter()
    {

        
        SurRestaurant.SetActive(true);
        
    }

    public void OnMouseExit()
    {
        SurRestaurant.SetActive(false);
        


    }

    public void OnMouseDown()
    {
        SceneManager.LoadScene(3);
    }

}
