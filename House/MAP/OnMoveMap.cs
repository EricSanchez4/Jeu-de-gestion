using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class OnMoveMap : MonoBehaviour
{
    public GameObject MonButton;

   
    public void OnMouseEnter()
    {
        MonButton.SetActive(true);
    }

    public void OnMouseExit()
    {
        MonButton.SetActive(false);
    }

   
}
