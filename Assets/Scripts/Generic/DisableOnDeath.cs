using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DisableOnDeath : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private CharacterController character;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject playerUI;

    public void DisableAll()
    {
        fpsController.enabled = false;
        character.enabled = false;
        weaponHolder.SetActive(false);
        playerUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
