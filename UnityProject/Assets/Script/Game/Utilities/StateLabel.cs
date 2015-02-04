using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述: 这个类负责显示一个标签，打印物体的状态
// ------------------------------------------------------
public class StateLabel : MonoBehaviour {
	public Camera cam;
	public Color textColor;
	private Vector3 labelScreenPos;
	private Vector3 labelVpPos;

	private string m_foldedText;
	private string m_additionalText;

	public string foldedText {
		set { m_foldedText = value; }
		get { return m_foldedText; }
	}
	public string additionalText {
		set { m_additionalText = value; }
		get { return m_additionalText; }
	}

	protected virtual void printLabel (string foldedText, string additionalText)
	{
		GUI.contentColor = textColor;
		if (Vector3.Dot(transform.position - cam.transform.position, cam.transform.forward) < 0.0f) 
			return;
		labelScreenPos = cam.WorldToScreenPoint (transform.position);
		labelVpPos = cam.WorldToViewportPoint (transform.position);
		if (Mathf.Abs(labelVpPos.x - 0.5f) + Mathf.Abs(labelVpPos.y - 0.5f) < 0.05f){
			GUI.Label (new Rect (labelScreenPos.x, Screen.height - labelScreenPos.y, 200.0f, 100.0f),
			           foldedText + additionalText);
		}
		else{
			GUI.Label (new Rect (labelScreenPos.x, Screen.height - labelScreenPos.y, 200.0f, 100.0f),
			           foldedText);
		}
	}
		
	void OnGUI()
	{
		printLabel (foldedText, additionalText);
	}		
}
