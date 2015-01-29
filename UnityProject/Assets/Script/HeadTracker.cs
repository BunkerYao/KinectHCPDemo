using UnityEngine;
using System.Collections;

// -------------------------------------------------
// 类名：HeadTracker
// 描述：从SkeletonWrapper里获取头部位移和旋转，应用到绑定的GameObject上
//      启用HandTrackingMode后将目标关节设为玩家左手
// -------------------------------------------------
public class HeadTracker : MonoBehaviour {
	public KinectSensor sensor;
	public SkeletonWrapper skeletonWrapper;
	public Transform leftEyeTrans, rightEyeTrans;
	public int player = 0;
	public float IPD = 0.06f;
	public Vector3 eyeBias;
	public float scale = 1.0f;
	public bool jointRotation = false;
	public bool handTrackingMode = false;

	private Vector3 m_headPosition;
	public Vector3 headPosInKinectCoord
	{
		get { return m_headPosition; }
	}

	private Quaternion m_headOrien;
	public Quaternion headOrienInKinectCoord
	{
		get { return m_headOrien; }
	}

	private int m_targetJointId;

	void Start()
	{
		m_targetJointId = handTrackingMode ? 0x7 : 0x3;

	}
	
	// Update is called once per frame
	void Update () {
		// 更新瞳距
		Vector3 eyePos = Vector3.zero;
		eyePos.x = -IPD * 0.5f * scale;
		leftEyeTrans.localPosition = eyePos;
		eyePos.x = IPD * 0.5f * scale;
		rightEyeTrans.localPosition = eyePos;

		if (skeletonWrapper.pollSkeleton()) {
			// 获取头关节位移
			m_headPosition = skeletonWrapper.bonePos [player, m_targetJointId];
			m_headPosition += eyeBias;
			m_headPosition -= sensor.kinectCenter;
			m_headPosition.y -= sensor.sensorHeight;
			transform.localPosition = m_headPosition * scale;
			// 获取头关节旋转
			if (jointRotation){
			 	m_headOrien = skeletonWrapper.boneAbsoluteOrientation[player, m_targetJointId];
				Vector3 euler = m_headOrien.eulerAngles;
				// 获得的旋转四元数是右手坐标系下的，做变换到左手坐标系
				euler.y = 180.0f - euler.y;
				euler.z = -euler.z;
				transform.localRotation = Quaternion.Euler(euler);
			}
		}
	}		
}
