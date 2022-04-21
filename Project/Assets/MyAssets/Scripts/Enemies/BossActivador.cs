using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivador : MonoBehaviour
{

    public static BossActivador instance;

    //Entrance es el puente, y theBoss el objeto del jefe final
    public GameObject bridge, theBoss;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Desactivamos el puente
            bridge.SetActive(false);
            //Activamos el jefe
            theBoss.SetActive(true);
            //Desactivamos este objeto con el trigger
            gameObject.SetActive(false);

        }
    }

    

}
