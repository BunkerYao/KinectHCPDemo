using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：子弹接口
// ------------------------------------------------------
public interface IBullet{
	float baseDamage { get; }					// 基础伤害值

	// 被击发
	void onShot(float initialSpeed, Transform targetTrans);
}
