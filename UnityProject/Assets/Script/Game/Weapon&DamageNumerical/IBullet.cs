using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：子弹接口
// ------------------------------------------------------
public interface IBullet{
	float speed { get; }						// 飞行速度
	bool infiniteSpeed { get; }					// 是否速度无限大
	float steeringDamping { get; }				// 转向迟滞
	float fuelDecrement { get; }				// 燃料消耗率
	float fuel { get; } 						// 燃料
	float detonateRadius { get; }				// 引爆半径
	float baseDamage { get; }					// 基础伤害值
	float explosionRadius { get; }				// 爆炸半径

	// 被击发
	void onShot(float initialSpeed);
	// 伤害到目标
	void onDamage(IDamageable victim);
}
