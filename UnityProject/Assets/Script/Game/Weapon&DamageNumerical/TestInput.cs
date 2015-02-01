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
			testWeapon.reload(5);
		}

		float rotationX = -Input.GetAxis ("Mouse Y");
		float rotationY = Input.GetAxis ("Mouse X");
		weaponTrans.Rotate (new Vector3 (0.0f, rotationY, 0.0f));
		weaponTrans.Rotate (new Vector3 (rotationX, 0.0f, 0.0f));
	}

	void OnGUI()
	{
		GUI.Label (new Rect (0.0f, 0.0f, 200.0f, 400.0f), 
		          "GunState:\n" +
						"Ammo:" + testWeapon.ammo + "\n" +
						"Heat:" + testWeapon.heat + "\n" +
						"IsReloading:" + testWeapon.isReloading + "\n" +
		           "TargetState:\n" +
		           "Health:" + testTarget.health + "\n" +
		           "IsAlive:" + testTarget.isAlive + "\n");
	}
}
