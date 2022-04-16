using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifeTime;
    //Destruye objeto después del tiempo especificado
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
