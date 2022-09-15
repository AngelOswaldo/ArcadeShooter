using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Scriptable Objects/Waves", order = 52)]
public class Wave : ScriptableObject
{
    [Header("Enemies to Apper")]
    public List<GameObject> Enemies;

    [Header("Wave Settings")]
    [SerializeField] private float _SpawnSpeed;

    public float SpawnSpeed { get => _SpawnSpeed;}
}
