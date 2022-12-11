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
    [Header("Enemy Settigns")]
    private NavMeshAgent agent;
    private Collider mCollider;

    private Transform target;
    private PlayerHandler player;

    private EnemyHandler mHandler;
    private bool canAttack = true;
    private bool isDead = false;
    private bool isWalking = false;

    public EnemyType enemyType;

    private Animator anim;
    private DoIdle idleCom;
    private DoMove moveCom;
    private DoAttack attackCom;
    private DoDeath deathCom;

    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private float delayAttackSFX;

    [Header("Footstep Settings")]
    [SerializeField] private AudioSource stepsAudioSource;
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private FootStepsClips stepsClips;
    private float footstepTimer = 0;

    private void Start()
    {
        //Obtenemos los componentes necesarios
        agent = GetComponent<NavMeshAgent>();
        mCollider = GetComponent<Collider>();
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
        else if (player.isDead && !isDead)
        {
            IdleState();
            agent.enabled = false;
            enabled = false;
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

        //if (!canAttack)
        //    return;

        if (!isDead && canAttack)
        {
            if (distance <= mHandler.stats.ChaseDistance)
            {
                AttackState();
            }
            else if(distance > mHandler.stats.ChaseDistance)
            {
                ChaseState();
                FootStepsHandle();
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
        isWalking = false;

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
                StartCoroutine(RangeAttack());
                break;

            case EnemyType.Explosive:
                StartCoroutine(ExplosiveAttack());
                break;

            default:
                StartCoroutine(MeleeAttack());
                break;
        }
        
        //Debug.Log("Atacando");
    }

    /// <summary>
    /// Ejecutamos animacion de ataque y realizamos daño al jugador.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeleeAttack()
    {
        canAttack = false;
        Invoke(nameof(CallDamageSound), mHandler.stats.AttackAnimation - delayAttackSFX);
        Invoke(nameof(CallDamage), mHandler.stats.AttackAnimation);
        yield return new WaitForSeconds(mHandler.stats.SpeedAttack);
        canAttack = true;
    }

    private IEnumerator ExplosiveAttack()
    {
        canAttack = false;
        Invoke(nameof(CallDamageSound), mHandler.stats.AttackAnimation - delayAttackSFX);
        Invoke(nameof(CallDamage), mHandler.stats.AttackAnimation);
        yield return null;
    }

    private IEnumerator RangeAttack()
    {
        canAttack = false;
        Invoke(nameof(CallDamageSound), mHandler.stats.AttackAnimation - delayAttackSFX);
        Invoke(nameof(CallDamage), mHandler.stats.AttackAnimation);
        yield return new WaitForSeconds(mHandler.stats.SpeedAttack);
        canAttack = true;
    }

    private void CallDamage()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= mHandler.stats.GetMaxRangeAttack() && !isDead)
            player.TakeDamage(mHandler.stats.GetDamage(GameManager.instance.ActualWave()));

        if(enemyType == EnemyType.Explosive)
        {
            mHandler.TakeDamage(mHandler.stats.GetMaxHealth(GameManager.instance.ActualWave()));
        }
    }

    private void CallDamageSound()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= mHandler.stats.GetMaxRangeAttack() && !isDead)
        {
            if (mHandler.stats.AttackSFX.Length > 0)
            {
                sfxAudioSource.PlayOneShot(mHandler.stats.AttackSFX[Random.Range((int)0, (int)mHandler.stats.AttackSFX.Length)]);
            }
        }
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
        isWalking = true;
    }

    private void FootStepsHandle()
    {
        if (!isWalking)
            return;

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3f))
            {
                switch (hit.collider.tag)
                {
                    case "FootSteps/Metal":
                        stepsAudioSource.volume = stepsClips.MetalVolume;
                        stepsAudioSource.PlayOneShot(stepsClips.MetalClips[Random.Range(0, stepsClips.MetalClips.Length)]);
                        break;

                    case "FootSteps/Concrete":
                        stepsAudioSource.volume = stepsClips.ConcreteVolume;
                        stepsAudioSource.PlayOneShot(stepsClips.ConcreteClips[Random.Range(0, stepsClips.ConcreteClips.Length)]);
                        break;

                    case "FootSteps/Carpet":
                        stepsAudioSource.volume = stepsClips.CarpetVolume;
                        stepsAudioSource.PlayOneShot(stepsClips.CarpetClips[Random.Range(0, stepsClips.CarpetClips.Length)]);
                        break;

                    default:
                        stepsAudioSource.PlayOneShot(stepsClips.ConcreteClips[Random.Range(0, stepsClips.ConcreteClips.Length)]);
                        break;
                }
            }
            footstepTimer = baseStepSpeed;
        }
    }

    /// <summary>
    /// Ejecutamos animacion de muerte y destruimos al enemigo.
    /// </summary>
    public void DeathState()
    {
        if (anim != null && !isDead)
            deathCom.Execute(anim);

        if (mHandler.stats.DeathSFX.Length > 0)
            sfxAudioSource.PlayOneShot(mHandler.stats.DeathSFX[Random.Range((int)0, (int)mHandler.stats.DeathSFX.Length)]);

        isDead = true;
        agent.enabled = false;
        mCollider.enabled = false;
        Destroy(gameObject, mHandler.stats.DeathAnimation + 1.5f);
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
