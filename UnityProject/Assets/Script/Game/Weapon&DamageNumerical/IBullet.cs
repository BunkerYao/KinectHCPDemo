using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：子弹接口
// ------------------------------------------------------
public interface IBullet{
	float baseDamage { get; }					// 基础伤害值
	bool isPlayerBullet { get; }				// 是否是玩家的子弹
	// 被击发
	void shot(bool isPlayerBullet);
}
