using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Scriptable Objects/Enemy Stats", order = 52)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private float _Health;

    //public float Health { get { return _Health;  }
    public float Health { get => _Health;}
}
