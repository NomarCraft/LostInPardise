#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class CharacterController : MonoBehaviour
{
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawLine(transform.TransformPoint(cc.center + new Vector3(0, -cc.height / 2, 0)), transform.TransformPoint(cc.center + new Vector3(0, -cc.height / 2 - 0.25f, 0)));
	}
}
#endif
