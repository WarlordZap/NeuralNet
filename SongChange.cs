using UnityEngine;
using System.Collections;

public class SongChange : MonoBehaviour {
	public AudioClip victorySong;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void changeSong() {
		audio.Stop();
		audio.clip = victorySong;
		audio.Play ();
	}
}
