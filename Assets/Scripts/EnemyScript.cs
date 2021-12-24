using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject Player;
    public float speedEnemy = 1;
    public int Health = 0;
    public bool Sword_Attack = false;
    public bool Get_Damage = false;
    public int Sword_Damage = 50;
    public GameObject WorldCenter;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        WorldCenter = GameObject.FindWithTag("WorldCenter");
    }

    void FixedUpdate()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        if (WorldCenter == null) {
            WorldCenter = GameObject.FindWithTag("WorldCenter");
        }
        
        GetSwordStatus();
        transform.Translate((Player.transform.position - transform.position).normalized * speedEnemy / 100);
        if (Get_Damage && Sword_Attack)
        {
            Health -= Sword_Damage;
            Get_Damage = false;
            Sword_Attack = false;
        } 
        if(Health <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        Destroy(gameObject);
    }
    void GetSwordStatus()
    {
        Sword_Attack = WorldCenter.GetComponent<FightButton>().Sword_Status;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            Get_Damage = true;
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        Get_Damage = false;
    }

}
