using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InteractableG : MonoBehaviour, IInteractable
{
    public LevelLoader levelLoader;


    private void Start()
    {
    }

    public new void Interact()
    {
        levelLoader.LoadDucksLevel();
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