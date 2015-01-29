using UnityEngine;
using System.Collections;

// -------------------------------------------------
// 类名：StereoDisplay
// 描述：立体显示设备
// -------------------------------------------------
public class StereoDisplay : MonoBehaviour {
	public float physicalWidth, physicalHeight;
	public float scale = 1.0f;
	public Transform leftCamTrans, rightCamTrans;

	private Vector2 m_displaySize;
	public Vector2 displaySize
	{
		get { return m_displaySize; }
	}

	private float m_aspect;
	public float aspectRatio
	{
		get { return m_displaySize.x / m_displaySize.y; }
	}

	void Update()
	{
		m_displaySize.x = physicalWidth * scale;
		m_displaySize.y = physicalHeight * scale;
	}

	// 绘制Gizmo
	void OnDrawGizmos()
	{
		m_displaySize.x = physicalWidth * scale;
		m_displaySize.y = physicalHeight * scale;
		Vector3 leftTop = transform.position - displaySize.x * 0.5f * transform.right + displaySize.y * 0.5f * transform.up;
		Vector3 rightTop = transform.position + displaySize.x * 0.5f * transform.right + displaySize.y  * 0.5f * transform.up;
		Vector3 leftBottom = transform.position - displaySize.x * 0.5f * transform.right - displaySize.y  * 0.5f * transform.up;
		Vector3 rightBottom = transform.position + displaySize.x * 0.5f * transform.right - displaySize.y  * 0.5f * transform.up;
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (leftTop, rightTop);
		Gizmos.DrawLine (rightTop, rightBottom);
		Gizmos.DrawLine (rightBottom, leftBottom);
		Gizmos.DrawLine (leftBottom, leftTop);
		// 绘制视锥体
		Vector3 viewPoint = leftCamTrans.position;
		drawFrustumGizmo(viewPoint, leftTop, rightTop, leftBottom, rightBottom, new Color(1.0f, 0.0f, 0.0f));
		viewPoint = rightCamTrans.position;
		drawFrustumGizmo (viewPoint, leftTop, rightTop, leftBottom, rightBottom, new Color (0.0f, 1.0f, 1.0f));
	}	

	// 绘制视锥体
	void drawFrustumGizmo(Vector3 viewPoint, Vector3 lt, Vector3 rt, Vector3 lb, Vector3 rb, Color lineColor)
	{
		Gizmos.color = lineColor;
		Gizmos.DrawLine (viewPoint, lt);
		Gizmos.DrawLine (viewPoint, rt);
		Gizmos.DrawLine (viewPoint, lb);
		Gizmos.DrawLine (viewPoint, rb);
	}
}
