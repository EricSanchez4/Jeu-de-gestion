using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanierJoueur : MonoBehaviour
{
    
    private GameObject MenuGameOver;
    public static int score = 0;
    public static int Live = 3;
    public Text scoreText;
    public Text LiveText;
    public GameObject PommePrefab;
    public GameObject PommePrefabJaune;
    public GameObject EGGBonusPrefab;
    public float wavetime;
    private float rr = 100f; // Pourcentage de chance maximal a obtenir des truc bien / mieux
    public bool GameOver = false;
    private int StockOfEgg;
    public GameObject Deadline; // je veux la supprimer quand je suis en GameOver pour evité le bruit de merde
    private Collider ColJoueur; // pour le desactive lorsque je suis en gameOver
    private GameObject Panier; // mon panier Gameobject (pour le déplacer)
    public Text EggText;
    public Image EggImg;

    



    private void Awake()
    {
        MenuGameOver = GameObject.Find("MenuGameOver");
        MenuGameOver.SetActive(false);
        ColJoueur = this.GetComponent<BoxCollider>();
        Panier = this.gameObject;
    }


    private void Start()
    {
        StartCoroutine(Debutdejeu());

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score.ToString();
        LiveText.text = " Lives : " + Live.ToString();
       

        if (score < 0) { score = 0; }


        if (Live <= 0) // GAME OVER
        {

            Live = 0;
            if (GameOver == false)
            {
                ColJoueur.enabled = false;
                Deadline.SetActive(false);
                GameOver = true;
                Debug.Log("Game Over");
                StopCoroutine(Debutdejeu());
                //Time.timeScale = 0f;

                MenuGameOver.SetActive(true);
                GameObject TextGold = GameObject.Find("TextGold");
                Text stringGold = TextGold.GetComponent<Text>();

                GameObject TextScore = GameObject.Find("TextScore");
                Text stringScore = TextScore.GetComponent<Text>();


                int Gold = score / 6;

                stringGold.text = "You earned " + Gold.ToString() + " Gold";
                stringScore.text = "Score : " + score.ToString();
                if(Pomme.AddStockOfEgg > 0)
                {
                    EggText.gameObject.SetActive(true);
                    EggImg.gameObject.SetActive(true);
                    EggText.text = ": " + Pomme.AddStockOfEgg.ToString() + " Found";
                }
                else if (Pomme.AddStockOfEgg == 0)
                {
                    EggText.gameObject.SetActive(false);
                    EggImg.gameObject.SetActive(false);
                }


                int Ancientgold = XenoPrefs.GetInt("Gold", 0);
                int newGold = Gold + Ancientgold;
                XenoPrefs.SetInt("Gold", newGold);
                XenoPrefs.Save();

                score = 0;
                //faire une menu avec X 2Gold  si publicité

            }
        }
        else // Si mon jeu tourne (Live > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //reconstruit le vecteur direction en 3D depuis l'écran vers le monde
             RaycastHit hit;



            if (Physics.Raycast(ray, out hit, 1000f))// Si ma souris ce déplace 
            {
                Panier.transform.position = new Vector3(hit.point.x, -1.47f, -0.28f); // Déplace mon panier
            }
            
        }
    }


    IEnumerator Debutdejeu()
    {
        float r = 0;

        while (Live >= 1)
        {



            Vector3 spawnPos = new Vector3(Random.Range(-3.17f, 3.22f), Random.Range(3.9f, 5.35f), -0.225f); // ma zone de spawn

            float random = Random.Range(r, rr); // r = 0 rr = maxi a avoir donc chaque wavetime passé je retire un up de rr pour que le maximun a ateindre soit moindre
                                                //  donc les chance d'avoir des bonus et des pomme jaune POP sont plus grandes
            if (random > 6)
            {
                GameObject poissonLent = Instantiate(PommePrefab, spawnPos, Quaternion.identity); // pomme + 1 score
            }
            else if (random > 0.25 && random <= 6)
            { Instantiate(PommePrefabJaune, spawnPos, Quaternion.identity); } // Pomme Jaune + 3 score
            else if (random <= 0.25)
            {
               
                Instantiate(EGGBonusPrefab, spawnPos, Quaternion.identity);
            } // Bonus spawn



            if (wavetime > 0.35f)
            {

                wavetime = wavetime - 0.018f;
                Pomme.vitessePomme = Pomme.vitessePomme + 0.003f;
                if (rr > 15)                        // 15 = 33% de chance de faire spawn un poisson rouge
                {
                    rr = rr - 0.6f;                 //// cette ligne veut diminué la chance que des pomme classique apparaissent

                }

            }
            else
            {
                if (rr <= 15)
                {                  // 15 = 33% de chance de faire spawn une pomme Jaune
                    rr = rr - 0.06f;                 // cette ligne veut diminué la chance que des bleu apparaissent
                }
                if (rr <= 6)
                {                  // 15 = 33% de chance de faire spawn une pomme Jaune
                                     // cette ligne veut diminué la chance que des bleu apparaissent
                    Pomme.vitessePomme = Pomme.vitessePomme + 0.03f;
                }

                Pomme.vitessePomme = Pomme.vitessePomme + 0.06f;
            }
            
            yield return new WaitForSeconds(wavetime);

        }
    }

    public void Restart()
    {
        Live = 3;
        GameOver = false;
        ColJoueur.enabled = true;
        Deadline.SetActive(true);
        StartCoroutine(Debutdejeu());
        MenuGameOver.SetActive(false);
        Pomme.vitessePomme = 0.6f;
        rr = 80;

    }

    public void QuitFishingGame()
    {
        SceneManager.LoadScene(6);
    }
}
