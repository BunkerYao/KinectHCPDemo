using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：可毁坏物抽象类
// ------------------------------------------------------
[RequireComponent (typeof(Collider))]
public sealed class Damageable : MonoBehaviour, IDamageable {
	private float m_health;
	private float m_defenseFactor;
	private bool m_isHeavyArmored;
	private bool m_isEnemy;

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
	public bool isEnemy {
		get { return m_isEnemy; }
	}

	/*
	 *  Editor tweakable parameters
	 */
	public float editHealth;
	public float editDefenseFactor;
	public bool editIsHeavyArmored;
	public bool editIsEnemy;
	
	public void spawn(float initialHealth, bool isEnemy)
	{
		m_health = initialHealth;
		m_isEnemy = isEnemy;
	}

	public void decreaseHealth(float Value)
	{
		m_health -= Value;
		if (onDamage != null)
			onDamage ();
		if (m_health <= 0.0f){
			m_health = 0.0f;
			if (onDeath != null)
				onDeath();
		}
	}

	public bool die()
	{
		if (isAlive){
			m_health = 0.0f;
			if (onDeath != null)
				onDeath();
			return true;
		}
		return false;
	}

	void Awake()
	{
		m_defenseFactor = editDefenseFactor;
		m_isHeavyArmored = editIsHeavyArmored;
		spawn (editHealth, editIsEnemy);
	}

	// 被击中事件
	public delegate void damageAction();
	public static event damageAction onDamage;
	// 死亡事件
	public delegate void deathAction();
	public static event deathAction onDeath;
}
