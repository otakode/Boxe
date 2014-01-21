using UnityEngine;
using System.Collections;

public class ResetPos : MonoBehaviour
{
	void Start ()
	{
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		this.transform.localScale = Vector3.one * 0.2f;
	}
}
