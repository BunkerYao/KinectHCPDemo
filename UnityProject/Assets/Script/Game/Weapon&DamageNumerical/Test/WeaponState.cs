using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述: 打印Weapon的状态
// ------------------------------------------------------
[RequireComponent (typeof(Weapon))]
public class WeaponState : StateLabel {
	private Weapon m_weapon;

	void Awake()
	{
		m_weapon = GetComponent<Weapon> ();
		textColor = new Color (0.2f, 0.2f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		foldedText = "WeaponState:\n" +
					 "Heat:" + m_weapon.heat + "\n" +
					 "Ammo:" + m_weapon.ammo + "/" + m_weapon.capacity + "\n";
		additionalText = "IsReloading:" + m_weapon.isReloading + "\n" +
						 "IsOverheat:" + m_weapon.isOverheat + "\n" +
						 "IsPlayerWeapon:" + m_weapon.isPlayerWeapon + "\n";
	}
}
