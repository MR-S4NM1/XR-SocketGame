using System;
using UnityEngine;

namespace Dante.VR {

	#region Class

	[Serializable]
	public class VRMap
	{
		public Transform vrTarget;
		public Transform ikTarget;

		public Vector3 positionOffset;
		public Vector3 rotationOffset;

		public void Map()
		{
			ikTarget.position = vrTarget.TransformPoint(positionOffset);
			ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(rotationOffset);
		}
	}

    #endregion

    public class IKAttachTargetWithXROrigin : MonoBehaviour
	{
		#region References

		[SerializeField] protected VRMap head;
		[SerializeField] protected VRMap leftHand;
		[SerializeField] protected VRMap rightHand;

		#endregion

		#region Knobs

		[SerializeField] protected float turnSmoothness = 0.1f;

		[SerializeField] protected Vector3 headPositionOffset;

		[SerializeField] protected float bodyOffset;

        #endregion

        #region UnityMethods

        private void Update()
        {
			transform.position = head.ikTarget.position + headPositionOffset;
			float temp_rotationY = head.vrTarget.eulerAngles.y;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, temp_rotationY, transform.eulerAngles.z), turnSmoothness);

			head.Map();
			leftHand.Map();
			rightHand.Map();
        }

        #endregion

        #region PublicMethods



        #endregion

        #region LocalMethods



        #endregion

        #region GettersSetters



        #endregion
    }
}
