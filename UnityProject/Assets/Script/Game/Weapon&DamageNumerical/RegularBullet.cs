using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：常规类子弹
// ------------------------------------------------------
[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public class RegularBullet : MonoBehaviour, IBullet {
	private float m_baseDamage;
	private float m_speed;
	private bool m_infiniteSpeed;

	public float baseDamage {
		get { return m_baseDamage; }
	}

	/*
	 * Editor tweakable parameters
	 */
	public float editBaseDamage;
	public float editSpeed;
	public bool editInfiniteSpeed;
	public float editHeavyArmorDamagePercentage;

	void Start()
	{
		// Initialize attributes from editor 
		m_baseDamage = editBaseDamage;
		m_speed = editSpeed;
		m_infiniteSpeed = editInfiniteSpeed;
		// Turn on ccd because bullet moves fast
		if (!m_infiniteSpeed)
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		rigidbody.useGravity = false;
		rigidbody.freezeRotation = true;
	}

	public void onShot(float initialSpeed, Transform targetTrans)
	{
		// If speed is infinite, do a raycast
		if (m_infiniteSpeed) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit)){
				if (isValidTarget(hit.transform.tag)){
					IDamageable victim = hit.transform.GetComponent<BDamageable>();
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
			IDamageable victim = collision.transform.GetComponent<BDamageable>();
			applyDamage(victim);
		}
		Destroy (gameObject);
	}		
}
