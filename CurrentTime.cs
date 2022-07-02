using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; 
using UnityEngine.Networking;
using UnityEngine.UI;

public class CurrentTime : MonoBehaviour
{
	
	private string timeData;
	private string currentDate;
	private string currentTime;
	
	private string annee;
	private string mois;
	private string jours;
	private string anneeN;
	private string moisN;
	private string joursN;
	private string heure;
	private string minute;
	private string seconde;
	
	public static int intHeure;
	public static int intMinute;
	public static int intSeconde;
	public static int intJours;
	public static int intMois;
	public static int intAnnee;
	
	public static string dateRepriseNaissance;
	public static int intJoursDeNaissance;
	public static int intMoisDeNaissance;
	public static int intAnneeDeNaissance;
	
	public static string Date;
	public static int ageDuPetEnJours;
	public static int nbHeureDepuisConnexionprecedente;
	
	public Text texteDerniereConnexion;
	public Text ageDuPet_text;
	

   

    //Var connexion précedentes
    private int exannee;
	private int exmois;
	private int exjours;
	private int exheure;
	private int exminute;
	private int exseconde;
	
	//Scrip StatsJoueur pour soustraire les scrollBars
	
	public StatsJoueur statsJoueur;
	public JourNuit jourNuit;

    public int StockForFoodsPerDay;
    private int StockOfHam;
    private int StockOfEgg;
    private int StockOfFish;

    void Awake()
    {
    	//DontDestroyOnLoad(gameObject);
    	
    	dateRepriseNaissance = XenoPrefs.GetString("dateDeNaissance","0-0-0");
    	StartCoroutine("GetTimeOnVar");
    	
    	
    		}
    	
    	
    
    
   void Start(){
    	
    	   	
		//Debug.Log(Random.Range(1, 100)); //###########Utile##############
	
    }
    
    void Update() {

        statsJoueur.Evolution(ageDuPetEnJours);


    }
    void OnApplicationQuit()
    {
        
    	XenoPrefs.SetInt("intJoursPrecedent",CurrentTime.intJours);
    	XenoPrefs.SetInt("intMoisPrecedent",CurrentTime.intMois);
    	XenoPrefs.SetInt("intAnneePrecedent",CurrentTime.intAnnee);
    	XenoPrefs.SetInt("intHeurePrecedent",CurrentTime.intHeure);
    	XenoPrefs.SetInt("intMinutePrecedent",CurrentTime.intMinute);
    	XenoPrefs.SetInt("intSecondePrecedent",CurrentTime.intSeconde);
    	XenoPrefs.Save();
    }
   

