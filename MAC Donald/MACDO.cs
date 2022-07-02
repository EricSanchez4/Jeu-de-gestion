using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MACDO : MonoBehaviour
{

    public GameObject eat1;
    public GameObject eat2;
    public GameObject eat3;
    public GameObject eat4;
    public GameObject purchaseButton;
    public GameObject NotEnoughtGold;
    public GameObject NotEnoughtHam;
    public GameObject NotEnoughtEgg;
    public GameObject NotEnoughtFish;

    private string choice;

    [Header("Ham Set")]
    public int EatHam;
    public int priceEat1;
    public int WaterGain1;

    [Header("Egg Set")]
    public int EatEgg;
    public int priceEat2;
    public int WaterGain2;

    [Header("Fish Set")]
    public int EatFish;
    public int priceEat3;
    public int WaterGain3;

    [Header("Watter Set")]
    public int priceEat4;
    public int WaterGain4;

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
    private int progressSTR;
    private int progressAGI;
    private int progressINT;
    private GameObject Gold;
    private Text TextGold;

   //Stock de nourriture Ganger en jouent aux mini jeux
    private int StockOfHam;
    private int StockOfEgg;
    private int StockOfFish;

    //Stock de nourriture recupéré par jours
    private int StockOfHamPerDay;
    private int StockOfEggPerDay;
    private int StockOfFishPerDay;

    public Text StockOfHamText;
    public Text StockOfEggText;
    public Text StockOfFishText; 








    // Start is called before the first frame update
    void Start()
    {
        
        purchaseButton.SetActive(false);
        var TextPriceEat1 = eat1.GetComponentInChildren<Text>() ;
        TextPriceEat1.text = priceEat1.ToString();
        var TextPriceEat2 = eat2.GetComponentInChildren<Text>();
        TextPriceEat2.text = priceEat2.ToString();
        var TextPriceEat3 = eat3.GetComponentInChildren<Text>();
        TextPriceEat3.text = priceEat3.ToString();
        var TextPriceEat4 = eat4.GetComponentInChildren<Text>();
        TextPriceEat4.text = priceEat4.ToString();
         Gold = GameObject.Find("Gold");
        TextGold = Gold.GetComponentInChildren<Text>();
        


    }

    // Update is called once per frame
    void Update()
    {
        TextGold.text = CurrentGold + " Gold";

       


        currentHealth = PlayerPrefs.GetFloat("CurrentHealth", StatsJoueur.currentHealth);
        currentstamina = XenoPrefs.GetFloat("CurrentStamina", 100);
        CurrentGold = XenoPrefs.GetInt("Gold", 0);
        CurrentEatMacDO = XenoPrefs.GetFloat("CurrentEat", 100);
        CurrentHydratationMacDO = XenoPrefs.GetFloat("CurrentHydratation", 100);

        str = XenoPrefs.GetInt("ForceDuPet", 0);
        Agi = XenoPrefs.GetInt("AgiliteDuPet", 0);
        Int = XenoPrefs.GetInt("IntelligenceDuPet", 0);
        progressSTR = XenoPrefs.GetInt("ProgressSTR", 0);
        progressAGI = XenoPrefs.GetInt("ProgressAGI", 0);
        progressINT = XenoPrefs.GetInt("ProgressINT", 0);

        StockOfHamPerDay = XenoPrefs.GetInt("FoodOfHamPerDay", 2);
        StockOfEggPerDay = XenoPrefs.GetInt("FoodOfEggPerDay", 2);
        StockOfFishPerDay = XenoPrefs.GetInt("FoodOfFishPerDay", 2);
        StockOfHam = XenoPrefs.GetInt("StockOfHam", 0);
        StockOfEgg = XenoPrefs.GetInt("StockOfEgg", 0);
        StockOfFish = XenoPrefs.GetInt("StockOfFish", 0);



        agilityTxt.text = Agi.ToString();
        strongTxt.text = str.ToString();
        intelligenceTxt.text = Int.ToString();

        
        int A = StockOfHam + StockOfHamPerDay;
        StockOfHamText.text = A.ToString();
       
        int B = StockOfEgg + StockOfEggPerDay;
        StockOfEggText.text = B.ToString();
       
        int C = StockOfFish + StockOfFishPerDay;
        StockOfFishText.text = C.ToString();


        //Mes scrollbar

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
        //Ham selected 
        purchaseButton.SetActive(true);
        choice = "Ham";


    }
    public void SelectionItem2()
    {
        //EGG selected 
        purchaseButton.SetActive(true);
        choice = "Egg";


    }
    public void SelectionItem3()
    {
        //Fish selected 
        purchaseButton.SetActive(true);
        choice = "Fish";


    }
    public void SelectionItem4()
    {
        //Water selected 
        purchaseButton.SetActive(true);
        choice = "Water";


    }

    public void PurchaseEat()
    {

        if (choice == "Ham")
        {
            int NbHam = StockOfHam + StockOfHamPerDay;
            if (CurrentGold - priceEat1 > 0 && NbHam > 0)
            {
                if (StockOfHamPerDay > 0)
                {
                    StockOfHamPerDay = StockOfHamPerDay - 1;
                    XenoPrefs.SetInt("FoodOfHamPerDay", StockOfHamPerDay);
                    XenoPrefs.Save();
                }
                else if (StockOfHamPerDay <= 0 && StockOfHam > 0)
                {
                    StockOfHam = StockOfHam - 1;
                    XenoPrefs.SetInt("StockOfHam", StockOfHam);
                    XenoPrefs.Save();

                }



                CurrentEatMacDO = CurrentEatMacDO + EatHam;
            CurrentGold = CurrentGold - priceEat1;
            CurrentHydratationMacDO = CurrentHydratationMacDO + WaterGain1;
            progressSTR = progressSTR + 1;
                if (progressSTR >= 5)
                {
                    progressSTR = 0;
                    str = str + 1;
                    XenoPrefs.SetInt("ForceDuPet", str);
                    XenoPrefs.SetInt("ProgressSTR", progressSTR);
                    XenoPrefs.Save();
                }
                XenoPrefs.SetInt("Gold", CurrentGold);
                XenoPrefs.SetFloat("CurrentEat", CurrentEatMacDO);
                XenoPrefs.SetFloat("CurrentHydratation", CurrentHydratationMacDO);
                XenoPrefs.SetInt("ProgressSTR", progressSTR);
                XenoPrefs.Save();
               
            }

            else if (CurrentGold - priceEat1 <= 0) // si je n'est pas assez d'argents pour achetté
            {
                NotEnoughtGold.SetActive(true);
            }
            else if (NbHam <= 0) // Si je n'est plus de Ham a achetté
            {
                NotEnoughtHam.SetActive(true);
            }

        }
        else if (choice == "Egg")
        {
            int NbEgg = StockOfEgg + StockOfEggPerDay;
            if (CurrentGold - priceEat2 > 0 && NbEgg > 0)
            {
                if (StockOfEggPerDay > 0)
                {
                    StockOfEggPerDay = StockOfEggPerDay - 1;
                    XenoPrefs.SetInt("FoodOfEggPerDay", StockOfEggPerDay);
                    XenoPrefs.Save();
                }
                else if (StockOfEggPerDay <= 0 && StockOfEgg > 0)
                {
                    StockOfEgg = StockOfEgg - 1;
                    XenoPrefs.SetInt("StockOfEgg", StockOfEgg);
                    XenoPrefs.Save();

                }

                CurrentEatMacDO = CurrentEatMacDO + EatEgg;
                CurrentGold = CurrentGold - priceEat2;
                CurrentHydratationMacDO = CurrentHydratationMacDO + WaterGain2;
                progressAGI = progressAGI + 1;
                if (progressAGI >= 5)
                {
                    progressAGI = 0;
                    Agi = Agi + 1;
                    XenoPrefs.SetInt("AgiliteDuPet", Agi);
                    XenoPrefs.SetInt("ProgressAGI", progressAGI);
                    XenoPrefs.Save();
                }
                XenoPrefs.SetInt("Gold", CurrentGold);
                XenoPrefs.SetFloat("CurrentEat", CurrentEatMacDO);
                XenoPrefs.SetFloat("CurrentHydratation", CurrentHydratationMacDO);
                XenoPrefs.SetInt("ProgressAGI", progressAGI);
                XenoPrefs.Save();
            }

            else if (CurrentGold - priceEat2 <= 0) // si je n'est pas assez d'argents pour achetté
            {
                NotEnoughtGold.SetActive(true);
            }
            else if (NbEgg <= 0) // Si je n'est plus de Egg a achetté
            {
                NotEnoughtEgg.SetActive(true);
            }
        }
        else if (choice == "Fish")
        {
            int NbFish = StockOfFish + StockOfFishPerDay;
            if (CurrentGold - priceEat3 > 0 && NbFish > 0)
            {
                if (StockOfFishPerDay > 0) {
                    StockOfFishPerDay = StockOfFishPerDay  - 1;
                    XenoPrefs.SetInt("FoodOfFishPerDay", StockOfFishPerDay);
                    XenoPrefs.Save();
                }
                else if (StockOfFishPerDay <= 0 && StockOfFish > 0)
                {
                    StockOfFish = StockOfFish - 1;
                    XenoPrefs.SetInt("StockOfFish", StockOfFish);
                    XenoPrefs.Save();

                }


                CurrentEatMacDO = CurrentEatMacDO + EatFish;
                CurrentGold = CurrentGold - priceEat3;
                CurrentHydratationMacDO = CurrentHydratationMacDO + WaterGain3;
                progressINT = progressINT + 1;
                if (progressINT >= 5)
                {
                    progressINT = 0;
                    Int = Int + 1;
                    XenoPrefs.SetInt("IntelligenceDuPet", Int);
                    XenoPrefs.SetInt("ProgressINT", progressINT);
                    XenoPrefs.Save();
                }
                XenoPrefs.SetInt("Gold", CurrentGold);
                XenoPrefs.SetFloat("CurrentEat", CurrentEatMacDO);
                XenoPrefs.SetFloat("CurrentHydratation", CurrentHydratationMacDO);
                XenoPrefs.SetInt("ProgressINT", progressINT);
                XenoPrefs.Save();

            }

            else if (CurrentGold - priceEat3 <= 0) // si je n'est pas assez d'argents pour achetté
            {
                NotEnoughtGold.SetActive(true);
            }
            else if (NbFish <= 0) // Si je n'est plus de poissons a achetté
            {
                NotEnoughtFish.SetActive(true);
            }

        }
        else if (choice == "Water")
        {
            if (CurrentGold - priceEat4 > 0)
            {
                CurrentGold = CurrentGold - priceEat4;
                CurrentHydratationMacDO = CurrentHydratationMacDO + WaterGain4;
                
                XenoPrefs.SetInt("Gold", CurrentGold);
                XenoPrefs.SetFloat("CurrentHydratation", CurrentHydratationMacDO);
                XenoPrefs.Save();
            }

            else
            {
                NotEnoughtGold.SetActive(true);
            }
        }







    }

}
