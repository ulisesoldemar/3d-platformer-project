using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Solamente se llama a Hurt en caso de entrar en contacto
        // con el collider
        if (other.tag == "Player")
        {
            HealthManager.Instance.Hurt();
        }
    }
}
