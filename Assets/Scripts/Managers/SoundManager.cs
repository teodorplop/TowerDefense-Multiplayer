using UnityEngine;
using System;
using System.Linq;

public partial class SoundManager : Singleton<SoundManager> {
	enum SoundType {Music, Piercing, Siege, Magic, Death};
	[System.Serializable]
	class Sound {
		public SoundType type;
		public AudioClip clip;
	}
	
	[SerializeField]
	private Sound[] _sounds;
	void Start() {
		PlayClip(GetClip(SoundType.Music), true);
	}
	
	AudioClip GetClip(SoundType type) {
		Sound[] eligibleSounds = _sounds.Where(obj => obj.type == type).ToArray();
		if (eligibleSounds.Length == 0) {
			return null;
		}
		return eligibleSounds[UnityEngine.Random.Range(0, eligibleSounds.Length)].clip;
	}
	void PlayClip(AudioClip clip, bool loop = false) {
		if (clip == null) {
			return;
		}
		
		var go = new GameObject();
		go.name = clip.name;
		var audioSource = go.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.loop = loop;
		audioSource.Play();
		
		if (!loop) {
			Destroy(go, clip.length);
		}
	}
}
