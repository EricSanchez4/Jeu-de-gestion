using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemie : MonoBehaviour

{
    [Header("Stats Enemie")]
    public string NomEnemi;
    public float maxHealhOnGameEnemie = 100.0f;
    public float degatEnemy;
    public float attackDelay; // delais entre 2 
    public float LookAt;
    public float Speed = 3;
    public int experience;

    [Header("Fonction Jump")]
    public float JumpAt;
    public float DoNoJumpAt;
    public float SpeedInJump = 7;
    public float jumpDelay = 8.0f; // delais entre 2 

    [Header("Audio")]
    public AudioSource AudioJump;
    public AudioSource AudioDying;
    public AudioSource HitAudio;

    private TextMesh TextHeal;
    private Transform TextToRotate;
    private NavMeshAgent navMeshAgent;
    private Vector3 playerVector3;
    private GameObject player;
    private TPSController TPSController;
    private Collider colBodyEnemie;
    private float currentHealthOnGameEnemie;
    private float lastJump = 0f;
    private bool isJump;
    private bool isDead;
    private bool playerisDead = false;
    private GameObject FX1;
    private float timeAutoAttack; // ajustement entre 2 
    private float lastAttacked = 0f;
    private Animator anim;
    private float timePOSdown;
    private float timePOSdownToDestroy;
    private bool Islaunch = false;
    private bool isDying = false;

    

    private void Awake()
    {



        TextToRotate = this.transform.Find("New Text");
        currentHealthOnGameEnemie = maxHealhOnGameEnemie;
        TextHeal = GetComponentInChildren<TextMesh>();
        colBodyEnemie = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        //HitAudio = GetComponent<AudioSource>();
       // AudioJump = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        TPSController = player.GetComponent<TPSController>();


        if (NomEnemi == "Bacchus")
        {
            FX1 = GameObject.Find("FX Saut Bacchus");
            FX1.SetActive(false);
        }


        if (NomEnemi != "Training Dummie")
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerVector3 = player.transform.position;
            navMeshAgent.SetDestination(playerVector3);
            navMeshAgent.speed = 0;
        }
    }

    void Start()
    {
        
        
        
    }

    

    void Update()
    {

        //var pour que les PV des enemies soient afficher a la caméra en permanence.
        var m_Camera = GameObject.Find("Camera").transform;
        TextToRotate.transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
        

        if (NomEnemi != "Training Dummie" && isDead == false )
        {
            TextHeal.text = currentHealthOnGameEnemie.ToString();

            if (TPSController.justRez == true && isDead == false && Islaunch == false && TPSController.isDead == false)//resurection du joueur donc on arrete de danser
            {
                
                Islaunch = true;
                anim.SetBool("KillPlayer", false);
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
                anim.SetBool("Jump", false);

            }
            if (TPSController.isDead == true && playerisDead == false || TPSController.isDead == true && TPSController.justRez == true) //Si le joueur est tué
            {
                
                playerisDead = true;
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
                anim.SetBool("Jump", false);
                anim.SetBool("KillPlayer",true);

            }
            else
            {   playerVector3 = player.transform.position;
                navMeshAgent.SetDestination(playerVector3);

                if (currentHealthOnGameEnemie <= 0 && isDead == false) // Si cet enemie est tué
                {
                    isDead = true;
                    TextHeal.text = " ";
                    anim.SetTrigger("Dead");
                    anim.SetBool("Run", false);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Jump", false);
                   
                    navMeshAgent.speed = 0;
                    navMeshAgent.isStopped = true;
                    AudioDying.Play();
                    colBodyEnemie.isTrigger = true;
                    timePOSdown = Time.time + 5f; // 5f = temps avant que le corp descende
                    timePOSdownToDestroy = Time.time + 15f; // 15f = temps avant que le gameObject soit détruit
                    TPSController.XPGAIN(experience);  // Gain d'expérience


                }
                /////////////////////////////////////         Configuration du NavMesh de l'enemie                  ///////////////////////////////////////////////////
                if (Vector3.Distance(playerVector3, transform.position) < LookAt && isJump == false && isDead == false)
                {
                    navMeshAgent.SetDestination(playerVector3);
                    navMeshAgent.speed = Speed;
                    anim.SetBool("Run", true);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Jump", false);

                }
                //////////////////////////JUMP////////////////////////////////////////////////////JUMP////////////////////////////////////////////////////JUMP////////////////////////////////////////////////////JUMP//////////////////////////
                if (Vector3.Distance(playerVector3, transform.position) < JumpAt && Vector3.Distance(playerVector3, transform.position) > DoNoJumpAt && Time.time >= lastJump && isJump == false && isDead == false && NomEnemi == "Bacchus")   //////////////////////////JUMP//////////////////////////
                {                                                                                                                                                                                   //////////////////////////JUMP//////////////////////////
                    navMeshAgent.SetDestination(player.transform.position);                                                                                                                         //////////////////////////JUMP//////////////////////////
                    isJump = true;                                                                                                                                                                  //////////////////////////JUMP//////////////////////////
                    navMeshAgent.speed = SpeedInJump;                                                                                                                                               //////////////////////////JUMP//////////////////////////
                    anim.SetBool("Run", false);                                                                                                                                                     //////////////////////////JUMP//////////////////////////
                    anim.SetBool("Attack", false);                                                                                                                                                  //////////////////////////JUMP//////////////////////////
                    anim.SetBool("Jump", true);                                                                                                                                                     //////////////////////////JUMP//////////////////////////
                }                                                                                                                                                                                   //////////////////////////JUMP//////////////////////////
                //////////////////////////JUMP////////////////////////////////////////////////////JUMP////////////////////////////////////////////////////JUMP////////////////////////////////////////////////////JUMP//////////////////////////

                if (Vector3.Distance(playerVector3, transform.position) < DoNoJumpAt && Vector3.Distance(playerVector3, transform.position) < LookAt && isJump == false && isDead == false)
                {
                    navMeshAgent.speed = Speed;
                    anim.SetBool("Run", true);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Jump", false);
                }


                if (Vector3.Distance(playerVector3, transform.position) > LookAt && isJump == false && isDead == false)
                {
                    navMeshAgent.SetDestination(playerVector3);
                    navMeshAgent.speed = 0;
                    anim.SetBool("Run", false);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Jump", false);
                }

                //stopping distance
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && isJump == false && isDead == false)
                {
                    navMeshAgent.SetDestination(playerVector3);
                    navMeshAgent.speed = Speed;
                    anim.SetBool("Attack", true);
                    anim.SetBool("Run", false);
                    anim.SetBool("Jump", false);
                }

            }
            

        }
        //Manequin d'entrainement 
        if (NomEnemi == "Training Dummie" && currentHealthOnGameEnemie >=1)
        {
            TextHeal.text = currentHealthOnGameEnemie.ToString();
            

        }
        //Manequin d'entrainement remise a 100 de PV quand tombe a 0%
        else if (currentHealthOnGameEnemie <= 0f && NomEnemi == "Training Dummie")
            {
                TextHeal.text = currentHealthOnGameEnemie.ToString();
                currentHealthOnGameEnemie = maxHealhOnGameEnemie;
            }

        if (isDead == true && Time.time >= timePOSdown)
        {   Destroy(this.gameObject.GetComponent<NavMeshAgent>());
            this.gameObject.transform.Translate(0, -0.14f * Time.deltaTime, 0);}

        if (isDead == true && Time.time >= timePOSdownToDestroy)
        {
            Destroy(this.gameObject);
            
            
        }



    }

    //Enemy qui attaque le joueur
    public void Attack()
    {

       // if (Time.time >= lastAttacked)
       // {
            
            //timeAutoAttack = Time.time + 1.5f;
            //lastAttacked = Time.time + attackDelay;

            TPSController.currentHealthOnGame = TPSController.currentHealthOnGame - degatEnemy;
            //anim.SetBool("Attack", true);
            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);
        //}
    }

    public void voidAudioJump()
    {
        AudioJump.Play();

    }



    public void JumpAttack()
    {
        if (Time.time >= lastJump && isJump == true && isDead == false)
        {
           
            
            lastJump = Time.time + jumpDelay;
            TPSController.currentHealthOnGame = TPSController.currentHealthOnGame - degatEnemy;
            anim.SetBool("Attack", false);
            anim.SetBool("Run", false);
            anim.SetBool("Jump", true);

            if (NomEnemi == "Bacchus")
            {
                
                FX1.SetActive(true);
            }
        }
    }

    public void ENDJumpAttack()
    {
        

        if (NomEnemi == "Bacchus" && isJump)
        {
            anim.SetBool("Jump", false);
            FX1.SetActive(false);
            isJump = false;
        }
    }




    


    //Attaque du joueur
    public void Damage(int degats)
    {
        if (isDead == false)
        {
            currentHealthOnGameEnemie = currentHealthOnGameEnemie - degats;
            anim.SetBool("Hit", true);
            HitAudio.Play();
        }
    
    }
    
}
