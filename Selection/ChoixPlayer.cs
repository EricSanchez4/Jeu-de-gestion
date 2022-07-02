using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoixPlayer : MonoBehaviour
{
	public static Transform cursor;
	public static Renderer meshValider;
	public static GameObject boutonSuivant;
	public static GameObject inputField;

    // Start is called before the first frame update
    void Awake()
    {
    	
    	cursor = GameObject.Find("Cursor").transform;
    	meshValider = GameObject.Find("Cursor").GetComponent<Renderer>();
    	meshValider.enabled = false;
    	boutonSuivant = GameObject.Find("ButtonSuivant");
    	boutonSuivant.SetActive(false);
    	inputField = GameObject.Find("InputField");
    	inputField.SetActive(false);
    	
    	
    }
}
