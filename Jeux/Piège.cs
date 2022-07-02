using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piège : MonoBehaviour
{

    private BoxCollider attackZone;
    public int degats = 10;
    public float tempsPourAttack = 0.0f;
    public float fireRate = 0.5F;
    public TPSController TPSController;

   
    private void OnTriggerStay(Collider other)
    { 
        if (other.tag == "Player" && Time.time > tempsPourAttack)
        {
            tempsPourAttack = Time.time + fireRate;
            TPSController.currentHealthOnGame -= 10f;
        }

    }
   
}
