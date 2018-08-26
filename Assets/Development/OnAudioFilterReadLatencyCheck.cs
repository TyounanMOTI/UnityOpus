using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAudioFilterReadLatencyCheck : MonoBehaviour {
    float[] noise = new float[1024];

    void Start() {
        var source = GetComponent<AudioSource>();
        source.clip = AudioClip.Create("LatencyCheck", 1, 1, 48000, false);
        source.loop = true;
        source.Play();

        for (int i = 0; i < noise.Length; i++) {
            noise[i] = Random.Range(-0.5f, 0.5f);
        }
    }

    bool triggered = false;

    private void Update() {
        if (Input.GetKey(KeyCode.A)) {
            triggered = true;
        }
    }

    private void OnAudioFilterRead(float[] data, int channels) {
        if (triggered) {
            for (int i = 0; i < data.Length; i += channels) {
                var sample = noise[i];
                data[i] = sample;
                data[i + 1] = sample;
            }

            triggered = false;
        }
    }
}
