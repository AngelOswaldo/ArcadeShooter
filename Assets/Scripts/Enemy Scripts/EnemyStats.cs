using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Scriptable Objects/Enemy Stats", order = 52)]
public class EnemyStats : ScriptableObject
{
    /// <summary>
    /// Vida maxima del enemigo.
    /// </summary>
    [Header("Enemy Stats", order = 10)]
    [SerializeField] private int _StartHealth;
    [SerializeField] private int _HealthMultiplier;
    /// <summary>
    /// Daño que realiza al jugador.
    /// </summary>
    [Header("Attack Stats")]
    [SerializeField] private int _StartDamage;
    [SerializeField] private int _DamageMultiplier;
    [SerializeField] private float _DamageSplitter;
    /// <summary>
    /// Velocidad de ataque del enemigo.
    /// </summary>
    [SerializeField] private float _SpeedAttack; 
    /// <summary>
    /// Rango extra para alcanzar al jugador.
    /// </summary>
    [SerializeField] private float _RangeAttack;

    /// <summary>
    /// Velocidad de movimiento.
    /// </summary>
    [Header("Movement Stats")]
    [SerializeField] private float _MovementSpeed; 
    /// <summary>
    /// Rango de ataque (el enemigo se dentendra a esta distancia para atacar).
    /// </summary>
    [SerializeField] private float _ChaseDistance;
    /// <summary>
    /// Velocidad de rotacion.
    /// </summary>
    [SerializeField] private float _RotationSpeed;

    /// <summary>
    /// Tiempo que tarda para atacar.
    /// </summary>
    [Header("Timing Animations")]
    [SerializeField] private float _AttackAnimation;
    /// <summary>
    /// Tiempo que tarda el enemigo en morir.
    /// </summary>
    [SerializeField] private float _DeathAnimation;

    public int GetMaxHealth(int waveNumber)
    {
        if(waveNumber <= 0) { return _StartHealth; }
        return (_HealthMultiplier * waveNumber) + _StartHealth;
    }
    public int GetDamage(int waveNumber)
    {
        if (waveNumber <= 0) { return _StartDamage; }
        return ((int)(((waveNumber * _DamageMultiplier) / _DamageSplitter) + _StartDamage));
    }
    public float GetMaxRangeAttack()
    {
        return _RangeAttack + _ChaseDistance;
    }

    public float SpeedAttack { get => _SpeedAttack; }
    public float MovementSpeed { get => _MovementSpeed; }
    public float ChaseDistance { get => _ChaseDistance; }
    public float RotationSpeed { get => _RotationSpeed; }
    
    public float AttackAnimation { get => _AttackAnimation; }
    public float DeathAnimation { get => _DeathAnimation; }
}
