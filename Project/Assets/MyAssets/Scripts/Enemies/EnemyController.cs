using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //Puntos de patrulla totales
    public Transform[] patrolPoints;
    //Punto de patrulla actual
    public int currentPatrolPoint;
    //Agente del enemigo para controlar su posición
    public NavMeshAgent agent;
    //Animador del enemigo
    public Animator animator;
    //Tiempo de espera al llevar a un punto de patrulla
    public float waitAtPoint = 2f;
    //Contador del tiempo de espera
    private float waitCounter;
    //Rango en el que el enemigo perseguirá al jugador
    public float chaseRange;
    //Rango al cual se activa el ataque del enemigo
    public float attackRange = 1f;
    //Tiempo entre ataques
    public float timeBetweenAttacks = 2f;
    //Contador del tiempo de espera entre ataques
    private float attackCounter;

    //Enum que almacena los estados del enemigo
    public enum AIState
    {
        Idle,
        Patrolling,
        Chasing,
        Attacking
    };
    //Estado actual del enemigo
    public AIState currentState;

    private void Start()
    {
        //Espera el tiempo especificado antes de moverse
        waitCounter = waitAtPoint;
    }


    // Update is called once per frame
    void Update()
    {
        //Almacena la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

        //Acciones del enemigo en función de su estado
        switch(currentState)
        {
            case AIState.Idle:
                animator.SetBool("IsMoving", false);
                    
                if(waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.Patrolling;
                    //Indica el siguiente punto de patrulla
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }  
                if(distanceToPlayer <= chaseRange)//Verifica si el jugador se encuetra a rango para perseguirlo
                {
                    currentState = AIState.Chasing;
                    //Activa el trigger del animator para indicar que el enemigo se mueve y utilice la animación correcta
                    animator.SetBool("IsMoving", true);
                }
                break;
            case AIState.Patrolling:

                if (agent.remainingDistance <= .2f)
                {
                    //Actualiza el punto de patrulla actual
                    currentPatrolPoint++;
                    //Reinicia el contador de puntos de patrulla
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;
                }
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                }

                animator.SetBool("IsMoving", true);
                break;

            case AIState.Chasing:

                agent.SetDestination(PlayerController.Instance.transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                    animator.SetTrigger("Attack");
                    animator.SetBool("IsMoving", false);

                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;

                }
                if(distanceToPlayer >= chaseRange)//Verifica si el jugador sale del rango de persecución, de ser así, detiene el movimiento del enemigo, espera y continúa con la patrulla
                {
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;

                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
                break;
            case AIState.Attacking:
                //Mira directamente al jugador
                transform.LookAt(PlayerController.Instance.transform, Vector3.up);
                //Suaviza rotación del enemigo
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                //Inicia el contador para atacar
                attackCounter -= Time.deltaTime;
                if(attackCounter <= 0)
                {
                    if (distanceToPlayer < attackRange)//Ataca si el jugador se encuentra a rango
                    {
                        animator.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        currentState = AIState.Idle;
                        waitCounter = waitAtPoint;

                        agent.isStopped = false;
                    }
                    
                }
                
                break;
        }
        
    }
}
