using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class Menu : MonoBehaviour
{
    private GameObject player;
    public static Transform spawnPointActuel;
    private TPSController tPSController;
    private ParticleSystem fxRessurection;

      void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tPSController = player.GetComponent<TPSController>();
        fxRessurection = GameObject.Find("Resurrection").GetComponent<ParticleSystem>();


    }
    void Update()
    {
        

    }


    public void RESPAWN()
    {
        float x = PlayerPrefs.GetFloat("SpawnPointX", 0.0f);
        float y = PlayerPrefs.GetFloat("SpawnPointY", 0.0f) + 0.5f;
        float z = PlayerPrefs.GetFloat("SpawnPointZ", 0.0f);
        Vector3 spawnPoint = new Vector3(x,y,z);

        tPSController.Respawn();
        player.transform.position = spawnPoint;
        fxRessurection.Play();





    }

    public void QUITTER()
    {

    }

   



}   
        

