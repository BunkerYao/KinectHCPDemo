using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：一个测试用的脚本
// ------------------------------------------------------
public class TestInput : MonoBehaviour {
	public Weapon testWeapon;
	public Transform weaponTrans;
	public Damageable testTarget;
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Fire1") > 0.0f){
			testWeapon.shoot();
		}
		if (Input.GetAxis("Fire2") > 0.0f) {
			testWeapon.reload(30);
		}
	}	
}
