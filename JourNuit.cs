using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JourNuit : MonoBehaviour
{
	
	[Header("Jour/Nuit/Tempete Matérials")]
	public Renderer gameObjectSkydomerend;
	public Material[] material;
	
	[Header("Jour/Nuit GameObject")]
	public GameObject gameObjectJours;
	public GameObject gameObjectNuit;
	public GameObject gameObjectFeu;
	public GameObject gameObjectSoleil;
	public GameObject gameObjectMoon;	
	[Header("Clouds")]	
	public GameObject gameObjectCloud1;
	public GameObject gameObjectCloud2;
	public GameObject gameObjectCloud3;
	public float GenrateCloud;
	[Header("Rain")]	
	public int GenrateTempete;
	public bool tempete = false;
	public GameObject gameObjectRainPrefab;
    public StatsJoueur statsJoueur; //Script Statjoueur

    void Awake()
    {
    		tempete = false;
        	gameObjectSkydomerend = GetComponent<Renderer> ();
        	gameObjectSkydomerend.enabled = true;
			gameObjectSkydomerend.sharedMaterial = material[0];
			GenrateTempete = Random.Range(0, 100);
			
			if 	(GenrateTempete <= 6) {
				tempete = true;
            statsJoueur.Tempete();
			}
    }
    void Update() {
    	GenrateCloud = Random.Range(0.0f, 100.00f);

        StartCoroutine(CloudDestroy());
    		
    		
    }
    
     private IEnumerator CloudDestroy()
     {

        if (GenrateCloud <= 0.15f)
        {
            if (GenrateCloud > 0.135f && GenrateCloud <= 0.15f)
            {

                GameObject Clouds1 = Instantiate(gameObjectCloud1, new Vector3(-7, 65, -343), transform.rotation * Quaternion.Euler(0f, 135f, 0f));
                yield return new WaitForSeconds(600);
                Destroy(Clouds1);
            }
            if (GenrateCloud >= 0.008f && GenrateCloud < 0.014f)
            {
                GameObject Clouds2 = Instantiate(gameObjectCloud2, new Vector3(-150, 23, -257), transform.rotation * Quaternion.Euler(0f, 150f, 0f));
                yield return new WaitForSeconds(600);
                Destroy(Clouds2);

            }
            if (GenrateCloud > 0.00f && GenrateCloud <= 0.008f)
            {
                GameObject Clouds3 = Instantiate(gameObjectCloud3, new Vector3(-51, 19, -124), transform.rotation * Quaternion.Euler(0f, 135f, 0f));
                yield return new WaitForSeconds(600);
                Destroy(Clouds3);

            }
        }
        if (tempete)
        {
            if (GenrateCloud <= 4f)
            {
                if (GenrateCloud > 0.135f && GenrateCloud <= 0.15f)
                {
                    GameObject Clouds1 = Instantiate(gameObjectCloud1, new Vector3(-7, 65, -343), transform.rotation * Quaternion.Euler(0f, 135f, 0f));
                    yield return new WaitForSeconds(600);
                    Destroy(Clouds1);
                }
                if (GenrateCloud >= 0.008f && GenrateCloud < 0.014f)
                {

                    GameObject Clouds2 = Instantiate(gameObjectCloud2, new Vector3(-150, 23, -257), transform.rotation * Quaternion.Euler(0f, 150f, 0f));
                    yield return new WaitForSeconds(600);
                    Destroy(Clouds2);
                }
                if (GenrateCloud > 0.00f && GenrateCloud <= 0.008f)
                {
                    GameObject Clouds3 = Instantiate(gameObjectCloud3, new Vector3(-51, 19, -124), transform.rotation * Quaternion.Euler(0f, 135f, 0f));
                    yield return new WaitForSeconds(600);
                    Destroy(Clouds3);
                }
            }
        }
        

    }


    public void Jours(){
    	gameObjectFeu.SetActive(false);
    	gameObjectMoon.SetActive(false);
    	gameObjectNuit.SetActive (false);
    	gameObjectSoleil.SetActive(true);
    	gameObjectJours.SetActive(true);
    	gameObjectSkydomerend.sharedMaterial = material[2];
    	
    	if (tempete) {
    		
    		gameObjectFeu.SetActive(false);
   			Instantiate(gameObjectRainPrefab);
    		gameObjectSkydomerend.sharedMaterial = material[4];
    	}
    }
    
    public void Nuit(){
    	gameObjectFeu.SetActive(true);
    	gameObjectMoon.SetActive(true);
    	gameObjectSoleil.SetActive(false);
    	gameObjectNuit.SetActive(true);
    	gameObjectJours.SetActive(false);
    	gameObjectSkydomerend.sharedMaterial = material[3];
    	if (tempete) {
    		
    		gameObjectFeu.SetActive(false);
  			Instantiate(gameObjectRainPrefab);
    		gameObjectSkydomerend.sharedMaterial = material[4];
    }
    }
    public void Lever(){
    	gameObjectFeu.SetActive(false);
    	gameObjectMoon.SetActive(false);
    	gameObjectSoleil.SetActive(true);
    	gameObjectNuit.SetActive(true);
    	gameObjectJours.SetActive(false);
    	gameObjectSkydomerend.sharedMaterial = material[0];
    	if (tempete) {
    		
    		gameObjectFeu.SetActive(false);
   			Instantiate(gameObjectRainPrefab);
    		gameObjectSkydomerend.sharedMaterial = material[4];
    }
    }
    public void Coucher(){
    	gameObjectFeu.SetActive(true);
    	gameObjectMoon.SetActive(true);
    	gameObjectSoleil.SetActive(false);
    	gameObjectNuit.SetActive(true);
    	gameObjectJours.SetActive(false);
    	gameObjectSkydomerend.sharedMaterial = material[1];
    	if (tempete) {
    		
    		gameObjectFeu.SetActive(false);
   			Instantiate(gameObjectRainPrefab);
    		gameObjectSkydomerend.sharedMaterial = material[4];
    }
    }
    
      
    
}
