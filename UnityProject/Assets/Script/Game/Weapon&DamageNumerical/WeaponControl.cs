using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：玩家的武器操纵脚本
// ------------------------------------------------------
public class WeaponControl : MonoBehaviour {
	public Transform leftHandTrans, rightHandTrans;
	public Transform weaponPivot;
	public float triggerFiringDist;
	public float triggerFiringAngle;
	public Weapon weapon;

	void Update()
	{
		// 将武器的射击线保持在左手与武器转轴的连线上
		Vector3 lhand2Pivot = (weaponPivot.position - leftHandTrans.position).normalized;
		Quaternion rot = Quaternion.FromToRotation (transform.forward, lhand2Pivot);
		weaponPivot.localRotation = rot;
		// 计算右手到左手的向量
		Vector3 hand2hand = leftHandTrans.position - rightHandTrans.position;
		// 当距离小于开火触发距离
		if (hand2hand.magnitude <= triggerFiringDist && Vector3.Angle(hand2hand.normalized, Vector3.up) <= triggerFiringAngle)
			weapon.shoot ();
	}

}
