using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HurtEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")//Reduce la salud del enemigo
        {
            other.GetComponent<EnemyHealthManager>().takeDamage();
        }
    }
}
