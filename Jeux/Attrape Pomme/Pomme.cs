using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pomme : MonoBehaviour
{
    public static float vitessePomme = 0.8f;
    public Collider MonCol;
    public Animator animPomme;
    public AudioSource AudioPomme;
    public AudioSource AudioPommeFail;
    public static int AddStockOfEgg = 0;


    void Update()
    {
        transform.Translate(new Vector3(0, -vitessePomme * Time.deltaTime, 0));

    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            animPomme.SetBool("Dead", true);
            MonCol.enabled = false;
            Destroy(gameObject, 0.3f);
            AudioPomme.Play();


            if (this.tag == "Bonus")
            {
                AddStockOfEgg = AddStockOfEgg + 1;
               int StockOfEgg = XenoPrefs.GetInt("StockOfEgg", 0);
                XenoPrefs.SetInt("StockOfEgg", StockOfEgg + AddStockOfEgg);

                Debug.Log("Ajout de " + AddStockOfEgg + " Oeuf sur le stock presents de " + StockOfEgg);
            }
            else if(this.tag == "fish+1")
            {
                PanierJoueur.score = PanierJoueur.score + 1;
                
            }
            else if (this.tag == "pomme +3")
            {
                PanierJoueur.score = PanierJoueur.score + 3;

            }

        }


        if (collision.gameObject.name == "Deadline")
        {
            MonCol.enabled = false;
           AudioPommeFail.Play();
            PanierJoueur.Live--;
            Destroy(gameObject, 2.0f);
            
        }
    }
    
}