using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voyage : MonoBehaviour
{

    public static GameObject playerGameObject;
    
    public GameObject Angel;
    public GameObject AngelLVL2;
    public GameObject AngelLVL3;
    public GameObject AngelLVL4;
    public GameObject AngelLVL5;
    public GameObject Dark;
    public GameObject DarkLVL2;
    public GameObject DarkLVL3;
    public GameObject DarkLVL4;
    public GameObject DarkLVL5;

    public Material SkinPlayer;


    // Start is called before the first frame update
    void Awake()
    {

        //StatsJoueur.Evolve1 =
        // StatsJoueur.currentHealth =
        // StatsJoueur.intelligence =
        // StatsJoueur.strong =
        // StatsJoueur.agility =





        if (XenoPrefs.GetString("Evolution1", "0") == "Non" && XenoPrefs.GetString("Evolution2", "0") == "Non" && XenoPrefs.GetString("Evolution3", "0") == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(Dark); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { //playerGameObject = Instantiate(Angel,  new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity); 
           }
        }
        if (XenoPrefs.GetString("Evolution1", "0") == "true" && XenoPrefs.GetString("Evolution2", "0") == "Non" && XenoPrefs.GetString("Evolution3", "0") == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL2); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL2); }
        }
        if (XenoPrefs.GetString("Evolution1", "0") == "Non" && XenoPrefs.GetString("Evolution2", "0") == "true" && XenoPrefs.GetString("Evolution3", "0") == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL3); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL3); }
        }
        if (XenoPrefs.GetString("Evolution1", "0") == "Non" && XenoPrefs.GetString("Evolution2", "0") == "Non" && XenoPrefs.GetString("Evolution3", "0") == "true" && XenoPrefs.GetString("EvolutionMax", "0") == "Non")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL4); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL4); }
        }
        if (XenoPrefs.GetString("Evolution1", "0") == "Non" && XenoPrefs.GetString("Evolution2", "0") == "Non" && XenoPrefs.GetString("Evolution3", "0") == "true" && XenoPrefs.GetString("EvolutionMax", "0") == "true")
        {
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Dark")
            { playerGameObject = Instantiate(DarkLVL5); }
            if (XenoPrefs.GetString("CategorieDuPet", PlayerSelection.currentPlayer) == "Angel")
            { playerGameObject = Instantiate(AngelLVL5); }
        }


       // playerGameObject = Instantiate(Angel, Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
       // myNewSmoke.transform.parent = gameObject.transform;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
