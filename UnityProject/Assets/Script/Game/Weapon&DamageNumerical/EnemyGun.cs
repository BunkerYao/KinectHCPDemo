using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述:敌人的武器
// 		没有热量值
// 		弹药量无限
//		有开枪声
// ------------------------------------------------------
[RequireComponent (typeof(AudioSource))]
public class EnemyGun : Weapon
{
	public AudioClip fire;

	override public bool shoot()
	{
		if (base.shoot ()){
			audio.Play();
			return true;
		}
		return false;
	}
	
	void Start()
	{
		initState ();
		audio.clip = fire;
		m_heatIncrement = 0.02f;
		m_capacity = int.MaxValue;
		m_ammo = m_capacity;
	}
}

