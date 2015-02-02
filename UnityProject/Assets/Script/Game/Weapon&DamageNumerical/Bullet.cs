using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：子弹基类
// ------------------------------------------------------
[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour, IBullet {	
	protected float m_baseDamage;
	protected float m_lifeTime;
	protected bool m_isPlayerBullet;

	public float baseDamage {
		get { return m_baseDamage; }
	}
	public float lifeTime {
		get { return m_lifeTime; }
	}
	public bool isPlayerBullet {
		get { return m_isPlayerBullet; }
	}
		
	public abstract void shot(bool isPlayerBullet);

	protected void reduceLifeTime(){
		m_lifeTime -= Time.deltaTime;
		if (m_lifeTime <= 0.0f){
			Destroy(gameObject);
		}
	}

	protected bool notFriendlyFire(IDamageable victim){
		return (victim.isEnemy && isPlayerBullet) || (!victim.isEnemy && !isPlayerBullet);
	}

	void Update()
	{
		reduceLifeTime ();
	}
}
