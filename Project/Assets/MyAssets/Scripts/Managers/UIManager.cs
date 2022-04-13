using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("References")]
    [SerializeField]
    Image _blackScreen;

    [Header("Fade controls")]
    [SerializeField]
    float _fadeSpeed;
    public bool FadeToBlack { get; set; }
    public bool FadeFromBlack { get; set; }

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_blackScreen == null)
        {
            Debug.LogWarning("No hay asignada una imagen para desvancer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Se activa la transición hacia el negro
        if (FadeToBlack)
        {
            // Se actualiza únicamente el valor del alpha, por eso los demás
            // parámetros siguen usando los mismos valores de _blackScreen
            _blackScreen.color = new Color(
                _blackScreen.color.r,
                _blackScreen.color.g,
                _blackScreen.color.b,
                // Mathf.MoveTowards permite pasar lentamente de un valor a otro
                // Desde el alpha (0) hasta el 1 (full) con la velocidad _fadeSpeed
                Mathf.MoveTowards(_blackScreen.color.a, 1f, _fadeSpeed * Time.deltaTime)
            );
            // Cuando se llega a la opacidad completa del negro, se detiene
            // la animación
            if (_blackScreen.color.a == 1f)
            {
                FadeToBlack = false;
            }
        }

        // El mismo caso anterior, pero desde el negro hacia la vista transparente
        if (FadeFromBlack)
        {
            _blackScreen.color = new Color(
                _blackScreen.color.r,
                _blackScreen.color.g,
                _blackScreen.color.b,
                // Desde el alpha (0) hasta el 1 (empty) con la velocidad _fadeSpeed
                Mathf.MoveTowards(_blackScreen.color.a, 0f, _fadeSpeed * Time.deltaTime)
            );

            if (_blackScreen.color.a == 0f)
            {
                FadeFromBlack = false;
            }
        }
    }
}
