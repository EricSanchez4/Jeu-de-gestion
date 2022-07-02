using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FERME : MonoBehaviour
{
    private GameObject MenuGameOver;
    public static int score = 0;
    public static int Live = 1;
    public Text scoreText;
    public GameObject[] MesAnimaux;
    public GameObject Vache;
    public GameObject Mur;
    public GameObject Loup;
    public GameObject CoinGameObject;
    public float wavetime;
    private float rr = 100f; // Pourcentage de chance maximal a obtenir des truc bien / mieux
    public bool GameOver = false;
    private int StockOfEgg;
    public Text CoinText;
    public static int Coin;
    public Text HamText;
    public Image HamImg;
    private int HamObtain;
    private int i;
    private GameObject GOpasage, GOpasage1;
    private int Stage;

    public GameObject[] Barrage1, Barrage2, Barrage3, Barrage4, Barrage5, Barrage6, Barrage7, Barrage8, Barrage9, Barrage10;
    public List<GameObject[]> BarListGO = new List<GameObject[]>();
    private List<int> Barrage = new List<int>();


    





    private void Awake()
    {
        MenuGameOver = GameObject.Find("MenuGameOver");
        MenuGameOver.SetActive(false);
        
       
    }


    private void Start()
    {
        
        StartCoroutine(Debutdejeu());
        

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score.ToString();
        CoinText.text =  Coin.ToString();
        HamText.text = ": " + Fermier.AddStockOfHam + " found";

        if (score < 0) { score = 0; }


        if (Live <= 0) // GAME OVER
        {

            Live = 0;
            if (GameOver == false)
            {
                GameOver = true;
                Debug.Log("Game Over");
                StopCoroutine(Debutdejeu());
                //Time.timeScale = 0f;

                MenuGameOver.SetActive(true);
                GameObject TextGold = GameObject.Find("TextGold");
                Text stringGold = TextGold.GetComponent<Text>();

                GameObject TextScore = GameObject.Find("TextScore");
                Text stringScore = TextScore.GetComponent<Text>();


                int Gold = score / 2;

                stringGold.text = "You earned " + Gold.ToString() + " Gold";
                stringScore.text = "Score : " + score.ToString();
                int Ancientgold = XenoPrefs.GetInt("Gold", 0);
                int newGold = Gold + Ancientgold;
                XenoPrefs.SetInt("Gold", newGold);
                XenoPrefs.Save();

                score = 0;
                //faire une menu avec X 2Gold  si publicité

            }
        }

    }


    IEnumerator Debutdejeu()
    {
        float time;
        time = Time.time + wavetime;

       
        BarListGO.Add(Barrage1); BarListGO.Add(Barrage2); BarListGO.Add(Barrage3); BarListGO.Add(Barrage4); BarListGO.Add(Barrage5); BarListGO.Add(Barrage6); BarListGO.Add(Barrage7); BarListGO.Add(Barrage8); BarListGO.Add(Barrage9); BarListGO.Add(Barrage10);
        while (Live >= 1 && time <= Time.time + wavetime && Stage <= 9 ) { // tant que mon stage n'est pas 10 ou que je suis encore en vie mes prefab peuvent ce crée
            
            // Il me faut une boucle !!!! 
            Stage = Stage + 1;
            Barrage.Add(1); Barrage.Add(2); Barrage.Add(3); Barrage.Add(4); Barrage.Add(5);

            int Passage = Random.Range(1, 6);
            int Passage2 = Random.Range(1, 6);
        
                if (Passage2 == Passage)
                {
                    if (Passage == 1)
                    {
                    Passage2 = Random.Range(2, 5);
                    }
                    else if (Passage == 2)
                    {
                    Passage2 = 1;
                    }
                    else if (Passage == 3)
                    {
                    Passage2 = 2;
                    }
                    else if (Passage == 4)
                    {
                    Passage2 = 3;
                    }
                    else if (Passage == 5)
                    {
                    Passage2 = Random.Range(1, 4);
                    }
                }

            
            Barrage.Remove(Passage); Barrage.Remove(Passage2);
        
            // j'instantie mes MUR sur 3 ligne
            Instantiate(Mur, new Vector3(BarListGO[Stage - 1][Barrage[0] - 1].transform.position.x, BarListGO[Stage - 1][Barrage[0] - 1].transform.position.y, BarListGO[Stage - 1][Barrage[0] - 1].transform.position.z), transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            Instantiate(Mur, new Vector3(BarListGO[Stage - 1][Barrage[1] - 1].transform.position.x, BarListGO[Stage - 1][Barrage[1] - 1].transform.position.y, BarListGO[Stage - 1][Barrage[1] - 1].transform.position.z), transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            Instantiate(Mur, new Vector3(BarListGO[Stage - 1][Barrage[2] - 1].transform.position.x, BarListGO[Stage - 1][Barrage[2] - 1].transform.position.y, BarListGO[Stage - 1][Barrage[2] - 1].transform.position.z), transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            
            // le code ci dessous est le choix des prebab a instancié
            int psg = Random.Range(0, 100);
            int psg2 = Random.Range(0, 100);
            Debug.Log(psg + "/" + psg2);
            if(psg >= 0 && psg < 19)
            { GOpasage = MesAnimaux[0]; psg = 1; }       //19 % de chance d'un animal
            else if (psg >= 19 && psg < 38)             // je dois absolument reprendre mes varriables de 1 a 6 sinon le reste du code ne fonctionnera pas
            { GOpasage = MesAnimaux[1]; psg = 2; }
            else if (psg >= 38 && psg < 57)
            { GOpasage = MesAnimaux[2]; psg = 3; }
            else if (psg >= 57 && psg < 76)
            { GOpasage = MesAnimaux[3]; psg = 4; }
            else if (psg >= 76 && psg < 95)     //19 % de chance d'un LOUP
            { GOpasage = Loup; psg = 5; }
            else if (psg >= 95 && psg <= 100)   //5 % de chance d'une vache
            { GOpasage = Vache; psg = 6; }

            if (psg2 >= 0 && psg2 < 19)
            { GOpasage1 = MesAnimaux[0]; psg2 = 1; }       //19 % de chance d'un animal
            else if (psg2 >= 19 && psg2 < 38)             // je dois absolument reprendre mes varriables de 1 a 6 sinon le reste du code ne fonctionnera pas
            { GOpasage1 = MesAnimaux[1]; psg2 = 2; }
            else if (psg2 >= 38 && psg2 < 57)
            { GOpasage1 = MesAnimaux[2]; psg2 = 3; }
            else if (psg2 >= 57 && psg2 < 76)
            { GOpasage1 = MesAnimaux[3]; psg2 = 4; }
            else if (psg2 >= 76 && psg2 < 95)     //19 % de chance d'un LOUP
            { GOpasage1 = Loup; psg2 = 5; }
            else if (psg2 >= 95 && psg2 <= 100)   //5 % de chance d'une vache
            { GOpasage1 = Vache; psg2 = 6; }
            Debug.Log(psg + "/" + psg2);
            //instantiate des mes animaux sur les passage libres
            Instantiate(GOpasage, new Vector3(BarListGO[Stage - 1][Passage - 1].transform.position.x, BarListGO[Stage - 1][Passage - 1].transform.position.y, BarListGO[Stage - 1][Passage - 1].transform.position.z), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            Instantiate(GOpasage1, new Vector3(BarListGO[Stage - 1][Passage2 - 1].transform.position.x, BarListGO[Stage - 1][Passage2 - 1].transform.position.y, BarListGO[Stage - 1][Passage2 - 1].transform.position.z), transform.rotation * Quaternion.Euler(0f, 180f, 0f));


            Barrage.Clear();


            // public GameObject[] MesAnimaux;
            // public GameObject Vache;
            // public GameObject Mur;
            // public GameObject Loup;

            Debug.Log(Stage + " fini");
            yield return new WaitForSeconds(time);
        }
    }
    

    public void Restart()
    {
        Live = 1;
        GameOver = false;
        //ColJoueur.enabled = true;
        //Deadline.SetActive(true);
        StartCoroutine(Debutdejeu());
        MenuGameOver.SetActive(false);
        Fermier.FarmerSpeed = 6;
        rr = 80;

    }

    public void QuitFishingGame()
    {
        SceneManager.LoadScene(6);
    }
}
