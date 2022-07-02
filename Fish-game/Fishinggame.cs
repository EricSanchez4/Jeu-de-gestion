using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fishinggame : MonoBehaviour
{

    public LayerMask FishMask;
    private Vector3 hitPoint;
    public GameObject fxHitFishPrefab;
    private Animator animFish;
    private GameObject MenuGameOver;

    public static int score = 0;
    public static int Live = 20;
    private NavMeshAgent poissonMort;

    public Collider FinishFish;
    public Text scoreText;
    public Text LiveText;
    private AudioSource FishSplouf;

    public GameObject fishPrefab;
    public GameObject fishPrefabRed;
    public GameObject BonusPrefab;


    public float wavetime;
    private float moreSpeed = 6;
    private float moreAngular = 160;
    private float moreacceleration =8 ;
    private float rr = 80; //5 chance sur 80 d'avoir un poisson rouge' 80 = valeur de base
    private  bool GameOver = false;


    private int StockOfFish;
    public Text FishTextMenu;
    public Image FishImg;

    // faire des poisson spécial
    // récompence au retour de missons a sauvegardé
    private void Awake()
    {

        MenuGameOver = GameObject.Find("MenuGameOver");
        MenuGameOver.SetActive(false);
    }

    void Start()
    {
        FishSplouf = this.GetComponent<AudioSource>();
        StartCoroutine(Debutdejeu());
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = "Score : " + score.ToString();
        LiveText.text = " Lives : " + Live.ToString();

        
        if (score < 0) { score = 0; }
        

        if (Live <=0) // GAME OVER
        {
            
            Live = 0;
            if (GameOver == false)
            {
                GameOver = true;
            Debug.Log("Game Over");
            StopCoroutine(Debutdejeu());
            //Time.timeScale = 0f;
            
                MenuGameOver.SetActive(true);
                GameObject TextGold =  GameObject.Find("TextGold");
               Text stringGold = TextGold.GetComponent<Text>();

               GameObject TextScore = GameObject.Find("TextScore");
               Text stringScore = TextScore.GetComponent<Text>();


                int Gold = score / 6;
                int AddStockOfFish = score / 200; 
                if (AddStockOfFish > 0)
                {
                    StockOfFish = XenoPrefs.GetInt("StockOfFish", 0);
                    XenoPrefs.SetInt("StockOfFish", StockOfFish +  AddStockOfFish);
                    Debug.Log("Ajout de " + AddStockOfFish + " poissons sur le stock presents de " + StockOfFish);
                }
                if (AddStockOfFish > 0)
                {
                    FishTextMenu.gameObject.SetActive(true);
                    FishImg.gameObject.SetActive(true);
                    FishTextMenu.text = ": " + AddStockOfFish.ToString() + " Found";
                }
                else if (AddStockOfFish == 0)
                {
                    FishTextMenu.gameObject.SetActive(false);
                    FishImg.gameObject.SetActive(false);
                }


                stringGold.text = "You earned " + Gold.ToString() + " Gold";
                stringScore.text = "Score : " + score.ToString();

                
               int Ancientgold = XenoPrefs.GetInt("Gold", 0);
                int newGold = Gold + Ancientgold;
                XenoPrefs.SetInt("Gold", newGold);
                XenoPrefs.Save();

                score = 0;
                ///faire une menu avec X 2Gold  si publicité

                
              
               
            }





        }
        else { //le jeu commence 

            

            TheFish1.fishAgent.speed = moreSpeed;
            TheFish1.fishAgent.acceleration = moreacceleration;
            TheFish1.fishAgent.angularSpeed = moreAngular;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //reconstruit le vecteur direction en 3D depuis l'écran vers le monde
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, FishMask))// Si ma souris touche un poisson
            {


           
             hitPoint = new Vector3(hit.point.x, 1.2f, hit.point.z);
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            destroyObject(hit.transform.gameObject);
            
            
            if (hit.transform.gameObject.tag != "Bonus")
            {
                poissonMort = hit.transform.gameObject.GetComponent<NavMeshAgent>();
                poissonMort.speed = 0f;
                poissonMort.angularSpeed = 0f;
                
            }
            animFish = hit.transform.gameObject.GetComponent<Animator>();
            animFish.SetBool("Dead", true);

            hit.transform.gameObject.layer = 0; //supp du layer pour touché qu'une fois


            }
        }
    }


     void destroyObject(GameObject objectToDestroy)
    {       

        // Ici, je trie mes différents poissons, poisson+1 (classic); les rouge(negatif); les bonus(pieces).
        if   (objectToDestroy.tag == "fish+1") { 
        GameObject hitFish = Instantiate(fxHitFishPrefab, hitPoint, new Quaternion(90f, 0f,0f,1)) as GameObject;
        FishSplouf.Play();
        int i = 0;
             if (i == 0){
            i++;
            score ++;
            Debug.Log("+1 du poison");
            Destroy(hitFish, 5.5f);
            Destroy(objectToDestroy, 1f);
            
             }
        }
        else if (objectToDestroy.tag == "Enemie")
        {
            GameObject hitFish = Instantiate(fxHitFishPrefab, hitPoint, new Quaternion(90f, 0f, 0f, 1)) as GameObject;
            FishSplouf.Play();
            int i = 0;
            if (i == 0)
            {
                i++;
                Live = Live - 2;
                score = score - 20;
                Debug.Log("-20 de score & -2 lives");
                Destroy(hitFish, 5.5f);
                Destroy(objectToDestroy, 1f);
                XenoPrefs.SetString("EtatDuJoueur", "Malade");

            }
        }
        else if (objectToDestroy.tag == "Bonus")
        {
            GameObject hitFish = Instantiate(fxHitFishPrefab, hitPoint, new Quaternion(90f, 0f, 0f, 1)) as GameObject;
            AudioSource B = objectToDestroy.GetComponent<AudioSource>();
            B.Play();


            int i = 0;
            if (i == 0)
            {
                i++;
                score = score + 20;


                TheFish1.fishAgent.speed = 6;
                moreSpeed = 6;
                moreacceleration = 8;
                moreAngular = 60;

                rr = 50;
                
                Debug.Log("Bonus trouvé");
                Destroy(hitFish, 5.5f);
                Destroy(objectToDestroy, 1f);

            }
        }

    }

 IEnumerator Debutdejeu()
    {
        float r = 0;
         
        while (Live >= 1) {

           
           
                Vector3 spawnPos = new Vector3(Random.Range(38,32), 0.02f, Random.Range(45, 21));

                float random = Random.Range(r, rr);
            if (random > 5)
            { GameObject poissonLent = Instantiate(fishPrefab, spawnPos, Quaternion.identity); // poisson + 1 point
            } 
            else if (random >0.5 && random <=5)  
            { Instantiate(fishPrefabRed, spawnPos, Quaternion.identity); } // poisson rouge - 2 vie
            else if (random <= 0.5)
            {
                spawnPos.y = spawnPos.y + (0.4f);
                Instantiate(BonusPrefab, spawnPos, new Quaternion(0f, 50f, 90f, 1f)); } // Bonus spawn



            if (wavetime > 0.4f) { 

                wavetime = wavetime - 0.04f;
                if (rr > 15)                        // 15 = 33% de chance de faire spawn un poisson rouge
                {
                    rr = rr - 0.6f;                 //// cette ligne veut diminué la chance que des bleu apparaissent
                }
                
            }
            else
            {
                    if (rr > 15) {                  // 15 = 33% de chance de faire spawn un poisson rouge
                    rr = rr - 0.6f;                 // cette ligne veut diminué la chance que des bleu apparaissent
                    }
                   
                moreSpeed = moreSpeed + 0.5f;
                moreacceleration = moreacceleration + 0.5f;
                moreAngular = moreAngular + 0.8f;





                
            }



        yield return new WaitForSeconds (wavetime);

        }
    }
   public void Restart()
        {
        Live = 20;
        GameOver = false;
        StartCoroutine(Debutdejeu());
        MenuGameOver.SetActive(false);
        TheFish1.fishAgent.speed = 6;
        moreSpeed = 6;
        moreacceleration = 8;
        moreAngular = 60;
        rr = 80;

    }

    public void QuitFishingGame()
        {
        SceneManager.LoadScene(1);
        }

}
