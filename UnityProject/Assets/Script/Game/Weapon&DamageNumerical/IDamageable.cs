using UnityEngine;
using System.Collections;

public interface IDamageable{
	float health { get; } 					// 生命值
	bool isAlive { get; }					// 是否存活
	float defenseFactor { get; }			// 防护值因子
	int armorType { get; }					// 装甲类型

	// 创生
	void onSpawn();
	// 减少生命值
	void decreaseHealth(float Value);		
	// 立即杀死
	bool die();
}
