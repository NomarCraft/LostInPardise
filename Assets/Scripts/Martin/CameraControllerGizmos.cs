#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class CameraController : MonoBehaviour
{
	private void OnDrawGizmosSelected()
	{
		Handles.color = Color.yellow;
		Handles.DrawWireDisc(_playerCenter.position + new Vector3(0, heightOffset, 0), new Vector3(0, 1, 0), rotationRadius);

		Handles.color = Color.yellow;
		Handles.DrawLine(new Vector3(transform.position.x, minimumHeightOffset, transform.position.z), new Vector3(transform.position.x, maximumHeightOffset, transform.position.z));

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.5f);

		Gizmos.color = Color.green;
		Handles.DrawLine(transform.position, transform.position + Vector3.right);
	}
}
#endif
