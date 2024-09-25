using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region VARIABLES

    [Header("Camera Transition References")]
    public Transform[] positions;
    public Camera targetCam;

    [Header("Transition References")]
    public float duration;
    public AnimationCurve curve;

    [SerializeField] private int targetIndex;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        if (targetCam == null)
        {
            targetCam = Camera.main;
        }

        //targetIndex = 0;
    }

    private void Start()
    {
        if (positions.Length > 0)
        {
            StopAllCoroutines();
            StartCoroutine(Move());
        }
    }

    #endregion

    #region METHODS

    public void GoDirect(int index)
    {
        if ((index < positions.Length) == false)
        {
            return;
        }

        targetIndex = index;

        StopAllCoroutines();
        StartCoroutine(Move());
    }

    public void GoNext()
    {
        if (targetIndex < positions.Length)
        {
            targetIndex++;
        }

        StopAllCoroutines();
        StartCoroutine(Move());
    }

    public void GoPrev()
    {
        if (targetIndex > 1)
        {
            targetIndex--;
        }

        StopAllCoroutines();
        StartCoroutine(Move());
    }

    #endregion

    #region COROUTINES

    private IEnumerator Move()
    {
        if (positions.Length == 0 || targetCam.transform.position == positions[targetIndex].position)
        {
            yield break;
        }

        float startTime = Time.time;
        float eTime = Time.deltaTime - startTime;
        float percentComplete = eTime / duration;

        Vector3 startPos = targetCam.transform.position;
        Vector3 endPos = positions[targetIndex].position;

        while (percentComplete < 1)
        {
            eTime = Time.time - startTime;
            percentComplete = eTime / duration;

            targetCam.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percentComplete));

            yield return null;
        }
    }

    #endregion
}
