using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Melee,
    Range,
    Explosive
}

public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform target;
    private PlayerHandler player;

    private EnemyHandler mHandler;
    private bool canAttack = true;
    private bool isDead = false;

    public EnemyType enemyType;

    private Animator anim;
    private DoIdle idleCom;
    private DoMove moveCom;
    private DoAttack attackCom;
    private DoDeath deathCom;


    private void Start()
    {
        //Obtenemos los componentes necesarios
        agent = GetComponent<NavMeshAgent>();
        mHandler = GetComponent<EnemyHandler>();
        anim = GetComponent<Animator>();
        //Ajustamos valores dependiendo de sus estadisticas
        agent.speed = mHandler.stats.MovementSpeed;
        agent.stoppingDistance = mHandler.stats.ChaseDistance;
        //Buscamos al jugador
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<PlayerHandler>();
        //Inicializamos animaciones
        idleCom = new DoIdle();
        moveCom = new DoMove();
        attackCom = new DoAttack();
        deathCom = new DoDeath();
    }

    private void Update()
    {
        //Mientras el jugador este vivo la maquina de estados del enemigo funcionara.
        if (!player.isDead)
        {
            IAStateMachine();
        }
        else
        {
            IdleState();
        }
    }

    /// <summary>
    /// Maquina de estados del enemigo, se busca al jugador y cuando este en rango lo atacamos.
    /// </summary>
    private void IAStateMachine()
    {
        if (anim != null)
            idleCom.Cancel(anim);

        float distance = Vector3.Distance(transform.position, target.position);

        if (!canAttack)
            return;

        if (!isDead && canAttack)
        {
            if (distance <= mHandler.stats.ChaseDistance)
            {
                AttackState();
            }
            else if(distance > mHandler.stats.ChaseDistance)
            {
                ChaseState();
            }
        }
    }

    /// <summary>
    /// Detenemos el movimiento del enemigo y lo rotamos en direccion del jugador para atacarlo.
    /// </summary>
    private void AttackState()
    {
        if (anim != null)
            moveCom.Cancel(anim);

        agent.isStopped = true;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), mHandler.stats.RotationSpeed * Time.deltaTime);

        if (anim != null)
            attackCom.Execute(anim);

        //Dependiendo del tipo de enemigo elegimos su ataque.
        switch (enemyType)
        {
            case EnemyType.Melee:
                StartCoroutine(MeleeAttack());
                break;

            case EnemyType.Range:
                break;

            case EnemyType.Explosive:
                break;

            default:
                StartCoroutine(MeleeAttack());
                break;
        }
        
        Debug.Log("Atacando");
    }

    /// <summary>
    /// Ejecutamos animacion de ataque y realizamos da�o al jugador.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeleeAttack()
    {
        canAttack = false;
        player.TakeDamage(mHandler.stats.DamageAmount);
        yield return new WaitForSeconds(mHandler.stats.SpeedAttack);
        canAttack = true;
    }

    /// <summary>
    /// Permitimos el movimiento del enemigo y le pasamos la ubicacion del jugador.
    /// </summary>
    private void ChaseState()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
        if (anim != null)
            moveCom.Execute(anim);
    }

    /// <summary>
    /// Ejecutamos animacion de muerte y destruimos al enemigo.
    /// </summary>
    public void DeathState()
    {
        isDead = true;
        agent.isStopped = true;
        if (anim != null)
            deathCom.Execute(anim);
        Destroy(gameObject, 3f);
    }

    /// <summary>
    /// Cancela el movimiento y ataques del enemigo, para colocarlo en idle.
    /// </summary>
    private void IdleState()
    {
        canAttack = false;
        agent.isStopped = true;
        if (anim != null)
            idleCom.Execute(anim);
    }
}
