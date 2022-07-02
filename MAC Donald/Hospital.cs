using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hospital : MonoBehaviour
{

    public GameObject Heal1;
    public GameObject Heal2;
    public GameObject Heal3;

    public GameObject purchaseButton;
    public GameObject NotEnoughtGold;
    public GameObject HealFull;
    public GameObject TextDejaMort;
    public GameObject NoREZinLife;
    public GameObject NoStaminaUpFull;

    private string choice;

    [Header("Heal Set")]
    public int healGain;
    public int priceHeal;

    [Header("Ressurrection Set")]
    private string etatDuJoueur;
    public int HealAfterRez;
    public float staminaAfterRez;
    public int priceRes;


    [Header("Stamina Set")]
    public int StaminaGain;
    public int priceStamina;



    [Header("ScrollBar Settings")]
    private float maxHealth = 100.0f;
    public static float currentHealth;
    public SimpleHealthBar healthBar;

    private float maxStamina = 100.0f;
    private static float currentstamina;
    public SimpleHealthBar staminaBar;

    private float maxEat = 100f;
    
    public SimpleHealthBar eatBar;

    private float maxHydratation = 100.0f;
    
    public SimpleHealthBar hydratationBar;

    [Header("Caractéristiques")]//Var caractères
    
    public Text agilityTxt;
    public Text strongTxt;
    public Text intelligenceTxt;

    //Var
    private float CurrentEatMacDO;
    private float CurrentHydratationMacDO;
    private int CurrentGold;
    private int str;
    private int Agi;
    private int Int;

    private GameObject Gold;
    private Text TextGold;









    // Start is called before the first frame update
    void Start()
    {
        
        purchaseButton.SetActive(false);
        var TextPriceEat1 = Heal1.GetComponentInChildren<Text>() ;
        TextPriceEat1.text = priceHeal.ToString();
        var TextPriceEat2 = Heal2.GetComponentInChildren<Text>();
        TextPriceEat2.text = priceRes.ToString();
        var TextPriceEat3 = Heal3.GetComponentInChildren<Text>();
        TextPriceEat3.text = priceStamina.ToString();
         Gold = GameObject.Find("Gold");
        TextGold = Gold.GetComponentInChildren<Text>();
        


    }

    // Update is called once per frame
    void Update()
    {
        TextGold.text = CurrentGold + " Gold";
        
        etatDuJoueur =  XenoPrefs.GetString("EtatDuJoueur", "Mort");
        currentHealth = PlayerPrefs.GetFloat("CurrentHealth", StatsJoueur.currentHealth);
        currentstamina = XenoPrefs.GetFloat("CurrentStamina", 100);
        CurrentGold = XenoPrefs.GetInt("Gold", 0);
        CurrentEatMacDO = XenoPrefs.GetFloat("CurrentEat", 100);
        CurrentHydratationMacDO = XenoPrefs.GetFloat("CurrentHydratation", 100);

        str = XenoPrefs.GetInt("ForceDuPet", 0);
        Agi = XenoPrefs.GetInt("AgiliteDuPet", 0);
        Int = XenoPrefs.GetInt("IntelligenceDuPet", 0);

        strongTxt.text = str.ToString();
        agilityTxt.text = Agi.ToString();
        intelligenceTxt.text = Int.ToString();
        //Limite des ScrollBar

        //Stamina
        if (currentstamina > 100.00f)
        { //Max Stamina
            currentstamina = 100.00f;
        }
        if (currentstamina < 00.00f)
        {  //Min Stamina
            currentstamina = 00.00f;
        }
      
        //EAT
        if (CurrentEatMacDO > 100.00f)
        { //Max Eat
            CurrentEatMacDO = 100.00f;
        }
        if (CurrentEatMacDO <= 00.00f) //Min Eat
        {
            CurrentEatMacDO = 00.00f;
        }
        

        

        //Hydratation
        if (CurrentHydratationMacDO > 100.00f)
        { // Max Hydratation
            CurrentHydratationMacDO = 100.00f;
        }
        if (CurrentHydratationMacDO < 00.00f)
        { // Min Hydratation
            CurrentHydratationMacDO = 00.00f;
        }

        //Health
        if (currentHealth > 100.00f)
        { //Max Health
            currentHealth = 100.00f;
        }

        if (currentHealth <= 00.00f)
        { // Min Health 
            currentHealth = 00.00f;
        }

        //MAJ de barres Continuellement
        healthBar.UpdateBar(currentHealth, maxHealth);
        staminaBar.UpdateBar(currentstamina, maxStamina);
        eatBar.UpdateBar(CurrentEatMacDO, maxEat);
        hydratationBar.UpdateBar(CurrentHydratationMacDO, maxHydratation);
    }
    public void SelectionItem1 ()
    {
        //Heal selected 
        purchaseButton.SetActive(true);
        choice = "Heal";


    }
    public void SelectionItem2()
    {
        //EGG selected 
        purchaseButton.SetActive(true);
        choice = "Rez";


    }
    public void SelectionItem3()
    {
        //Stamina selected 
        purchaseButton.SetActive(true);
        choice = "Stamina";
    }

    public void PurchaseEat() // Actif lorsque le player appui sur purchase
    {
        if (choice == "Heal") // Heal et soin malade
        {
            if (CurrentGold - priceHeal > 0 && currentHealth > 0)
            {
                currentHealth = healGain;
                CurrentGold = CurrentGold - priceHeal;
                etatDuJoueur = "Heureux";
                XenoPrefs.SetString("EtatDuJoueur", etatDuJoueur);
                PlayerPrefs.SetFloat("CurrentHealth", currentHealth);
                XenoPrefs.SetInt("Gold", CurrentGold);
                PlayerPrefs.Save();
                XenoPrefs.Save();
            }
            else if (CurrentGold - priceHeal <= 0) // si je n'est pas assez d'argents pour achetté
            {
                NotEnoughtGold.SetActive(true);
            }
            else if (currentHealth == 100) // Si je n'est plus de Heal a achetté
            {
                HealFull.SetActive(true);
            }
            else if (currentHealth == 0) // Si le joueur est mort
            {
                TextDejaMort.SetActive(true);
            }

        }
        else if (choice == "Rez") // REZ
        {
            if (CurrentGold - priceRes > 0 && etatDuJoueur == "Mort")
            {
                CurrentGold = CurrentGold - priceRes;
                etatDuJoueur = "Malade";
                currentHealth = HealAfterRez;
                currentstamina =staminaAfterRez;
                CurrentEatMacDO = 00.00f;
                CurrentHydratationMacDO = 00.00f;
               
                PlayerPrefs.SetFloat("CurrentHealth", currentHealth);
                XenoPrefs.SetString("EtatDuJoueur", "Malade");
                XenoPrefs.SetFloat("CurrentStamina", currentstamina);
                XenoPrefs.SetFloat("CurrentEat", CurrentEatMacDO);
                XenoPrefs.SetFloat("CurrentHydratation", CurrentHydratationMacDO);
                XenoPrefs.SetInt("Gold", CurrentGold);
                PlayerPrefs.Save();
                XenoPrefs.Save();
            }
            else if (CurrentGold - priceRes <= 0) // si je n'est pas assez d'argents pour achetté
            {
                NotEnoughtGold.SetActive(true);
            }
            else if (currentHealth > 0) // Si le joueur n'est pas mort
            {
                NoREZinLife.SetActive(true);
            }
        }
        else if (choice == "Stamina")// Stamina Up
        {
          
            if (CurrentGold - priceStamina > 0 && currentstamina < 100)
            {
                CurrentGold = CurrentGold - priceStamina;
                currentstamina =  StaminaGain;

                XenoPrefs.SetInt("Gold", CurrentGold);
                XenoPrefs.SetFloat("CurrentStamina", currentstamina);
                XenoPrefs.Save();
            }

            else if (CurrentGold - priceStamina <= 0) // si je n'est pas assez d'argents pour achetté
            {
                NotEnoughtGold.SetActive(true);
            }
            else if (currentstamina == 100) // Si la stamina est deja full
            {
                NoStaminaUpFull.SetActive(true);
            }
        }
    }
}


