using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Scriptable Objects/Weapon Stats", order = 52)]
public class WeaponStats : ScriptableObject
{
    [Header("Weapon Stats")]
    [SerializeField] private int _Damage;
    [SerializeField] private List<int> _DamageLevels;
    [SerializeField] private float _Range;
    [SerializeField] private float _FireRate;
    [SerializeField] private int _MaxAmmo;
    [SerializeField] private List<int> _AmmoLevels;
    [SerializeField] private float _ReloadTime;
    [SerializeField] private List<float> _ReloadLevels;
    [SerializeField] private ParticleSystem _MuzzleFlash;

    public int Damage
    {
        get { return _Damage; }
        set { _Damage = value; }
    }
    public int MaxAmmo
    {
        get { return _MaxAmmo; }
        set { _MaxAmmo = value; }
    }
    public float ReloadTime
    {
        get { return _ReloadTime; }
        set { _ReloadTime = value; }
    }
    public float Range { get => _Range;}
    public float FireRate { get => _FireRate;}
    public ParticleSystem MuzzleFlash { get => _MuzzleFlash;}
    public List<int> DamageLevels { get { return _DamageLevels; } }
    public List<int> AmmoLevels { get { return _AmmoLevels; } }
    public List<float> ReloadLevels { get { return _ReloadLevels; } }
}
