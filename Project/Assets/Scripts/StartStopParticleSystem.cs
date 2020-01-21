using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopParticleSystem : MonoBehaviour {

    private ParticleSystem _ParticleSystem;

	// Use this for initialization
	void Start () {
        _ParticleSystem = GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	public void Stop () {
        _ParticleSystem.Stop();

    }
    public void Play()
    {
        _ParticleSystem.Play();

    }
}
