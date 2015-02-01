using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：可伤害物
// ------------------------------------------------------
public interface IDamageable{
	float health { get; } 					// 生命值
	bool isAlive { get; }					// 是否存活
	float defenseFactor { get; }			// 防护值因子
	bool isHeavyArmored { get; }			// 是否是重装甲类型
	bool isEnemy { get; }					// 是否是敌人

	// 创生
	void spawn(float initialHealth, bool isEnemy);
	// 减少生命值
	void decreaseHealth(float Value);		
	// 立即杀死
	bool die();
}
