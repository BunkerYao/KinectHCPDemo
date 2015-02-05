using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：使测试目标移动
// ------------------------------------------------------
public class TargetMove : MonoBehaviour {
	public float m_radius = 5.0f;
	private Vector3 m_center;
	private Vector3 m_newPos;
	private float m_angle;
		
	void Start()
	{
		m_newPos = transform.position;
		m_center = transform.position;
	}
	// Update is called once per frame
	void Update () {
		m_newPos.z = Mathf.Sin (m_angle) * m_radius + m_center.z;
		m_newPos.x = Mathf.Cos (m_angle) * m_radius + m_center.x;
		transform.position = m_newPos;
		m_angle += Time.deltaTime * 0.1f;
	}
}
