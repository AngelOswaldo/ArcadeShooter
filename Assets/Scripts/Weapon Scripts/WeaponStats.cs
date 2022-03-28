using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Scriptable Objects/Weapon Stats", order = 52)]
public class WeaponStats : ScriptableObject
{
    [Header("Weapon Stats")]
    [SerializeField] private float _Damage;
    [SerializeField] private float _Range;
    [SerializeField] private float _FireRate;
    [SerializeField] private float _ImpactForce;
    [SerializeField] private ParticleSystem _MuzzleFlash;

    public float Damage { get => _Damage;}
    public float Range { get => _Range;}
    public float FireRate { get => _FireRate;}
    public float ImpactForce { get => _ImpactForce;}
    public ParticleSystem MuzzleFlash { get => _MuzzleFlash;}
}