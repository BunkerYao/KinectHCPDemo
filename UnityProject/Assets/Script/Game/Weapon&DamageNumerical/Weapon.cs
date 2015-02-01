using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述:武器抽象类
// ------------------------------------------------------
public abstract class Weapon : MonoBehaviour, IWeapon {
	public const float heatDecrement = 0.1f;
	protected float m_heat;
	protected float m_heatIncrement;
	protected float m_fireInterval;
	protected float m_reloadingTime;
	protected int m_capacity;
	protected int m_ammo;
	protected float m_timeSinceLastShot;
	protected float m_reloadingTimeCount;
	protected bool m_isReloading;
	protected bool m_isPlayerWeapon;
	private int m_newAmmoNumber;

	public float heat {
		get { return m_heat; }
	}
	public float heatIncrement {
		get { return m_heatIncrement; }
	}
	public float fireInterval {
		get { return m_fireInterval; }
	}
	public float reloadingTime {
		get { return m_reloadingTime; }
	}
	public int capacity {
		get { return m_capacity; }
	}
	public int ammo {
		get { return m_ammo; }
	}
	public bool isOverheat {
		get { return m_heat >= 1.0f; }
	}
	public bool isOut {
		get { return m_ammo == 0; }
	}
	public bool isReloading {
		get { return m_isReloading; }
	}
	public bool isPlayerWeapon {
		get { return m_isPlayerWeapon; }
	}

	/*
	 *  Editor tweakable parameters
	 */
	public float editHeatIncrement;
	public float editFireInterval;
	public float editReloadingTime;
	public int editCapacity;
	public bool editIsPlayerWeapon;
	
	public GameObject bulletPrefab;
	public Transform bulletInitialTrans;					// 子弹的初始变换
	
	void Awake()
	{
		initState ();
	}

	protected void initState()
	{
		m_heatIncrement = editHeatIncrement;
		m_fireInterval = editFireInterval;
		m_reloadingTime = editReloadingTime;
		m_capacity = editCapacity;
		m_isPlayerWeapon = editIsPlayerWeapon;
		m_heat = 1.0f;
		m_ammo = 0;
		m_timeSinceLastShot = m_fireInterval + 1.0f;
		m_reloadingTimeCount = 0.0f;
		m_isReloading = false;
	}

	virtual public bool shoot(){
		if (isOut || isReloading || isOverheat)
			return false;
		if (m_timeSinceLastShot < m_fireInterval)
			return false;
		// Clone a bullet 
		GameObject firedBullet = Instantiate (bulletPrefab, 
		                                      bulletInitialTrans.position,
		                                      bulletInitialTrans.rotation) as GameObject;
		firedBullet.GetComponent<Bullet> ().shot (isPlayerWeapon);
		// Update weapon state
		m_heat += m_heatIncrement;
		if (m_heat > 1.0f){ 
			m_heat = 1.0f;
			if (onOverheat != null)
				onOverheat();
		}
		m_ammo--;
		if (m_ammo <= 0){
			m_ammo = 0;
			if (onAmmoOut != null)
				onAmmoOut();
		}
		m_timeSinceLastShot = 0.0f;
		if (onShot != null)
			onShot ();
		return true;
	}

	virtual public bool reload(int ammoNumber){
		if (isReloading)
			return false;
		m_newAmmoNumber = ammoNumber + m_ammo;
		if (m_newAmmoNumber > m_capacity || m_newAmmoNumber < 0)
			return false;
		m_reloadingTimeCount = 0.0f;
		m_isReloading = true;
		return true;
	}

	void Update()
	{
		if (isReloading){
			m_reloadingTimeCount += Time.deltaTime;
			// Reloaded
			if (m_reloadingTimeCount >= m_reloadingTime){
				m_isReloading = false;
				m_ammo = m_newAmmoNumber;
				if (onReloadingDone != null)
					onReloadingDone();
			}
		}
		m_timeSinceLastShot += Time.deltaTime;
		// Cool down
		m_heat -= heatDecrement * Time.deltaTime;
		if (m_heat < 0.0f)
			m_heat = 0.0f;
	}

	// 射击事件
	public delegate void shootingAction();
	public static event shootingAction onShot;
	// 子弹耗尽事件
	public delegate void ammoOutAction();
	public static event ammoOutAction onAmmoOut;
	// 过热事件
	public delegate void overheatAction();
	public static event overheatAction onOverheat;
	// 装弹完毕事件
	public delegate void reloadingDoneAction();
	public static event reloadingDoneAction onReloadingDone;
}
