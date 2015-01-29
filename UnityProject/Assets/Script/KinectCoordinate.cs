using UnityEngine;
using System.Collections;

// -------------------------------------------------
// 类名：KinectCoordinate
// 描述：Kinect坐标系
// -------------------------------------------------
public class KinectCoordinate : MonoBehaviour {
	public float scale = 1.0f;
	public HeadTracker headTracker;
	public StereoDisplay[] displays;
	public bool scaleChildrenPos = false;

	void Start()
	{
		if (scaleChildrenPos) {
			for (int i = 0; i < transform.childCount; ++i) {
				transform.GetChild(i).localPosition *= scale;
			}
		}
	}

	void Update()
	{
		UpdateScaling ();
	}

	void OnDrawGizmos()
	{
		UpdateScaling ();
	}
		
	void UpdateScaling()
	{	
		headTracker.scale = scale;
		for (int i = 0; i < displays.Length; ++i) {
			displays[i].scale = scale;
		}
	}
}
