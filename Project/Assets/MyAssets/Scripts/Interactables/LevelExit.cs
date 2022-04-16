using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        //Detecta el contacto con el jugador y activa un trigger
        if(other.tag == "Player")
        {
            animator.SetTrigger("Hit");
            StartCoroutine(GameManager.Instance.LevelEndWaiter());
        }
    }
}
