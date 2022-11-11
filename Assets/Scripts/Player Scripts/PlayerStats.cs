using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Scriptable Objects/Player Stats", order = 52)]
public class PlayerStats : ScriptableObject
{
    [Header("Player Health", order = 10)]
    [SerializeField] private float _MaxHealth;
    [SerializeField] private List<int> _HealthLevels;
    [Header("Player Movement", order = 10)]
    [SerializeField] private float _WalkSpeed;
    [SerializeField] private List<float> _WalkSpeedLevels;
    [SerializeField] private float _RunSpeed;
    [SerializeField] private List<float> _RunSpeedLevels;
    [SerializeField] private float _JumpForce;

    public float MaxHealth
    {
        get { return _MaxHealth; }
        set { _MaxHealth = value; }
    }
    public float WalkSpeed
    {
        get { return _WalkSpeed; }
        set { _WalkSpeed = value; }
    }
    public float RunSpeed 
    { 
        get { return _RunSpeed; }
        set { _RunSpeed = value; }
    }
    public float JumpForce { get => _JumpForce; }
    public List<int> HealthLevels { get { return _HealthLevels; } }
    public List<float> WalkSpeedLevels { get { return _WalkSpeedLevels; } }
    public List <float> RunSpeedLevels { get { return _RunSpeedLevels; } }
}
