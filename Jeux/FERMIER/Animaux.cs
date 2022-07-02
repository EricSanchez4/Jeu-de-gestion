using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animaux : MonoBehaviour
{
    private NavMeshAgent NavMACetAnimal;
    private Collider ColCetAniamal;

   


    // Start is called before the first frame update
    void Start()
    {

        NavMACetAnimal = this.gameObject.GetComponent<NavMeshAgent>();
        ColCetAniamal = this.GetComponent<BoxCollider>();
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            ColCetAniamal.enabled = false;


        }

    }
    // Update is called once per frame
    void Update()
    {
        
       




    }
}
