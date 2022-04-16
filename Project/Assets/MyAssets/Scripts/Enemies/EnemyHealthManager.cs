using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    //Vida máxima del enemigo
    public int maxHealth = 1;
    //Vida actual del enemigo
    private int currentHealth;
    //Sonido de muerte del enemigo
    public int deathSound;
    //Objetos para el efecto de muerte y drop de objetos(Hace falta modificar los assets para dropearlos en la posición correcta)
    public GameObject deathEffect, itemDrop;

    //Asigna la cantidad de vida máxima a la vida actual
    void Start()
    {
        currentHealth = maxHealth;
    }

    //Función para manejar la salud del enemigo
    public void takeDamage()
    {
        currentHealth--;
        //Reproducir sonido de muerte, efecto de muerte, destrucción del enemigo, dropeo de objetos y knockback al jugador
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
