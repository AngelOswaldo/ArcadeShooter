using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Scriptable Objects/Enemy Stats", order = 52)]
public class EnemyStats : ScriptableObject
{
    [Header("Enemy Stats", order = 10)]
    [SerializeField] private int _MaxHealth;

    [Header("Attack Stats")]
    [SerializeField] private int _DamageAmount;
    [SerializeField] private float _AttackAnimation;
    [SerializeField] private float _SpeedAttack;
    [SerializeField] private float _RangeAttack;

    [Header("Movement Stats")]
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private float _ChaseDistance;
    [SerializeField] private float _RotationSpeed;

    public int MaxHealth { get => _MaxHealth; }

    public int DamageAmount { get => _DamageAmount; }
    public float AttackAnimation { get => _AttackAnimation; }
    public float SpeedAttack { get => _SpeedAttack; }
    public float RangeAttack { get => _RangeAttack; }

    public float MovementSpeed { get => _MovementSpeed; }
    public float ChaseDistance { get => _ChaseDistance; }
    public float RotationSpeed { get => _RotationSpeed; }
}
