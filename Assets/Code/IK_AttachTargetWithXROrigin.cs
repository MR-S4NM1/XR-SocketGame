using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XRVRMap
{
    public Transform vRTarget;
    public Transform iKTarget;

    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public void Map()
    {
        iKTarget.position = vRTarget.TransformPoint(positionOffset);
        iKTarget.rotation = vRTarget.rotation * Quaternion.Euler(rotationOffset);
    }
}

public class IK_AttachTargetWithXROrigin : MonoBehaviour
{
    [SerializeField] protected float _turnSmoothness;

    [SerializeField] protected XRVRMap _head;
    [SerializeField] protected XRVRMap _leftHand;
    [SerializeField] protected XRVRMap _rightHand;

    [SerializeField] protected Vector3 _headPositionOffset;

    [SerializeField] protected float _bodyOffset;

    void Update()
    {
        gameObject.transform.position = _head.iKTarget.position;
        float temp_rotationY = _head.vRTarget.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation,
            Quaternion.Euler(gameObject.transform.eulerAngles.x, temp_rotationY, gameObject.transform.eulerAngles.z),
            _turnSmoothness);

        _head.Map();
        _leftHand.Map();
        _rightHand.Map();
    }
}
