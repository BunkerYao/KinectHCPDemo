using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述: 打印MissleLauncher的状态
// ------------------------------------------------------
[RequireComponent (typeof(MissileLauncher))]
public class MissileLauncherState : StateLabel {
	public Texture2D indicatorTex;
	public float indicatorSize;
	private MissileLauncher m_launcher;
	private Rect m_indicatorRect;

	void Awake()
	{
		m_launcher = GetComponent<MissileLauncher> ();
		m_indicatorRect = new Rect (0.0f, 0.0f, indicatorSize, indicatorSize);
		textColor = new Color (0.2f, 0.2f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		foldedText = "WeaponState:\n" +
					 "Ammo:" + m_launcher.ammo + "/" + m_launcher.capacity + "\n" +
					 "HasCandidate:" + m_launcher.hasCandidate + "\n" +
					 "HasLockedOn:" + m_launcher.hasLockedOn + "\n";
		additionalText = "IsReloading:" + m_launcher.isReloading + "\n" +
						 "IsOverheat:" + m_launcher.isOverheat + "\n" +
						 "IsPlayerWeapon:" + m_launcher.isPlayerWeapon + "\n";
	}

	void OnGUI()
	{
		printLabel (foldedText, additionalText);
		if (m_launcher.hasCandidate){
			Vector3 scrPos = cam.WorldToScreenPoint(m_launcher.canditateTrans.position);
			scrPos.y = Screen.height - scrPos.y;
			m_indicatorRect.x = scrPos.x - indicatorSize * 0.5f;
			m_indicatorRect.y = scrPos.y - indicatorSize * 0.5f;
			GUI.DrawTexture(m_indicatorRect, indicatorTex);
		}
	}

}
