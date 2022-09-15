using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Scriptable Objects/Weapon Stats", order = 52)]
public class WeaponStats : ScriptableObject
{
    [Header("Weapon Stats")]
    [SerializeField] private int _Damage;
    [SerializeField] private float _Range;
    [SerializeField] private float _FireRate;
    [SerializeField] private float _ImpactForce;
    [SerializeField] private int _MaxAmmo;
    [SerializeField] private float _ReloadTime;
    [SerializeField] private ParticleSystem _MuzzleFlash;

    public int Damage { get => _Damage;}
    public float Range { get => _Range;}
    public float FireRate { get => _FireRate;}
    public float ImpactForce { get => _ImpactForce;}
    public int MaxAmmo { get => _MaxAmmo;}
    public float ReloadTime { get => _ReloadTime;}
    public ParticleSystem MuzzleFlash { get => _MuzzleFlash;}
}
