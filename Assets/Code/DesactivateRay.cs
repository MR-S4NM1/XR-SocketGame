using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DesactivateRay : MonoBehaviour
{
    [SerializeField] protected InputActionProperty actionPropertyRight;
    [SerializeField] protected InputActionProperty actionPropertyLeft;

    [SerializeField] protected GameObject rightRay;
    [SerializeField] protected GameObject leftRay;

    #region UnityMethods
    private void Update()
    {
        rightRay.SetActive(actionPropertyRight.action.ReadValue<float>() > 0.1f);
        leftRay.SetActive(actionPropertyLeft.action.ReadValue<float>() > 0.1f);
    }

    #endregion
}
