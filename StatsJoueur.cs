using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; 
using UnityEngine.UI;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;

public class StatsJoueur : MonoBehaviour
{
	
	[Header("GameObject To Spawn")]
    public static GameObject playerGameObject;
    public  GameObject Dark;
	public  GameObject Angel;
    public  GameObject AngelLVL2;
    public  GameObject DarkLVL2;
    public  GameObject AngelLVL3;
    public  GameObject DarkLVL3;
    public  GameObject AngelLVL4;
    public  GameObject DarkLVL4;
    public  GameObject AngelLVL5;
    public  GameObject DarkLVL5;
    

    [Header("Name Player")]
	public static string nameTempo;
	public Text txt;

    [Header("Evolution")]
    [SerializeField] int EvoPointLVL1 = 30;
    [SerializeField] int EvoDaysLVL1 = 15;

    [SerializeField] int EvoPointLVL2 = 100;
    [SerializeField] int EvoDaysLVL2 = 45;

    [SerializeField] int EvoPointLVL3 = 200;
    [SerializeField] int EvoDaysLVL3 = 110;

    [SerializeField] int EvoPointLVL4 = 410;
    [SerializeField] int EvoDaysLVL4 = 250;

    public static String Evolve1 = "Non";   // Evolve 1/2/3 sur non = Niveau 0
    public String Evolve2 = "Non";   // Evolve 2/3 sur non  et 1 sur true = Niveau 1
    public String Evolve3 = "Non";   // Evolve 1/3 sur non et 2 sur true = Niveau 2 ______ Evolve 1/2 sur non et 3 sur true = Niveau 3
    public string EvoMAX = "Non";
    public GameObject Evole_Button;
    public Text joursRestantavantEvolutionText;
    private int joursRestantavantEvolution;
    private int PointAttributRestantAAvoir;
    public GameObject EvolutionFX;
    public Material White_Evo;
   

    [Header("Etat Du Familier")]
    public string etatDuJoueur;
	[SerializeField] public Image ImageEtatAModif;
	[SerializeField] public Image[] imageEtat;
	[SerializeField] public Text txtEtat;

    private int faim = 0;               ///BOOL "INT"// 0 = false; 1 = true ///
    private int heureux = 0;            ///BOOL "INT"// 0 = false; 1 = true ///
    private int tired = 0;             ///BOOL "INT"// 0 = false; 1 = true ///
    private int malade = 0;             ///BOOL "INT"// 0 = false; 1 = true ///
    private int mort = 0;               ///BOOL "INT"// 0 = false; 1 = true ///
    private int resurection = 0;        ///BOOL "INT"// 0 = false; 1 = true ///

    [Header("ScrollBar Settings")]
    private float maxHealth = 100.0f;
    public static float currentHealth;
	public SimpleHealthBar healthBar;

    private float maxStamina = 100.0f;
    private static float currentstamina;
	public SimpleHealthBar staminaBar;

    private float maxEat = 100f;
    private static float currenteat;
	public SimpleHealthBar eatBar;

    private float maxHydratation = 100.0f;
    private static float currenthydratation;
	public SimpleHealthBar hydratationBar;
	
	
	[Header("Caractéristiques")]//Var caractères
    public static int agility;
	public  Text agilityTxt;

    public static int strong;
	public Text strongTxt;

    public static int intelligence;
	public Text intelligenceTxt;

    private static int PointAttribut;
    private int gold;
    private Text TextGold;
    //script:
    public CurrentTime currentTime;

	
	void Awake() {
        GameObject GoldG = GameObject.Find("Text-Gold");
        TextGold = GoldG.GetComponent<Text>();



    }
	
