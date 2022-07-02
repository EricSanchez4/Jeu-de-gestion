using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
public class CMDbouton : MonoBehaviour
{
	
	public InputField namePet;
	public static string nomDuPet;
	public static int nomDuPetLength;
	public Text textErreur;
	
	
	private string timeData;
	private static string currentDate;
	
	private string annee;
	private string mois;
	private string jours;
	
	
	public static int intJoursDeNaissance;
	public static int intMoisDeNaissance;
	public static int intAnneeDeNaissance;
	
	public static string Date;
	void Awake(){
		
		
		textErreur.text = "";
		
	}
		
		
	
	
	public void BOUTONSUIVANT(){
		if (PlayerSelection.currentPlayer != null) {
			if(nomDuPetLength < 3){
				
			textErreur.text = "Le nom du familier est trop court";
				
			
			}
			
			else if (nomDuPetLength > 10){
				
				textErreur.text = "Le nom du familier est trop long";
				
			}
			
			
			else{
				
				
				ChoixPlayer.meshValider.enabled = false;
				StartCoroutine("GetDateNaissance");
				StartCoroutine("Wait1Sec");
				
			
							
			}
		}
		}
	
	
	
    public void NOMDUPET() {
			
		nomDuPet = namePet.text;
		nomDuPetLength = nomDuPet.Length;

		}
	
	public IEnumerator GetDateNaissance()
    {
		
		UnityWebRequest uri = UnityWebRequest.Get("https://www.tutounity.fr/upload/currenttime.php");
    	yield return uri.SendWebRequest();
    	
    	//notre var avec date et heure combiné
    	timeData = uri.downloadHandler.text;

    	//notre var va etre séparer parti 0 et parti 1 en fonction de '/'
    	string[] finalTime = timeData.Split('/');
    	
    	//assignation des var current de  Date et heure
    	currentDate = finalTime[0];
    	//currenTime = finalTime[1];
    	
    	//
    	string[] Date = currentDate.Split('-');
    	jours = Date[0];
    	mois = Date[1];
    	annee = Date[2];
    	
    	int.TryParse(jours, out intJoursDeNaissance);
 	 	int.TryParse(mois, out intMoisDeNaissance);
 	 	int.TryParse(annee, out intAnneeDeNaissance);
    	
       	XenoPrefs.SetInt("dateDeNaissanceJour", CMDbouton.intJoursDeNaissance);
		XenoPrefs.SetInt("dateDeNaissanceMois", CMDbouton.intMoisDeNaissance);
 		XenoPrefs.SetInt("dateDeNaissanceAnnee", CMDbouton.intAnneeDeNaissance);
 		XenoPrefs.SetString("dateDeNaissance", CMDbouton.currentDate);
		XenoPrefs.SetString("PseudoDuPet", CMDbouton.nomDuPet);
    	XenoPrefs.SetInt("ForceDuPet", 3);
    	XenoPrefs.SetInt("IntelligenceDuPet", 3);
    	XenoPrefs.SetInt("AgiliteDuPet", 3);
    
    	
    	PlayerPrefs.SetFloat("CurrentHealth", 100.0F);
    	XenoPrefs.SetFloat("CurrentStamina", 100.0F);
    	XenoPrefs.SetFloat("CurrentEat", 100.0F);
    	XenoPrefs.SetFloat("CurrentHydratation", 100.0F);
    	
    	
    	XenoPrefs.SetString("EtatDuJoueur", "Heureux");
    	
    	XenoPrefs.Save();
    	
    }
	
	IEnumerator Wait1Sec(){
		yield return new WaitForSeconds (1);
				SceneManager.LoadScene("House");
		
	}
	
	
}
