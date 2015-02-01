using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：常规类子弹
// ------------------------------------------------------
public class RegularBullet : Bullet {
	private float m_speed;
	private float m_lifeTime;
	private bool m_infiniteSpeed;

	/*
	 * Editor tweakable parameters
	 */
	public float editBaseDamage;
	public float editSpeed;
	public float editLifeTime;
	public bool editInfiniteSpeed;
	public float editHeavyArmorDamagePercentage;

	void Awake()
	{
		// Initialize attributes from editor 
		m_baseDamage = editBaseDamage;
		m_speed = editSpeed;
		m_lifeTime = editLifeTime;
		m_infiniteSpeed = editInfiniteSpeed;
		// Turn on ccd because bullet moves fast
		if (!m_infiniteSpeed)
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		rigidbody.useGravity = false;
		rigidbody.freezeRotation = true;
	}

	override public void shot(bool isPlayerBullet)
	{
		m_isPlayerBullet = isPlayerBullet;
		// If speed is infinite, do a raycast
		if (m_infiniteSpeed) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit)){
				if (isValidTarget(hit.transform.tag)){
					IDamageable victim = hit.transform.GetComponent<Damageable>();
					if ((victim.isEnemy && isPlayerBullet) || (!victim.isEnemy && !isPlayerBullet))
						applyDamage(victim);
					return;
				}
			}
			// Destroy the bullet if hit nothing
			Destroy(gameObject);
		}
		else{
			rigidbody.velocity = m_speed * transform.forward;
		}
	}	

	private void applyDamage(IDamageable victim)
	{
		float damageTaken = m_baseDamage * (1.0f - victim.defenseFactor);
		if (victim.isHeavyArmored) 
			damageTaken *= editHeavyArmorDamagePercentage;
		victim.decreaseHealth (damageTaken);
	}

	private bool isValidTarget(string targetTag){
		return "Damageable" == targetTag;
	}

	void OnCollisionEnter(Collision collision)
	{
		// When hit something
		if (isValidTarget(collision.transform.tag)){
			IDamageable victim = collision.transform.GetComponent<Damageable>();
			if ((victim.isEnemy && isPlayerBullet) || (!victim.isEnemy && !isPlayerBullet))
				applyDamage(victim);
		}
		Destroy (gameObject);
	}	

	void Update()
	{
		m_lifeTime -= Time.deltaTime;
		if (m_lifeTime <= 0.0f){
			Destroy(gameObject);
		}
	}
}
