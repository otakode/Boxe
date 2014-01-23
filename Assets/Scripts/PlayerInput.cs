using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private PXCUPipeline.Mode	ppMode = PXCUPipeline.Mode.FACE_LOCATION |
										 PXCUPipeline.Mode.GESTURE;
	private PXCUPipeline		pp = null;

	public GameObject	Left;
	public GameObject	Right;
	public GameObject	Head;

	void Start()
	{
		this.pp = new PXCUPipeline();
		if (this.pp == null || !this.pp.Init(this.ppMode))
		{
			Debug.Log("PXCUPipeline Initialization ERROR");
			return;
		}
	}

	void OnDestroy()
	{
		this.pp.Close();
	}

	void Update()
	{
		if (!this.pp.AcquireFrame(false))
		{
		//	Debug.Log("PXCUPipeline AcquireFrame ERROR");
			return;
		}
		this.UpdatePlayer();
		this.pp.ReleaseFrame();
	}

	void UpdatePlayer()
	{
		this.UpdateFists();
		this.UpdateHead();
	}

	void UpdateFists()
	{
		PXCMGesture.GeoNode leftHand;
		PXCMGesture.GeoNode rightHand;
		this.pp.QueryGeoNode(PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_LEFT, out leftHand);
		this.pp.QueryGeoNode(PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_RIGHT, out rightHand);
		if (leftHand.positionWorld.x < rightHand.positionWorld.x)
		{
			PXCMGesture.GeoNode temp = leftHand;
			leftHand = rightHand;
			rightHand = temp;
		}
		this.UpdateFist(leftHand, this.Left);
		this.UpdateFist(rightHand, this.Right);
	}

	void UpdateFist(PXCMGesture.GeoNode fist, GameObject glove)
	{
		Vector3 newPos = new Vector3(-fist.positionWorld.x * 2, fist.positionWorld.z * 2, -fist.positionWorld.y * 2);
		SmoothPosition(glove.transform.position, ref newPos);
		glove.transform.position = newPos;
	}

	void UpdateHead()
	{
		int faceID = 0;
		ulong timestamp;
		PXCMFaceAnalysis.Detection.Data face;
		if (this.pp.QueryFaceID(0, out faceID, out timestamp) &&
		    this.pp.QueryFaceLocationData(faceID, out face))
		{
			Vector3 newPos = new Vector3(1f - (face.rectangle.x) / 250f,
			                             0.8f - (face.rectangle.y) / 250f,
			                             this.Head.transform.position.z);
			this.SmoothPosition(this.Head.transform.position, ref newPos);
			newPos = new Vector3(Mathf.Clamp(newPos.x, -0.25f, 0.25f), Mathf.Clamp(newPos.y, -0.15f, 0.15f), newPos.z);
			this.Head.transform.position = newPos;
		}
		else
		{
			// place head between and behind the fists
		}
	}

	void SmoothPosition(Vector3 last, ref Vector3 next)
	{
		next.x = (next.x * 0.9f) + (last.x - next.x) * 0.1f;
		next.y = (next.y * 0.9f) + (last.y - next.y) * 0.1f;
	}
}
