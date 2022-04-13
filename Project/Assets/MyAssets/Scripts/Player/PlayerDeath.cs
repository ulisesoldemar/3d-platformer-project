using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Solamente se Respawnea en caso de morir por
        // entrar a un collider que haga instakill
        if (other.tag == "Player")
        {
            GameManager.Instance.Respawn();
        }
    }
}
