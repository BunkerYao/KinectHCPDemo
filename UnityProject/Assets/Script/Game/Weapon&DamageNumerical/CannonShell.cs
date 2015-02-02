﻿using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：加农炮弹
// ------------------------------------------------------
public class CannonShell : Bullet {
	private float m_initialSpeed;
	private float m_explosionRadius;
	private float m_blowForce;

	/*
	 *  Editor tweakable parameters
	 */
	public float editBaseDamage;
	public float editLifeTime;
	public float editInitialSpeed;
	public bool editUseGravity;
	public float editExposionRadius;
	public float editHeavyArmorAddPercentage;
	public bool editBlowAway;
	public float editBlowForce;

	void Awake()
	{
		m_baseDamage = editBaseDamage;
		m_lifeTime = editLifeTime;
		m_initialSpeed = editInitialSpeed;
		m_explosionRadius = editExposionRadius;
		m_blowForce = editBlowForce;
		rigidbody.useGravity = editUseGravity;
		rigidbody.freezeRotation = true;
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
	}

	override public void shot(bool isPlayerBullet){
		m_isPlayerBullet = isPlayerBullet;
		rigidbody.velocity = m_initialSpeed * transform.forward;
	}

	private bool isValidTarget(string targetTag){
		return "Damageable" == targetTag;
	}

	private void applyDamage(IDamageable victim, float t){
		// Damage decreases linearly by distance
		float distFactor = Mathf.Lerp (baseDamage, 0.0f, t);
		float damageTaken = distFactor * (1.0f - victim.defenseFactor) * (1.0f + editHeavyArmorAddPercentage);
		victim.decreaseHealth (damageTaken);
	}

	private void applyBlowForce(Rigidbody rb, Vector3 Dir, float t){
		float forceAmount = Mathf.Lerp (m_blowForce, 0.0f, t);
		rb.AddForce (forceAmount * Dir);
	}

	void OnCollisionEnter(Collision collision)
	{
		// When hit something, boom!!!
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_explosionRadius);
		foreach (Collider c in colliders){
			if (isValidTarget(c.tag)){
				Vector3 vec = c.transform.position - transform.position;
				float dist = vec.magnitude;
				float t = (float)dist / m_explosionRadius;
				// If blow things away
				if (editBlowAway){
					Rigidbody rb = c.GetComponent<Rigidbody>();
					if (rb){
						applyBlowForce(rb, vec.normalized, t);
					}
				}
				IDamageable victim = c.GetComponent<Damageable>();
				if (notFriendlyFire(victim))
					applyDamage(victim, t);
			}
		}
		Destroy (gameObject);
	}		
}