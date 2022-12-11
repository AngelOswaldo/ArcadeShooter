using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Foot Steps Clips", menuName = "Scriptable Objects/FootSteps Clips", order = 52)]
public class FootStepsClips : ScriptableObject
{
    [SerializeField] private AudioClip[] _ConcreteClips;
    [SerializeField] private AudioClip[] _MetalClips;
    [SerializeField] private AudioClip[] _CarpetClips;
    [SerializeField] private float _ConcreteVolume;
    [SerializeField] private float _MetalVolume;
    [SerializeField] private float _CarpetVolume;

    public AudioClip[] ConcreteClips { get { return _ConcreteClips; } }
    public AudioClip[] MetalClips { get { return _MetalClips; } }
    public AudioClip[] CarpetClips { get { return _CarpetClips; } }
    public float ConcreteVolume { get { return _ConcreteVolume; } }
    public float MetalVolume { get { return _MetalVolume; } }
    public float CarpetVolume { get { return _CarpetVolume; } }
}
