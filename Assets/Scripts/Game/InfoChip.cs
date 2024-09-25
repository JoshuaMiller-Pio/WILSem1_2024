using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoChip : MonoBehaviour, IInteractable
{
    #region VARIABLES

    [Header("Info Chip References")]
    public Canvas hintBox;
    public Animator animator;

    [SerializeField] bool isDisplaying;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        isDisplaying = false;
    }

    private void Start()
    {
        Display();
    }

    #endregion

    #region METHODS

    public void Display()
    {
        if (isDisplaying)
        {
            animator.SetTrigger("start");
        }
        else
        {
            animator.SetTrigger("stop");
        }
    }

    #endregion

    #region INTERFACE METHODS

    public void OnInteract()
    {
        isDisplaying = !isDisplaying;
        Display();
    }

    #endregion

}
