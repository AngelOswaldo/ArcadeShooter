using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUtilities : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxOnMouseEnter;
    [SerializeField] private AudioClip sfxOnClick;

    [SerializeField] private UpgradesSelector upgradesSelector;
    [SerializeField] Transform newParent;

    [Header("Button Settings")]
    public int clicksToDestroy;
    private int clicksCount;

    private bool canPlay = true;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sfxOnMouseEnter != null)
        {
            if (canPlay)
        {
            audioSource.PlayOneShot(sfxOnMouseEnter);
            canPlay = false;
        }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Invoke(nameof(ActivatePlay), .5f);
    }

    public void SFXClick()
    {
        audioSource.PlayOneShot(sfxOnClick);
    }

    private void ActivatePlay()
    {
        canPlay = true;
    }

    public void DestroyButton()
    {
        clicksCount += 1;
        if(clicksCount >= clicksToDestroy)
        {
            upgradesSelector.upgrades.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void SetParent(GameObject child)
    {
        child.transform.SetParent(newParent);
        newParent.GetComponent<UpgradesSelector>().upgrades.Add(child);
    }

}
