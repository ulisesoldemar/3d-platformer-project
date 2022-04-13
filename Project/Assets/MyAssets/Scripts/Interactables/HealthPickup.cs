using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Healer")]
    [SerializeField]
    int _healAmount;
    [SerializeField]
    bool _isFullHeal;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // De esta manera, el corazón desaparecerá al tocarlo;
            // gameObject es la instancia del objeto a la que está
            // vinculado el Script
            Destroy(gameObject);
            if (_isFullHeal)
            {
                HealthManager.Instance.ResetHealth();
            }
            else
            {
                HealthManager.Instance.AddHealth(_healAmount);
            }
        }
    }
}
