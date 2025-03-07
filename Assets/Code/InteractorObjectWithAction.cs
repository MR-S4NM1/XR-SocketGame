using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorObjectWithAction : MonoBehaviour
{
    [SerializeField] protected InputActionProperty actionProperty;
    [SerializeField] protected GameObject obj;

    #region UnityMethods
    private void Update()
    {
        obj.SetActive(actionProperty.action.ReadValue<float>() > 0.1f);
    }

    #endregion
}
