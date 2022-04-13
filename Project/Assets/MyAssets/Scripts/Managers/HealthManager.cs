using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    [Header("Health")]
    [SerializeField]
    int _maxHealth;
    [SerializeField]
    int _currentHealth;
    [SerializeField]
    float _invincibleLength;
    float _invincibleCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // void Update()
    // {
    //     // WIP
    //     // Cuenta regresiva para dejar de ser invencible
    //     if (_invincibleCount > 0)
    //     {
    //         _invincibleCount -= Time.deltaTime;
    //         for (int i = 0; i < PlayerController.Instance.playerParts.Length; ++i)
    //         {
    //             if (Mathf.Floor(_invincibleCount * 5) % 2 == 0)
    //             {
    //                 PlayerController.Instance.playerParts[i].SetActive(true);
    //             }
    //             else
    //             {
    //                 PlayerController.Instance.playerParts[i].SetActive(false);
    //             }

    //             if (_invincibleCount <= 0)
    //             {
    //                 PlayerController.Instance.playerParts[i].SetActive(true);
    //             }
    //         }
    //     }
    // }

    void FixedUpdate()
    {
        if (_invincibleCount > 0)
        {
            // Cuenta regresiva para dejar de ser invencible
            _invincibleCount -= Time.deltaTime;
            // De esta manera, se genera un efecto de parpadeo en el personaje
            // Únicamente desactiva al modelo, no a la instancia del personaje
            for (int i = 0; i < PlayerController.Instance.playerParts.Length; ++i)
            {
                bool currentActive = PlayerController.Instance.playerParts[i].activeSelf;
                PlayerController.Instance.playerParts[i].SetActive(!currentActive);
                if (_invincibleCount <= 0)
                {
                    PlayerController.Instance.playerParts[i].SetActive(true);
                }
            }
        }
    }

    // WIP
    // IEnumerator Blink()
    // {
    //     while (_invincibleCount > 0)
    //     {
    //         for (int i = 0; i < PlayerController.Instance.playerParts.Length; ++i)
    //         {
    //             bool currentState = PlayerController.Instance.playerParts[i].activeSelf;
    //             PlayerController.Instance.playerParts[i].SetActive(!currentState);
    //         }
    //         _invincibleCount -= Time.deltaTime;
    //         yield return null;
    //     }
    //     for (int i = 0; i < PlayerController.Instance.playerParts.Length; ++i)
    //     {
    //         PlayerController.Instance.playerParts[i].SetActive(true);
    //     }

    // }

    public void Hurt()
    {
        // Si no se es invencible
        if (_invincibleCount <= 0)
        {
            // En caso de daño, se reduce la salud en 1
            --_currentHealth;
            // Si llega a 0, el personaje muere y respawnea
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                GameManager.Instance.Respawn();
            }
            else
            {
                // Si solamente se daño, se aplica un "Knockback" o retroceso
                PlayerController.Instance.Knockback();
                // Y comienza la cuenta regresiva de tiempo de invincibilidad
                _invincibleCount = _invincibleLength;
            }
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void AddHealth(int amount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += amount;
        }
    }
}
