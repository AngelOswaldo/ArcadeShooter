using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Scriptable Objects/Player Stats", order = 52)]
public class PlayerStats : ScriptableObject
{
    [Header("Player Health", order = 10)]
    [SerializeField] private int _MaxHealth;
    [SerializeField] private List<int> _HealthLevels;
    [Header("Player Movement", order = 10)]
    [SerializeField] private float _WalkSpeed;
    [SerializeField] private List<float> _WalkSpeedLevels;
    [SerializeField] private float _RunSpeed;
    [SerializeField] private List<float> _RunSpeedLevels;
    [SerializeField] private float _JumpForce;

    public int MaxHealth { get => _MaxHealth; }
    public float WalkSpeed { get => _WalkSpeed; }
    public float RunSpeed { get => _RunSpeed; }
    public float JumpForce { get => _JumpForce; }
}
