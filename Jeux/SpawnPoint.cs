using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

   private Transform thisSpawnPoint;
    private GameObject ThisSpawnPointColour;
    private ParticleSystem FXSpikes;
    private AudioSource spawnPointSound;

    void Start()
    {
        thisSpawnPoint = this.transform;
        ThisSpawnPointColour = this.gameObject;
        // FXSpikes = this.GetComponentInChildren<ParticleSystem>();
        this.gameObject.GetComponent<ParticleSystem>().Play();

        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            if (ps.gameObject != gameObject && ps.gameObject.name == "CFX3 Spikes")
            {
                
                FXSpikes = ps;
                spawnPointSound = ps.gameObject.GetComponent<AudioSource>();
                FXSpikes.Pause();
            }
            
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            PlayerPrefs.SetFloat("SpawnPointX",thisSpawnPoint.position.x);
            PlayerPrefs.SetFloat("SpawnPointY", thisSpawnPoint.position.y);
            PlayerPrefs.SetFloat("SpawnPointZ", thisSpawnPoint.position.z);
            FXSpikes.Play();
            spawnPointSound.Play();





        }
    }


}
