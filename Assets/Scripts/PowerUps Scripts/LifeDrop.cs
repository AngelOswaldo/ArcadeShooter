using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrop : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float healingPercentage;
    [SerializeField] private AudioClip effectClip;

    private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHandler player = other.GetComponent<PlayerHandler>();
            player.HealDamage(player.stats.MaxHealth * healingPercentage);
            CallAudioClip(effectClip);
            Destroy(gameObject, effectClip.length + .5f);
            Disable();
        }
    }

    private void CallAudioClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void Disable()
    {
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
