using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;

namespace UnityOpus.Example {
    [RequireComponent(typeof(MicrophoneDecoder), typeof(AudioSource))]
    public class LoopbackPlayer : MonoBehaviour {
        const NumChannels channels = NumChannels.Mono;
        const SamplingFrequency frequency = SamplingFrequency.Frequency_48000;
        AudioSource source;
        MicrophoneDecoder decoder;
        ConcurrentQueue<float> pcmBuffer = new ConcurrentQueue<float>();

        MicrophoneRecorder recorder;

        void OnEnable() {
            source = GetComponent<AudioSource>();
            source.clip = AudioClip.Create("Loopback", 1, (int)channels, (int)frequency, false);
            source.loop = true;
            source.Play();
            decoder = GetComponent<MicrophoneDecoder>();
            decoder.OnDecoded += OnDecoded;

            //recorder = GetComponent<MicrophoneRecorder>();
            //recorder.OnAudioReady += OnAudioReady;

            Debug.Log(AudioSettings.outputSampleRate);
        }

        void OnDisable() {
            decoder.OnDecoded -= OnDecoded;
            //recorder.OnAudioReady -= OnAudioReady;
            source.Stop();
        }

        void OnDecoded(float[] pcm, int pcmLength) {
            for (int i = 0; i < pcmLength; i++) {
                pcmBuffer.Enqueue(pcm[i]);
            }
        }

        void OnAudioReady(float[] pcm) {
            for (int i = 0; i < pcm.Length; i++) {
                pcmBuffer.Enqueue(pcm[i]);
            }
        }

        void OnAudioFilterRead(float[] data, int channels) {
            if (pcmBuffer.Count < data.Length / channels) {
                return;
            }
            for (int i = 0; i < data.Length; i += channels) {
                float sample;
                pcmBuffer.TryDequeue(out sample);
                data[i] = sample * 0.7080f; // -3dB
                data[i + 1] = sample * 0.7080f; // -3dB
            }
        }
    }
}
