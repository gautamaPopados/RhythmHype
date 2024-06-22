using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    public AudioSource pageSound;
    public GameObject page;
    public GameObject character;
    public GameObject characterGfx;
    public GameObject dialogue;


    private IEnumerator enableDialogue()
    {
        yield return new WaitForSeconds(2f);
        dialogue.SetActive(true);
    }

    private void Start()
    {
    }

    public void Interact()
    {
        pageSound.Play();
        page.SetActive(true);
        character.GetComponent<PlayerMovementINTRO>().enabled = false;
        characterGfx.GetComponent<Animator>().enabled = false;
        StartCoroutine(enableDialogue());
        Debug.LogFormat("Player interacted with '{0}'", gameObject.name);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction playerInteraction))
        {
            playerInteraction.CurrentInteractable = this;
            playerInteraction.InteractionMessage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction playerInteraction))
        {
            playerInteraction.CurrentInteractable = null;
            playerInteraction.InteractionMessage.gameObject.SetActive(false);
        }
    }
}