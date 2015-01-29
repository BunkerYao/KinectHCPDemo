using UnityEngine;
using System.Collections;

// -------------------------------------------------
// 类名：StereoProjection
// 描述：将摄像机世界坐标同步至对应眼睛的世界坐标，并对透视投影矩阵做修改，
//      执行视点相关的透视矫正算法
// -------------------------------------------------
public class StereoProjection : MonoBehaviour {
	public Transform eyeTrans;
	private StereoDisplay display;
	// 头部在屏幕坐标系的实际位置
	private Vector3 m_headPos;
	
	// Use this for initialization
	void Start () {
		display = transform.parent.gameObject.GetComponent<StereoDisplay> ();
		m_headPos = new Vector3 (0.0f, 0.0f, -0.2f);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		// 同步摄像机的位移至眼睛的位移
		transform.position = eyeTrans.position;
		// 更新视点位置
		m_headPos = transform.localPosition;
		// 修改投影矩阵
		float hw = display.displaySize.x * 0.5f;
		float hh = hw / display.aspectRatio;
		float InvAbsZ = 1.0f / Mathf.Abs (m_headPos.z);
		float l = (-hw - m_headPos.x) * InvAbsZ;
		float r = (hw - m_headPos.x) * InvAbsZ;
		float t = (hh - m_headPos.y) * InvAbsZ;
		float b = (-hh - m_headPos.y) * InvAbsZ;
		camera.projectionMatrix = PerspectiveOffCenter (l, r, b, t, 0.01f, camera.farClipPlane);
	}

	// 偏心投影矩阵
	static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far) {
		float x = 2.0F / (right - left);
		float y = 2.0F / (top - bottom);
		float a = (right + left) / (right - left);
		float b = (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F * far * near) / (far - near);
		float e = -1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = a;
		m[0, 3] = 0;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = b;
		m[1, 3] = 0;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = c;
		m[2, 3] = d;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = e;
		m[3, 3] = 0;
		return m;
	}
}
