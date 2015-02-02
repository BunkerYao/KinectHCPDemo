using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述: 打印Damageable的状态
// ------------------------------------------------------
[RequireComponent (typeof(Damageable))]
public class DamageableState : StateLabel {
	private Damageable m_damageable;
	
	void Awake()
	{
		m_damageable = GetComponent<Damageable> ();
		textColor = new Color (1.0f, 0.0f, 1.0f);
	}
	
	void Update()
	{
		foldedText = "DamageableState:\n" +
					 "Health:" + m_damageable.health + "\n";
		additionalText = "Defense:" + m_damageable.defenseFactor + "\n" +
				   		 "IsHeavyArmored:" + m_damageable.isHeavyArmored + "\n" +
				   		 "IsEnemy:" + m_damageable.isEnemy + "\n" +
				   		 "IsAlive:" + m_damageable.isAlive;
	}		
}