	void Update(){

        //Priorité de l'état du joueur
         gold = XenoPrefs.GetInt("Gold", 0);
        TextGold.text = gold.ToString() + " Gold"; ;

        if (mort == 0)
        {
            if (malade == 1) {

                etatDuJoueur = "Malade";
                XenoPrefs.SetString("EtatDuJoueur", "Malade");
                ImageEtatAModif.overrideSprite = imageEtat[2].overrideSprite;
                if (malade == 0 && etatDuJoueur == "Malade")
                {
                    ImageEtatAModif.overrideSprite = imageEtat[0].overrideSprite;

                }
            }
            
            else if (faim == 1 && malade == 0)
            {
                etatDuJoueur = "Faim";
                XenoPrefs.SetString("EtatDuJoueur", "Faim");
                ImageEtatAModif.overrideSprite = imageEtat[1].overrideSprite;
            }
            else if (tired == 1 && malade == 0 && faim == 0)
            {
                etatDuJoueur = "Epuisé";
                XenoPrefs.SetString("EtatDuJoueur", "Epuisé");
                ImageEtatAModif.overrideSprite = imageEtat[3].overrideSprite;
            }
            else if (malade == 0 && faim == 0 && tired == 0)
            {
                
                etatDuJoueur = "Heureux";
                XenoPrefs.SetString("EtatDuJoueur", "Heureux");
                ImageEtatAModif.overrideSprite = imageEtat[0].overrideSprite;
            }
          
        }else{

            currentHealth = 00.00f;
            currentstamina = 00.00f;
            currenteat = 00.00f;
            currenthydratation = 00.00f;
       

            etatDuJoueur = "Mort";
            XenoPrefs.SetString("EtatDuJoueur", "Mort");
            ImageEtatAModif.overrideSprite = imageEtat[4].overrideSprite;

            if (resurection == 1)
            {

                resurection = 0;
                etatDuJoueur = "Malade";
                XenoPrefs.SetString("EtatDuJoueur", "Malade");
                currentHealth = 15f;
                currentstamina = 50.00f;
                currenteat = 00.00f;
                currenthydratation = 00.00f;
           
               
            }
        }

        //Limite des ScrollBar


        //Stamina
        if (currentstamina > 100.00f) { //Max Stamina
			currentstamina = 100.00f;}
		if (currentstamina < 00.00f) {  //Min Stamina
			currentstamina = 00.00f;}
        if (currentstamina <= 10f)
        {tired = 1;}
        if (currentstamina >= 11f)
        { tired = 0; }

        //EAT
        if (currenteat > 100.00f) { //Max Eat
			currenteat = 100.00f;}
        if (currenteat <= 00.00f) //Min Eat
        { 
            currenteat = 00.00f;
        }
        if (currenteat > 15.00f)
        {
            faim = 0;
        } 
        
        if (currenteat < 25.00f) { // Joueur a Faim
            faim = 1;}

		//Hydratation
		if (currenthydratation > 100.00f) { // Max Hydratation
			currenthydratation = 100.00f;}
		if (currenthydratation < 00.00f) { // Min Hydratation
			currenthydratation = 00.00f;}
		
		//Health
		if (currentHealth > 100.00f) { //Max Health
			currentHealth = 100.00f;}

		if (currentHealth <= 00.00f) { // Min Health & Joueur Mort
		currentHealth = 00.00f;
        mort = 1;
		
		}
        if (currentHealth > 01.00f && mort == 1)
        {
            Debug.Log("Player Resurection");
            malade = 1;
            mort = 0;

        }

        //MAJ de barres Continuellement
        healthBar.UpdateBar(currentHealth, maxHealth);
        staminaBar.UpdateBar(currentstamina, maxStamina);
        eatBar.UpdateBar(currenteat, maxEat);
        hydratationBar.UpdateBar(currenthydratation, maxHydratation);

        //save des BARRE
        PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
		XenoPrefs.SetFloat("CurrentStamina", StatsJoueur.currentstamina);
		XenoPrefs.SetFloat("CurrentEat", StatsJoueur.currenteat);
		XenoPrefs.SetFloat("CurrentHydratation", StatsJoueur.currenthydratation);
		
		//Save des caractéristiques
		XenoPrefs.SetInt("AgiliteDuPet", StatsJoueur.agility);
		XenoPrefs.SetInt("ForceDuPet", StatsJoueur.strong);
		XenoPrefs.SetInt("IntelligenceDuPet", StatsJoueur.intelligence);
		XenoPrefs.Save();

  
        //Affichage du texte etat
        txtEtat.text = "Etat : " + XenoPrefs.GetString("EtatDuJoueur",etatDuJoueur);
       
        agilityTxt.text = agility.ToString();
        strongTxt.text = strong.ToString();
        intelligenceTxt.text = intelligence.ToString();


    }
	void Start(){
		nameTempo = XenoPrefs.GetString("PseudoDuPet", CMDbouton.nomDuPet);
        EvoMAX = XenoPrefs.GetString("EvolutionMax", EvoMAX);
        txt.text = nameTempo;
        Evole_Button.SetActive(false);
        EvolutionFX.SetActive(false);


        //Reprise des evolution
        Evolve1 = XenoPrefs.GetString("Evolution1", Evolve1);
        Evolve2 = XenoPrefs.GetString("Evolution2", Evolve2);
        Evolve3 = XenoPrefs.GetString("Evolution3", Evolve3);
        Debug.Log(Evolve1 + Evolve2 + Evolve3);
        //Reprise des currentBar
        currentHealth = PlayerPrefs.GetFloat("CurrentHealth",100.0f);
		currentstamina = XenoPrefs.GetFloat("CurrentStamina",100.0f);
		currenteat = XenoPrefs.GetFloat("CurrentEat",100.0f);
		currenthydratation = XenoPrefs.GetFloat("CurrentHydratation",100.0f);
		
		healthBar.UpdateBar( currentHealth, maxHealth);
		staminaBar.UpdateBar( currentstamina, maxStamina);
		eatBar.UpdateBar( currenteat, maxEat);
		hydratationBar.UpdateBar( currenthydratation, maxHydratation);
        intelligence = XenoPrefs.GetInt("IntelligenceDuPet", 3);
        strong = XenoPrefs.GetInt("ForceDuPet", 3);
        agility = XenoPrefs.GetInt("AgiliteDuPet", 3);

        //--------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------Gestion des barres dans CurrentTime.cs--------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------

        if (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(Dark); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(Angel); }
        }
        if (Evolve1 == "true" && Evolve2 == "Non" && Evolve3 == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL2); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL2); }
        }
        if (Evolve1 == "Non" && Evolve2 == "true" && Evolve3 == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL3); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL3); }
        }
        if (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "true" && EvoMAX == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL4); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL4); }
        }
        if (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "true" && EvoMAX == "true")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL5); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL5); }
        }


    }
	 
	 
	 void OnApplicationQuit()
    {
   
		//Save des currentBar
		PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
		XenoPrefs.SetFloat("CurrentStamina", StatsJoueur.currentstamina);
		XenoPrefs.SetFloat("CurrentEat", StatsJoueur.currenteat);
		XenoPrefs.SetFloat("CurrentHydratation", StatsJoueur.currenthydratation);
        //Save des evolution
        XenoPrefs.SetString("Evolution1", Evolve1);
        XenoPrefs.SetString("Evolution2", Evolve2);
        XenoPrefs.SetString("Evolution3", Evolve3);
        XenoPrefs.SetString("EvolutionMax", EvoMAX);
        //Save des caractéristiques
        XenoPrefs.SetInt("AgiliteDuPet", StatsJoueur.agility);
		XenoPrefs.SetInt("ForceDuPet", StatsJoueur.strong);
		XenoPrefs.SetInt("IntelligenceDuPet", StatsJoueur.intelligence);
		
		XenoPrefs.Save();
	}
	 	
	public void VerySmallDamages(){
   	
 	currentHealth -= 5;
   	healthBar.UpdateBar( currentHealth, maxHealth);
   	PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
   			PlayerPrefs.Save();
	}
	
	
	public void MiddleDamages(){
   	
 	currentHealth -= 20f;
   	healthBar.UpdateBar( currentHealth, maxHealth);
   	PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
   			PlayerPrefs.Save();
	}
	
	public void LargeDamages(){
   	
 	currentHealth -= 40f;
   	healthBar.UpdateBar( currentHealth, maxHealth);
   	PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
   			PlayerPrefs.Save();
	}
	
	public void PerteStamina(){
   	
 	currentstamina -= 10f;
   	staminaBar.UpdateBar( currentstamina, maxStamina);
   	XenoPrefs.SetFloat("CurrentStamina", StatsJoueur.currentstamina);
   	XenoPrefs.Save();
	}
	
	
	public void LargePerteStamina(){
   	
 	currentstamina -= 20f;
   	staminaBar.UpdateBar( currentstamina, maxStamina);
   	XenoPrefs.SetFloat("CurrentStamina", StatsJoueur.currentstamina);
   	XenoPrefs.Save();
	}
	
	public void PerteEat(int nbHeureDepuisConnexionprecedente){
        //Perte eat en 50H de déconnexion

        currentHealth = PlayerPrefs.GetFloat("CurrentHealth", 100.0f);
        currentstamina = XenoPrefs.GetFloat("CurrentStamina", 100.0f);
        currenteat = XenoPrefs.GetFloat("CurrentEat", 100.0f);
        currenthydratation = XenoPrefs.GetFloat("CurrentHydratation", 100.0f);



        if (currenthydratation <= 0 && nbHeureDepuisConnexionprecedente >0) {
            currenteat -= nbHeureDepuisConnexionprecedente * 02.00f;
   			eatBar.UpdateBar( currenteat, maxEat);
   			XenoPrefs.SetFloat("CurrentEat", StatsJoueur.currenteat);
   			XenoPrefs.Save();
            }
   	
   	else if (currenthydratation > 00.00f && nbHeureDepuisConnexionprecedente > 0) {
    		currenteat -= nbHeureDepuisConnexionprecedente * 01.00f;
   			eatBar.UpdateBar( currenteat, maxEat);
   			XenoPrefs.SetFloat("CurrentEat", StatsJoueur.currenteat);
   				
    		currenthydratation -= nbHeureDepuisConnexionprecedente * 02.00f;
    		hydratationBar.UpdateBar( currenthydratation, maxHydratation);
   			XenoPrefs.SetFloat("CurrentHydratation", StatsJoueur.currenthydratation);
   			XenoPrefs.Save();
   				
   					
  		}
   }




	public void DamagesNoEat(int nbHeureDepuisConnexionprecedente){
	 	if (currenteat < 00.00f) {
	        // en l'état 
 			currentHealth -= nbHeureDepuisConnexionprecedente * 00.50f;
   			healthBar.UpdateBar( currentHealth, maxHealth);
   			PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
   			PlayerPrefs.Save();
   			
		}
	}

    public void PerteMiddleEat()
    {

        currenteat -= 25f;
        eatBar.UpdateBar(currenteat, maxEat);
        XenoPrefs.SetFloat("CurrentEat", StatsJoueur.currenteat);
        XenoPrefs.Save();
    }
    public void PerteHydratation(){
   	
 	currenthydratation -= 10f;
   	hydratationBar.UpdateBar( currenthydratation, maxHydratation);
   	XenoPrefs.SetFloat("CurrentHydratation", StatsJoueur.currenthydratation);
   	XenoPrefs.Save();
	}
	
	public void UpMiddleDamages(){
   	
 	currentHealth += 20f;
   	healthBar.UpdateBar( currentHealth, maxHealth);
   PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
   			PlayerPrefs.Save();
	}
	
	
	
	public void UpStamina(){
   	
 	currentstamina += 10f;
   	staminaBar.UpdateBar( currentstamina, maxStamina);
   	XenoPrefs.SetFloat("CurrentStamina", StatsJoueur.currentstamina);
   	XenoPrefs.Save();
	}
	
	
	
	public void UpEat(){
   	//Perte eat en 48H de déconnexion
 	currenteat += 20f;
   	eatBar.UpdateBar( currenteat, maxEat);
   	XenoPrefs.SetFloat("CurrentEat", StatsJoueur.currenteat);
   	XenoPrefs.Save();
	}

    public void UpStr()
    {strong++;}
    public void UpInt()
    {intelligence++;}
    public void UpAgi()
    {agility++;}
    public void DwnStr()
    { strong--; }
    public void DwnInt()
    { intelligence--; }
    public void DwnAgi()
    { agility--; }

    public void UpHydratation(){
   	
 	currenthydratation += 10f;
   	hydratationBar.UpdateBar( currenthydratation, maxHydratation);
   	XenoPrefs.SetFloat("CurrentHydratation", StatsJoueur.currenthydratation);
   	XenoPrefs.Save();
	}
    public void SoinMaladieHostipal()
    {//Soin maladie pour X €€
        if (mort == 0 && malade == 1)
        {
            malade = 0;
            currentHealth += 15f;
            healthBar.UpdateBar(currentHealth, maxHealth);
            PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
            PlayerPrefs.Save();

        }
    }
    public void SoinHopital()
    {//Soin Health pour X €€
        if (mort == 0)
        {
            currentHealth += 30f;
            healthBar.UpdateBar(currentHealth, maxHealth);
            PlayerPrefs.SetFloat("CurrentHealth", StatsJoueur.currentHealth);
            PlayerPrefs.Save();
        }
    }
    public void ResurectionHopital()
    {
        if (mort == 1)
        {
            mort = 0;
            malade = 1;
            resurection = 1;
           
        }

    }
    public void Tempete()
    {
        malade = 1;

    }

    public void BOUTONMINIGAME()
    {
        GameObject g = GameObject.Find("Background-MiniGame");
        Animator AnimMenuMiniGame = g.GetComponent<Animator>();
        AnimMenuMiniGame.SetBool("Open", true);
    }
    public void BOUTONMINIGAMECLOSE()
    {
        GameObject g = GameObject.Find("Background-MiniGame");
        Animator AnimMenuMiniGame = g.GetComponent<Animator>();
        AnimMenuMiniGame.SetBool("Open", false);
    }

        public void Evolution(int ageDuPetEnJours)
    {
        //Calcul de jours restant avant l'evolution possible Niv 1
        PointAttribut = agility + intelligence + strong;
        

        //Palier a ateindre pour rank up
        //Niveau d'évolution 1
        if  (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "Non")
        {
            PointAttributRestantAAvoir = EvoPointLVL1 - PointAttribut; //      Coût en points d'attribut pour evolve-----------------------------------
           joursRestantavantEvolution = EvoDaysLVL1 - ageDuPetEnJours;//      Coût en jours pour evolve-----------------------------------------------
            joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + " jours. Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";

            if (PointAttribut <= EvoPointLVL1 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";
            }
            if (PointAttribut >= EvoPointLVL1 && joursRestantavantEvolution >= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + "jours";
            }

            if (PointAttribut >= EvoPointLVL1 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution disponible !";
                Evole_Button.SetActive(true);
                Debug.Log("On est ligne 552 ");

            }
        }

        //Niveau d'évolution 2
        else if(Evolve1 == "true" && Evolve2 == "Non" && Evolve3 == "Non")
        {
            PointAttributRestantAAvoir = EvoPointLVL2 - PointAttribut;//      Coût en points d'attribut pour evolve-----------------------------------
            joursRestantavantEvolution = EvoDaysLVL2 - ageDuPetEnJours;
            joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + " jours. Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";

            if (PointAttribut <= EvoPointLVL2 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";
            }
            if (PointAttribut >= EvoPointLVL2 && joursRestantavantEvolution >= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + "jours";
            }

            if (PointAttribut >= EvoPointLVL2 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution disponible !";
                Evole_Button.SetActive(true);
                Debug.Log("On est ligne 577 ");

            }
        }

        //Niveau d'évolution 3
        else if (Evolve1 == "Non" && Evolve2 == "true" && Evolve3 == "Non")
        {
            PointAttributRestantAAvoir = EvoPointLVL3 - PointAttribut;//      Coût en points d'attribut pour evolve-----------------------------------
            joursRestantavantEvolution = EvoDaysLVL3 - ageDuPetEnJours;
            joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + " jours. Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";

            if (PointAttribut <= EvoPointLVL3 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";
            }
            if (PointAttribut >= EvoPointLVL3 && joursRestantavantEvolution >= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + "jours";
            }

            if (PointAttribut >= EvoPointLVL3 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution disponible !";
                Evole_Button.SetActive(true);
                Debug.Log("On est ligne 602 ");

            }
        }
        //Niveau d'évolution 4 
        else if (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "true" && EvoMAX == "Non")
        {
            PointAttributRestantAAvoir = EvoPointLVL4 - PointAttribut;//      Coût en points d'attribut pour evolve-----------------------------------
            joursRestantavantEvolution = EvoDaysLVL4 - ageDuPetEnJours;
            joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + " jours. Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";

            if (PointAttribut <= EvoPointLVL4 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Prerequis " + PointAttributRestantAAvoir.ToString() + " points d'attributs";
            }
            if (PointAttribut >= EvoPointLVL4 && joursRestantavantEvolution >= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution dans " + joursRestantavantEvolution.ToString() + "jours";
            }

            if (PointAttribut >= EvoPointLVL4 && joursRestantavantEvolution <= 0)
            {
                joursRestantavantEvolutionText.text = "Evolution disponible !";
                Evole_Button.SetActive(true);

                Debug.Log("On est ligne 627 ");

            }
        }
        //Evolution maximale (sans skin) juste pour évité de relire la ligne du dessus...
        else if (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "true" && EvoMAX == "true")
        {
            joursRestantavantEvolutionText.text = "No Evolution for now, Support us for more contents ! ";
            Debug.Log("On est ligne 635 ");
        }
    }




    public void EvolutionByButton()
    {
        StartCoroutine("EvolveAPet");
        
        Evole_Button.SetActive(false);
    }

    public IEnumerator EvolveAPet()
    {
        EvolutionFX.SetActive(true);
        Renderer ColorPlayer = playerGameObject.GetComponentInChildren<Renderer>();
        ColorPlayer.sharedMaterial = White_Evo;

        yield return new WaitForSeconds(2);
        Destroy(playerGameObject);
        EvolutionFX.SetActive(false);
        // Fait apparaitre la bonne version du skin en question
        //Evolve Niveau 2
        if (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            {
                Evolve1 = "true";
                Evolve2 = "Non";
                Evolve3 = "Non";
                playerGameObject = Instantiate(DarkLVL2);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.Save();
            }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            {
                Evolve1 = "true";
                Evolve2 = "Non";
                Evolve3 = "Non";
                playerGameObject = Instantiate(AngelLVL2);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.Save();
            }
        }


        //Evolve Niveau 3
        else if (Evolve1 == "true" && Evolve2 == "Non" && Evolve3 == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            {
                Evolve1 = "Non";
                Evolve2 = "true";
                Evolve3 = "Non";
                playerGameObject = Instantiate(DarkLVL3);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.Save();
            }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            {
                Evolve1 = "Non";
                Evolve2 = "true";
                Evolve3 = "Non";
                playerGameObject = Instantiate(AngelLVL3);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.Save();
            }
        }
        //Evolve Niveau 4
        else if (Evolve1 == "Non" && Evolve2 == "true" && Evolve3 == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            {
                Evolve1 = "Non";
                Evolve2 = "Non";
                Evolve3 = "true";
                playerGameObject = Instantiate(DarkLVL4);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.Save();
                Evole_Button.SetActive(false);
            }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            {
                Evolve1 = "Non";
                Evolve2 = "Non";
                Evolve3 = "true";
                playerGameObject = Instantiate(AngelLVL4);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.SetString("EvolutionMax", EvoMAX);
                XenoPrefs.Save();
                Evole_Button.SetActive(false);
            }
        }
        //Evolve LVL 5 && Max
        else if (Evolve1 == "Non" && Evolve2 == "Non" && Evolve3 == "true" && EvoMAX == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            {
                EvoMAX = "true";
                playerGameObject = Instantiate(DarkLVL5);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.SetString("EvolutionMax", EvoMAX);
                XenoPrefs.Save();
                Evole_Button.SetActive(false);
            }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            {
                EvoMAX = "true";
                playerGameObject = Instantiate(AngelLVL5);
                XenoPrefs.SetString("Evolution1", Evolve1);
                XenoPrefs.SetString("Evolution2", Evolve2);
                XenoPrefs.SetString("Evolution3", Evolve3);
                XenoPrefs.SetString("EvolutionMax", EvoMAX);
                XenoPrefs.Save();
                Evole_Button.SetActive(false);
            }
        }
        

    }
    public void LOADSceneFishinggame()
    {
        SceneManager.LoadScene(2);
        


    }
}
