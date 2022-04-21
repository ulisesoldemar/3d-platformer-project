using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Instancia de la clase
    public static BossController instance;

    //Variable para cambiar las fases de anomacion
    public Animator animator;

    //Variable para la isla de victoria
    public GameObject victoryZone;

    //Variable para esperar antes de mostrar la estrella final
    public float waitToShowExit;

    //Musica para el jefe
    public int bossMusic, bossDeath, bossDeathShout, bossHit;

    //Enum para controlar las variables de animaciones
    public enum BossPhase
    {
        intro,
        phase1,
        phase2,
        end
    };

    //Phase actual del jefe
    public BossPhase currentPhase = BossPhase.intro;


    private void Awake()
    {
        instance = this;    
    }


    void Start()
    {
        
    }


    void Update()
    {
        //Resetear al enemigo cuando morimos
        if (GameManager.Instance.isRespawning)
        {
            //Restaurar la animacion
            currentPhase = BossPhase.intro;
             
            //Desactivar las fases de ataque
            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", false);

            //Volver a tocar la musica del nivel
            AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

            gameObject.SetActive(false);

            //Activar trigger, el puente y desactivar el jefe final
            BossActivador.instance.gameObject.SetActive(true);
            BossActivador.instance.theBoss.SetActive(false);
            BossActivador.instance.bridge.SetActive(true);

            GameManager.Instance.isRespawning = false;
        }
    }

    //Cuando se activa el objeto
    public void OnEnable()
    {
        //Se apaga la musica que sonaba anteriormente
        AudioManager.instance.PauseMusic(AudioManager.instance.levelMusicToPlay);

        //Se enciende la musica del jefe final
        GameManager.Instance.musicPlaying = bossMusic;
        AudioManager.instance.PlayMusic(bossMusic);
    }

    //funcion para manejar el daño del Jefe
    public void damageBoss()
    {
        //Sonido de efecto para golpear al jefe
        AudioManager.instance.PlaySFX(bossHit);

        //Cambiar de Fase del Jefe
        currentPhase++;

        //Si la animacion no es la final
        if(currentPhase != BossPhase.end)
        {
            //Prender la animacion de daño del jefe
            animator.SetTrigger("Hurt");
        }

        switch (currentPhase)
        {
            //Activar la fase1
            case BossPhase.phase1:
                animator.SetBool("Phase1", true);
                break;

            //Activar la fase2
            case BossPhase.phase2:
                animator.SetBool("Phase2", true);
                //Desactivar la Fase 1
                animator.SetBool("Phase1", false);
                break;

            //Activar la animacion end
            case BossPhase.end:
                animator.SetTrigger("End");
                //Iniciamos la corrutina para aplicar todos los sonidos y movimientos finales del jefe
                StartCoroutine(EndBoss());
                break;
                
        }

    }

    IEnumerator EndBoss()
    {
        //Encender los audios
        AudioManager.instance.PlaySFX(bossDeath);
        AudioManager.instance.PlaySFX(bossDeathShout);
        GameManager.Instance.musicPlaying = AudioManager.instance.levelMusicToPlay;

        //Esperar el tiempo 
        yield return new WaitForSeconds(waitToShowExit);

        //Pausamos la cancion del jefe final
        AudioManager.instance.PauseMusic(bossMusic);
        //Encendemos la musica del nivel
        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

        //Activar la zona de victoria
        victoryZone.SetActive(true);
        //Desactivar al jefe final
        gameObject.SetActive(false);

    }
}