    public IEnumerator GetTimeOnVar()
    {
    	UnityWebRequest uri = UnityWebRequest.Get("https://www.tutounity.fr/upload/currenttime.php");
    	yield return uri.SendWebRequest();
    	
    	
    	
    	//notre var avec date et heure combiné
    	timeData = uri.downloadHandler.text;
    	
    	
    	//notre var va etre séparer parti 0 et parti 1 en fonction de '/'
    	string[] finalTime = timeData.Split('/');
        // Var 1 11-02-2021
        // Var 2 12:25:30

        // Var 1 JOur 11
        // Var 2mois  02
        // Var 3 année 2021

        // Var 4 Heure  12
        // Var 5Minute  25
        // Var 6 Seconde 30 

        //assignation des var current de  Date et heure
        currentDate = finalTime[0];
    	currentTime = finalTime[1];
    	
    	string[] Date = currentDate.Split('-');
    	jours = Date[0];
    	mois = Date[1];
    	annee = Date[2];
    	
    	int.TryParse(jours, out intJours);
 	 	int.TryParse(mois, out intMois);
		int.TryParse(annee, out intAnnee);
		
		string[] heureDuJours = currentTime.Split(':');
    	heure = heureDuJours[0];
    	minute = heureDuJours[1];
    	seconde = heureDuJours[2];
    	
    	int.TryParse(heure, out intHeure);
 	 	int.TryParse(minute, out intMinute);
		int.TryParse(seconde, out intSeconde);
    	//C'est la date du jour
    	System.DateTime dateDuJour = new DateTime(intAnnee, intMois, intJours, intHeure, intMinute , intSeconde);
    	
  		string[] DateNaissanceToDateTime = dateRepriseNaissance.Split('-');
 	  	joursN = DateNaissanceToDateTime[0];
	    moisN = DateNaissanceToDateTime[1];
	    anneeN = DateNaissanceToDateTime[2];
    	
 	   int.TryParse(joursN, out intJoursDeNaissance);
 		int.TryParse(moisN, out intMoisDeNaissance);
		int.TryParse(anneeN, out intAnneeDeNaissance);
	
    	System.DateTime dateDeNaissance = new DateTime(intAnneeDeNaissance, intMoisDeNaissance, intJoursDeNaissance, 0, 0 ,0);
    	
    	//Calcul de l'age du pet en jours
    	double ageDuPet = (dateDuJour - dateDeNaissance).Days;
    	ageDuPetEnJours = System.Convert.ToInt32(System.Math.Floor(ageDuPet));
    	ageDuPet_text.text = "Age : " + ageDuPetEnJours.ToString() + " jours"; // affiche a l'écran Utilisateur l'age du pet

       
        


        exjours = XenoPrefs.GetInt("intJoursPrecedent",0);
    	exmois = XenoPrefs.GetInt("intMoisPrecedent",0);
    	exannee = XenoPrefs.GetInt("intAnneePrecedent",0);
    	exheure = XenoPrefs.GetInt("intHeurePrecedent",0);
    	exminute = XenoPrefs.GetInt("intMinutePrecedent",0);
    	exseconde = XenoPrefs.GetInt("intSecondePrecedent",0);
    	
    	//Calcul de la différences en Heures entre la connexion précendente et celle du jours.
    	System.DateTime dateDeLaPrecedenteConnexion = new DateTime(exannee, exmois, exjours , exheure , exminute , exseconde);

		double dateConnexionPrecedente = (dateDuJour - dateDeLaPrecedenteConnexion).TotalHours;
		nbHeureDepuisConnexionprecedente = System.Convert.ToInt32(System.Math.Floor(dateConnexionPrecedente));
		Debug.Log (nbHeureDepuisConnexionprecedente + " Heures se sont écouler depuis la dernières connexion.");
    	
		if (nbHeureDepuisConnexionprecedente >= 1) {
    	texteDerniereConnexion.text = "Dernière connexion il y a: " + nbHeureDepuisConnexionprecedente + " heures.";
    	}
    	if(nbHeureDepuisConnexionprecedente == 0)
    	{
    		texteDerniereConnexion.text = "Dernière connexion il y a moins d'une heure.";
    	}



        //Gestion du stock en restaurant. Droit a 2 types de bouffe tout les jours. Pas plus.
        if( intMois == exmois && intJours == exjours )
        {
            //Do nothing

        }
        else if (intJours > exjours)
        {
            StockOfHam = StockOfHam + StockForFoodsPerDay;
            StockOfEgg = StockOfEgg + StockForFoodsPerDay;
            StockOfFish = StockOfFish + StockForFoodsPerDay;
            
            if(StockOfHam >= 3) { StockOfHam = 2; }
            if (StockOfEgg >= 3) { StockOfEgg = 2; }
            if (StockOfFish >= 3) { StockOfFish = 2; }

            XenoPrefs.SetInt("FoodOfHamPerDay", StockOfHam);
            XenoPrefs.SetInt("FoodOfEggPerDay", StockOfEgg);
            XenoPrefs.SetInt("FoodOfFishPerDay", StockOfFish);
            XenoPrefs.Save();

        }
        else if (intMois > exmois)
        {
            StockOfHam = StockOfHam + StockForFoodsPerDay;
            StockOfEgg = StockOfEgg + StockForFoodsPerDay;
            StockOfFish = StockOfFish + StockForFoodsPerDay;

            if (StockOfHam >= 3) { StockOfHam = 2; }
            if (StockOfEgg >= 3) { StockOfEgg = 2; }
            if (StockOfFish >= 3) { StockOfFish = 2; }

            XenoPrefs.SetInt("FoodOfHamPerDay", StockOfHam);
            XenoPrefs.SetInt("FoodOfEggPerDay", StockOfEgg);
            XenoPrefs.SetInt("FoodOfFishPerDay", StockOfFish);
            XenoPrefs.Save();
        }
        else if (intAnnee > exannee)
        {
            StockOfHam = StockOfHam + StockForFoodsPerDay;
            StockOfEgg = StockOfEgg + StockForFoodsPerDay;
            StockOfFish = StockOfFish + StockForFoodsPerDay;

            if (StockOfHam >= 3) { StockOfHam = 2; }
            if (StockOfEgg >= 3) { StockOfEgg = 2; }
            if (StockOfFish >= 3) { StockOfFish = 2; }

            XenoPrefs.SetInt("FoodOfHamPerDay", StockOfHam);
            XenoPrefs.SetInt("FoodOfEggPerDay", StockOfEgg);
            XenoPrefs.SetInt("FoodOfFishPerDay", StockOfFish);
            XenoPrefs.Save();

        }

        //--------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------Gestion des barres---------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------


        //Besoin de nouriture sous 50h	
        statsJoueur.PerteEat(nbHeureDepuisConnexionprecedente); 		//Perte de la barre Eat et Hydratation 
    	statsJoueur.DamagesNoEat(nbHeureDepuisConnexionprecedente);		// perte des point de vie du familier
    	
    	
    	//Jour && Nuit
    	if (intHeure >= 6 && intHeure <= 9) {
    		jourNuit.Lever();
    	}
    	else if(intHeure >= 10 && intHeure <= 17) {
    		jourNuit.Jours();
    	}
    	else if(intHeure >= 18 && intHeure <= 21) {
    		jourNuit.Coucher();
    	}
    	else if(intHeure >= 22 && intHeure <= 23 || intHeure >= 0 && intHeure <= 5) {
    		jourNuit.Nuit();
    	}
    }
}
