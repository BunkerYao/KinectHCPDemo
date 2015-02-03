using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：跟踪导弹
// ------------------------------------------------------
[RequireComponent (typeof(SphereCollider))]
public class TrackingMissile : Bullet {
	private float m_force;
	private float m_speed;
	private float m_explosionRadius;
	private float m_detonateRadius;
	private float m_maxAngularSpeed;
	private float m_activateDelay;
	private float m_timeSinceShot;
	private bool m_isActivated;
	private float m_fuel;
	private float m_fuelDecrement;
	private bool m_isEngineWorking;
	private Transform m_targetTrans;
	private SphereCollider sphereCollider;

	public Transform targetTrans {
		set { m_targetTrans = value; }
	}

	/*
	 *  Editor tweakable parameters
	 */
	public float editBaseDamage;
	public float editLifeTime;
	public float editForce;
	public float editExplosionRadius;
	public float editDetonateRadius;
	public float editMaxAngularSpeed;
	public float editActivateDelay;
	public float editFuelDecrement;
	public float editHeavyArmorAddPercentage;
	public Transform editTargetTrans;

	void Awake()
	{
		m_baseDamage = editBaseDamage;
		m_lifeTime = editLifeTime;
		m_force = editForce;
		m_explosionRadius = editExplosionRadius;
		m_detonateRadius = editDetonateRadius;
		m_maxAngularSpeed = editMaxAngularSpeed;
		m_activateDelay = editActivateDelay;
		m_fuelDecrement = editFuelDecrement;
		m_timeSinceShot = 0.0f;
		sphereCollider = GetComponent<SphereCollider> ();
		sphereCollider.radius = 0.001f;							// 避免检测到与枪支的触发
		sphereCollider.isTrigger = true;
	}

	override public void shot(bool isPlayerBullet){
		m_isPlayerBullet = isPlayerBullet;
		m_isEngineWorking = true;
		m_isActivated = false;
		m_fuel = 1.0f;
		rigidbody.useGravity = false;
		rigidbody.velocity = transform.forward * m_force;
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		targetTrans = editTargetTrans;
		enabled = true;
	}

	void Update()
	{
		reduceLifeTime ();
		// 减少油量
		if (m_isEngineWorking){
			m_fuel -= m_fuelDecrement * Time.deltaTime;
			if (m_fuel <= 0.0f){
				// 油量耗尽
				rigidbody.useGravity = true;
				sphereCollider.radius = 0.001f;
				m_isActivated = false;
				m_isEngineWorking = false;
			}
		}
		if (!m_isActivated){
			m_timeSinceShot += Time.deltaTime;
			if (m_timeSinceShot >= m_activateDelay){
				// 激活导弹
				m_isActivated = true;
				sphereCollider.isTrigger = false;
				sphereCollider.radius = m_detonateRadius;
				// 记录下速度大小
				m_speed = rigidbody.velocity.magnitude;
			}
		}
	}

	void FixedUpdate()
	{
		if (!m_isEngineWorking)
			return;
		if (m_isActivated){
			// 修正飞行方向
			Vector3 targetVec = (m_targetTrans.position - transform.position).normalized;
			float targetAngle = Vector3.Angle(transform.forward, targetVec);
			float steeringAngle = Mathf.Clamp(targetAngle, 0.0f, m_maxAngularSpeed * Time.fixedDeltaTime);
			float t = steeringAngle / targetAngle;
			if (float.IsNaN(t)) return;
			Quaternion targetRot = Quaternion.FromToRotation(Vector3.forward, targetVec);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, t);
			rigidbody.velocity = m_speed * transform.forward;
		}
		else{
			rigidbody.AddForce(m_force * transform.forward);
		}
	}

	bool isValidTarget(string targetTag)
	{
		return targetTag == "Damageable";
	}

	void applyDamage(IDamageable victim, float t)
	{
		float damageTaken = Mathf.Lerp (m_baseDamage, 0.0f, t);
		damageTaken *= (1.0f - victim.defenseFactor);
		if (victim.isHeavyArmored){
			damageTaken *= 1.0f + editHeavyArmorAddPercentage;
		}
		victim.decreaseHealth (damageTaken);
	}
		
	void OnCollisionEnter(Collision collision)
	{
		if (m_isActivated){
			if (isValidTarget(collision.collider.tag)){
				// 判断是不是目标
				if (collision.collider.transform == m_targetTrans){
					Collider[] colliders = Physics.OverlapSphere (transform.position, m_explosionRadius);
					foreach (Collider c in colliders){
						if (isValidTarget(c.tag)){
							float dist = (c.transform.position - transform.position).magnitude;
							float t = dist / m_explosionRadius;
							IDamageable victim = c.GetComponent<Damageable>();
							applyDamage(victim, t);
						}
					}
					Destroy (gameObject);
				}
			}
		}
	}

}
