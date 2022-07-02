using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fermier : MonoBehaviour
{

    public Collider colHitbox;
    public float farmerspeedmodmanu;
    public static float FarmerSpeed;
    public Animator animFermier; // Anim du fermier qui tombe a mettre
    public static int AddStockOfHam;
    private NavMeshAgent NavMeshAgentFermier;
    public GameObject OrbeASuivre;
    public GameObject OrbeDeFin;
    private GameObject GOCamera;
    public Collider ColFinish;
    private List<NavMeshAgent> NavAnimaux = new List<NavMeshAgent>();
    public GameObject[] Orbes;
    public GameObject[] OrbesFin;
    private GameObject[] MesAnimaux;
    private bool Animaux;
    private int o;
    private bool Finish = false;

    void Start()
    {
        GOCamera = GameObject.Find("Main Camera");
        
        FarmerSpeed = farmerspeedmodmanu;
        NavMeshAgentFermier = this.gameObject.GetComponent<NavMeshAgent>();
        NavMeshAgentFermier.SetDestination(OrbeASuivre.transform.position);

        
    }

    void Update()
    {
        
        NavMeshAgentFermier.speed = FarmerSpeed;
        NavMeshAgentFermier.SetDestination(OrbeASuivre.transform.position);

        //Correspondance clavier

        if (Input.GetKeyDown(KeyCode.RightArrow)) { MoveFarmerRight(); }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { MoveFarmerLeft(); }

        if (NavMeshAgentFermier.remainingDistance <= 4) { // distance entre le fermier et l'orbe
            if(Finish == false) {
                OrbeASuivre.transform.Translate(new Vector3(0, 0, FarmerSpeed * Time.deltaTime));
            }
            
        }
        if(GOCamera.transform.position.z < (this.transform.position.z - 5f))// distance entre le fermier et la camera
        {
            GOCamera.transform.position = new Vector3(GOCamera.transform.position.x, GOCamera.transform.position.y, GOCamera.transform.position.z + (FarmerSpeed + 0.5f) * Time.deltaTime);
        }
         if(Animaux)// si j'ai des animaux qui me poursuivent
        {
            // ordre les orbes a suivre (animaux)
            foreach(NavMeshAgent i in NavAnimaux)
            {
                if (Finish == false) {
                    i.SetDestination(Orbes[o].transform.position);
                    o = o + 1;
                    if (o >= 8) { o = 0; }
                    
                }
                
                
                if (Finish)
                {
                    i.SetDestination(OrbesFin[o].transform.position); // Les animaux vont vers les points de la fin
                    //i.stoppingDistance = 1f;
                    o = o + 1;
                    if (o >= 8) { o = 0; }
                    bool isfinish = false;
                    float Disparcou;
                    if (i.remainingDistance <= 2.5 || isfinish) // lorsque les animaux arrive sur leurs points de fin ils se tourne vers la camera
                    {
                        isfinish = true;
                        i.SetDestination(GOCamera.transform.position);
                        
                        Disparcou = i.remainingDistance;
                        if (Disparcou >= 4)
                        {
                            i.speed = 0f;
                        }
                        
                    }
                        }
            }
        }

    }

    public void MoveFarmerLeft()
    {
         if(OrbeASuivre.transform.position.x > 209 && OrbeASuivre.transform.position.x <= 217)
        {
          OrbeASuivre.transform.position = new Vector3(OrbeASuivre.transform.position.x -2, OrbeASuivre.transform.position.y, OrbeASuivre.transform.position.z) ;
            
        }


    }

    public void MoveFarmerRight()
    {

       if (OrbeASuivre.transform.position.x >= 207 && OrbeASuivre.transform.position.x < 215)
        {
            OrbeASuivre.transform.position = new Vector3(OrbeASuivre.transform.position.x + 2, OrbeASuivre.transform.position.y, OrbeASuivre.transform.position.z);
            
        }

    }
    

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bonus") // si je touche une vache
        {
            
            NavAnimaux.Add(collision.gameObject.GetComponent<NavMeshAgent>());
            collision.gameObject.tag = "Followers";
            Animaux = true;

            //suppression du collide de l'animal pour compte 1
            FERME.score = FERME.score + 30;
            AddStockOfHam = AddStockOfHam + 1;
            int StockOfHam = XenoPrefs.GetInt("StockOfHam", 0);
            XenoPrefs.SetInt("StockOfHam", StockOfHam + AddStockOfHam);

            Debug.Log("Ajout de " + AddStockOfHam + " HAM sur le stock presents de " + StockOfHam);
            
        }

        if (collision.gameObject.tag == "fish+1") //si je touche un animal de la ferme
        {
            
            FERME.score = FERME.score + 10;
            NavAnimaux.Add(collision.gameObject.GetComponent<NavMeshAgent>());
            collision.gameObject.tag = "Followers";
            Animaux = true;

        }

        if (collision.gameObject.tag == "Enemie") // si je touche un enemy de merde
        {
            animFermier.SetBool("Dead", true);
            collision.gameObject.tag = "Followers";
            FERME.Live = 0;
            colHitbox.enabled = false;
            //AudioPomme.Play(); // jouer le son quand il tombe // on peux event son animation
            Destroy(gameObject, 2.0f);

            //Attendre un 3 secondes et lancé le menu de mort


        }
        if (collision.gameObject.tag == "Coin") // si j'attrape une piece
        {
            FERME.Coin = FERME.Coin  +1;
            collision.gameObject.tag = "Followers";

            //AudioPomme.Play(); // Son pour pik up piece

        }

        if (collision.gameObject.tag == "Finish") // si je touche la ligne d'arrivée
        {

            Finish = true;

            

        }

    }
}
