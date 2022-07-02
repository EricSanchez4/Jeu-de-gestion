using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDeplacement : MonoBehaviour
{
   
	
	
	
    void Start(){
		
		
		
        
    }

    
    void Update(){
    	
		transform.Translate(-Vector3.forward*Time.deltaTime);
		//transform.Translate(-Vector3.right*Time.deltaTime);
        
    }
}
