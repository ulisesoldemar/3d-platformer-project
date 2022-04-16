using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    //Vida m�xima del enemigo
    public int maxHealth = 1;
    //Vida actual del enemigo
    private int currentHealth;
    //Sonido de muerte del enemigo
    public int deathSound;
    //Objetos para el efecto de muerte y drop de objetos(Hace falta modificar los assets para dropearlos en la posici�n correcta)
    public GameObject deathEffect, itemDrop;

    //Asigna la cantidad de vida m�xima a la vida actual
    void Start()
    {
        currentHealth = maxHealth;
    }

    //Funci�n para manejar la salud del enemigo
    public void takeDamage()
    {
        currentHealth--;
        //Reproducir sonido de muerte, efecto de muerte, destrucci�n del enemigo, dropeo de objetos y knockback al jugador
        if(currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(deathSound);
            Destroy(gameObject);

            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(itemDrop, transform.position, transform.rotation);
        }
        PlayerController.Instance.Bounce();
    }
}
