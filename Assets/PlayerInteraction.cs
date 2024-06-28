using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject InteractionMessage;
    public PlayerInteraction Instance;
    [HideInInspector] public IInteractable CurrentInteractable;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CurrentInteractable != null)
            {
                CurrentInteractable.Interact();
                Instance.Instance.enabled = false;
                //Enable the world canvas here.
            }
        }
    }
}
