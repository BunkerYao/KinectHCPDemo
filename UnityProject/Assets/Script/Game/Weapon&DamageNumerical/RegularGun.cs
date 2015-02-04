using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述:常规武器,有音效
// ------------------------------------------------------
[RequireComponent (typeof(AudioSource))]
public class RegularGun : Weapon
{
	public bool editIsFullAmmo;
	public AudioClip audio_fire;
	public AudioClip audio_reloading;
	public AudioClip audio_overheat;
	
	override public bool reload(int ammoNumber)
	{
		if (base.reload(ammoNumber)){
			if (audio_reloading != null){
				audio.clip = audio_reloading;
				audio.Play();
			}
			return true;
		}
		return false;
	}

	protected void playOverheatAudio()
	{
		if (audio_overheat != null){
			audio.clip = audio_overheat;
			audio.Play ();
		}
	}

	protected void playShootAudio()
	{
		if (audio_fire != null){
			audio.clip = audio_fire;
			audio.Play ();
		}
	}
	
	void Awake()
	{
		initState ();
		if (editIsFullAmmo)
			m_ammo = m_capacity;
		onOverheat += playOverheatAudio;
		onShot += playShootAudio;
	}	
}

