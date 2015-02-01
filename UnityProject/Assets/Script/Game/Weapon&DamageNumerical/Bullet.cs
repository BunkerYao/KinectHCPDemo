using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：子弹基类
// ------------------------------------------------------
[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour, IBullet {	
	protected float m_baseDamage;
	protected bool m_isPlayerBullet;

	public float baseDamage {
		get { return m_baseDamage; }
	}
	public bool isPlayerBullet {
		get { return m_isPlayerBullet; }
	}
		
	public abstract void shot(bool isPlayerBullet);
}
