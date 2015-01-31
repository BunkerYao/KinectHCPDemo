using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：可毁坏物抽象类
// ------------------------------------------------------
[RequireComponent (typeof(Collider))]
public abstract class BDamageable : MonoBehaviour, IDamageable {
	private float m_health;
	private float m_defenseFactor;
	private bool m_isHeavyArmored;

	public float health {
		get { return m_health; }
	}
	public bool isAlive {
		get { return m_health > 0.0f; }
	}
	public float defenseFactor {
		get { return m_defenseFactor; }
	}
	public bool isHeavyArmored {
		get { return m_isHeavyArmored; }
	}

	/*
	 *  Editor tweakable parameters
	 */
	public float editHealth;
	public float editDefenseFactor;
	public bool editIsHeavyArmored;
	
	public abstract void onSpawn();
	public abstract void decreaseHealth(float Value);		
	public abstract bool die();
}
