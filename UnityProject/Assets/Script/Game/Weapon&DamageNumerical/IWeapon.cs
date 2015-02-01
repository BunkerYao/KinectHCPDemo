using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：武器接口
// ------------------------------------------------------
public interface IWeapon {
	float heat { get; }								// 当前热量值（0～1）
	float heatIncrement { get; }					// 热量增加率
	float fireInterval { get; }						// 射击间隔
	float reloadingTime { get; }					// 装弹所需时间
	int capacity { get; }							// 弹夹容量
	int ammo { get; }								// 弹夹子弹
	bool isOverheat { get; }						// 是否已经过热
	bool isOut { get; } 							// 是否已打完子弹
	bool isReloading { get; }						// 是否正在装弹
	bool isPlayerWeapon { get; }					// 是否是玩家的武器

	// 开火
	bool shoot();
	// 装弹
	bool reload(int ammoNumber);
}
