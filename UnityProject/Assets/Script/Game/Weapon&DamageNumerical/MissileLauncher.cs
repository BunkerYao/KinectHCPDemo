using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述:导弹发射器，在常规武器基础上增加了瞄准锁定功能
// ------------------------------------------------------
public class MissileLauncher : RegularGun {
	public float lockOnConeAngle;						// 锁定区域圆锥角大小，当候选目标离开此区域时即丢失目标
	public float lockOnTime;							// 需要将候选目标保持在锁定区域内的时间
	public float scanRadius;							// 扫描圈的半径
	public AudioClip audio_waitForLockOn;
	public AudioClip audio_lockedOn;
	public AudioClip audio_lostTarget;

	private bool m_hasCandidate;
	private bool m_hasLockedOn;
	private float m_InConeTime;
	private Transform m_candidateTrans;

	public Transform canditateTrans {
		get { return m_candidateTrans; }
	}
	public bool hasCandidate {
		get { return m_hasCandidate; }
	}
	public bool hasLockedOn {
		get { return m_hasLockedOn; }
	}

	private bool isInCone(Transform trans)
	{
		Vector3 vec = trans.position - transform.position;
		return Vector3.Angle (vec, transform.forward) <= lockOnConeAngle;
	}

	override public bool shoot()
	{
		if (m_hasLockedOn){						// 锁定才能发射
			if (base.shoot()){
				// 为导弹设定目标
				if (m_firedBullet != null){
					TrackingMissile missile = m_firedBullet.GetComponent<TrackingMissile>();
					if (missile != null){
						missile.targetTrans = m_candidateTrans;
					}
				}
				m_hasCandidate = false;
				m_hasLockedOn = false;
				return true;
			}
		}
		return false;
	}

	void Awake()
	{
		initState ();
		m_hasCandidate = false;
		m_hasLockedOn = false;
		m_InConeTime = 0.0f;

		onFoundCandidate += playWaitForLockOnAudio;
		onLockedOn += playLockedOnAudio;
		onLostTarget += playLostTargetAudio;

		if (editIsFullAmmo)
			m_ammo = m_capacity;
	}

	void Update()
	{
		updateState ();
		if (isOut)
			return;
		// 如果当前没有候选目标，做SphereCast检测目标
		if (!m_hasCandidate){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit)){
				if (hit.transform.tag == "Damageable"){
					// 是否在圆锥区域内
					if (isInCone(hit.transform)){
						m_candidateTrans = hit.transform;		// 保存候选目标的变换
						m_hasCandidate = true;
						m_InConeTime = 0.0f;					// 将锁定计时器清零
						if (onFoundCandidate != null)
							onFoundCandidate();
					}
				}
			}
		}
		else{
			// 若已经有候选目标，检查候选目标是否位于圆锥区域内
			if (!isInCone(m_candidateTrans)){
				// 当候选目标离开锁定圆锥，则丢失目标
				m_hasCandidate = false;
				m_hasLockedOn = false;
				m_candidateTrans = null;
				if (onLostTarget != null)
					onLostTarget();
			}
			else{
				// 若尚未锁定候选目标
				if (!m_hasLockedOn){
					m_InConeTime += Time.deltaTime;
					if (m_InConeTime > lockOnTime){
						// 锁定目标
						m_hasLockedOn = true;
						if (onLockedOn != null)
							onLockedOn();
					}
				}
			}
		}
	}

	// 获得候选目标事件
	public delegate void findCandidateAction();
	public static event findCandidateAction onFoundCandidate;
	// 锁定事件
	public delegate void lockOnAction();
	public static event lockOnAction onLockedOn;
	// 丢失目标事件
	public delegate void lostTargetAction();
	public static event lostTargetAction onLostTarget;	

	void playWaitForLockOnAudio()
	{
		audio.clip = audio_waitForLockOn;
		audio.loop = true;
		audio.Play ();
	}

	void playLockedOnAudio()
	{
		audio.clip = audio_lockedOn;
		audio.loop = false;
		audio.Play ();
	}

	void playLostTargetAudio()
	{
		audio.clip = audio_lostTarget;
		audio.loop = false;
		audio.Play ();
	}
}
