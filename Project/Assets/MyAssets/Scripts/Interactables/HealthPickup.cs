using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public GameObject PickupEffect;
    [Header("Healer")]
    [SerializeField]
    int _healAmount;
    [SerializeField]
    bool _isFullHeal;
    public int soundToPlay;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // De esta manera, el corazón desaparecerá al tocarlo;
            // gameObject es la instancia del objeto a la que está
            // vinculado el Script
            Destroy(gameObject);
            Instantiate(PickupEffect, PlayerController.Instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.Instance.transform.rotation);
            if (_isFullHeal)
            {
                HealthManager.Instance.ResetHealth();
                AudioManager.instance.PlaySFX(soundToPlay);
            }
            else
            {
                HealthManager.Instance.AddHealth(_healAmount);
                AudioManager.instance.PlaySFX(soundToPlay);
            }
        }
    }
}
