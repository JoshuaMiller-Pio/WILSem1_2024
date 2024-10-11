using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    #region VARIABLES

    [Header("Interaction Settings")]
    public GameObject[] objects;
    public UnityEvent onTrigger;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private List<IInteractable> listeners;
    private int detected;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        if (objects.Length != 0)
        {
            listeners = new List<IInteractable>();

            foreach (GameObject objects in objects)
            {
                if (objects.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    listeners.Add(interactable);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected++;
            
            if (detected == 1)
            {
                Trigger();
                OnEnter();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (detected > 1)
            {
                return;
            }

            detected--;

            Trigger();
            OnExit();
        }
    }

    public void OnEnter()
    {
        onEnter?.Invoke();
    }

    public void OnExit()
    {
        onExit?.Invoke();
    }

    public void Trigger()
    {
        if (listeners != null)
        {
            foreach (IInteractable listener in listeners)
            {
                listener.OnInteract();
            }
        }

        onTrigger?.Invoke();
    }

    #endregion
}

public interface IInteractable
{
    public abstract void OnInteract();
}
