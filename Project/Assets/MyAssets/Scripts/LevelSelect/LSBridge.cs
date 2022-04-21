using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSBridge : MonoBehaviour
{
    public string LevelToUnlock;



    void Start()
    {
        //Busca en las preferencias del jugador si el nivel aun esta bloqueado
        if(PlayerPrefs.GetInt(LevelToUnlock + "_unlocked") == 0)
        {
            //Bloquear el puente
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
