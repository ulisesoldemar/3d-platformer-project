using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LsResetPlayer : MonoBehaviour
{
    public static LsResetPlayer instance;

    //Posicion para el respawn del jugador
    public Vector3 respawnPosition;


    public void Awake()
    {
        instance = this;
    }

    //Cuando entre a la Kill Zone del agua
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Evitar que el player se quede bug
            PlayerController.Instance.gameObject.SetActive(false);
            //Regresar al inicio del nivel
            PlayerController.Instance.transform.position = respawnPosition;
            //Volver a activar el jugador
            PlayerController.Instance.gameObject.SetActive(true);
        }
    }
}
