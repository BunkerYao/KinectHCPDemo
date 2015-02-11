using UnityEngine;
using System.Collections;

// -------------------------------------------------
// 类名：HandTracker
// 描述：从SkeletonWrapper里获取手部位移和旋转，应用到绑定的GameObject上
// -------------------------------------------------
public class HandTracker : MonoBehaviour {
	public enum HAND { LeftHand, RightHand };
	public KinectSensor sensor;
	public SkeletonWrapper skeletonWrapper;
	public int player = 0;
	public HAND targetHand;
	public float scale = 1.0f;
	private int m_targetJointId;
	
	// Use this for initialization
	void Start () {
		m_targetJointId = targetHand == HAND.LeftHand ? 7 : 11 ;	
	}
	
	// Update is called once per frame
	void Update () {
		if (skeletonWrapper.pollSkeleton()){
			Vector3 pos = skeletonWrapper.bonePos[player, m_targetJointId] * scale;
			pos -= sensor.kinectCenter;
			pos.y -= sensor.sensorHeight;
			transform.localPosition = pos;
		}
	}
}
