using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
	public string choixNomPet;
	public Vector3 newPosition;
	public static string currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
    	newPosition = transform.position + new Vector3(0, 0, 1.5f);
    	
    }
    
    void OnMouseDown () {
    	if (ChoixPlayer.cursor != null) {
    		currentPlayer = choixNomPet;
    		XenoPrefs.SetString("CategorieDuPet", PlayerSelection.currentPlayer);
			ChoixPlayer.cursor.transform.position = newPosition;
			ChoixPlayer.boutonSuivant.SetActive(true);
			ChoixPlayer.inputField.SetActive(true);
			ChoixPlayer.meshValider.enabled = true;
			
			}
    	
    	
    }

}
