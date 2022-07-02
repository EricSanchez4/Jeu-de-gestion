using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheFish1 : MonoBehaviour
{
    public static NavMeshAgent fishAgent ;
    private GameObject Finish;
    
        
        // Start is called before the first frame update
    void Start()
    {
        Finish = GameObject.Find("FinishFish");

        if (this.tag != "Bonus")
        {
        fishAgent = this.GetComponent<NavMeshAgent>();
        fishAgent.destination = Finish.transform.position;

        }
    }

    private void Update()
    {
        if (this.tag == "Bonus")
        {
           this.transform.position = new Vector3(this.transform.position.x - 2 * Time.deltaTime, this.transform.position.y , this.transform.position.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "FinishFish")
        {
            if(this.gameObject.tag == "fish+1")
            { 
            Fishinggame.Live --;
            Destroy(gameObject,0.01f);
            }
           
            
            if (this.gameObject.tag == "Enemie")
            {
                
                Destroy(gameObject, 0.01f);
            }

            if (this.gameObject.tag == "Bonus")
            {

                Destroy(gameObject, 0.01f);
            }
        }
        
    }
    
}
