using System.Collections.Concurrent;
using UnityEngine;

namespace UnityOpus.Example {
    [RequireComponent(typeof(MicrophoneDecoder), typeof(AudioSource))]
    public class LoopbackPlayer : MonoBehaviour {
        public int bufferCount {
            get { return pcmBuffer.Count; }
        }

        const int bufferLengthGoal = 4000;
        const NumChannels channels = NumChannels.Mono;
        const SamplingFrequency frequency = SamplingFrequency.Frequency_48000;
        AudioSource source;
        MicrophoneDecoder decoder;
        ConcurrentQueue<float> pcmBuffer = new ConcurrentQueue<float>();
        int prepareBufferLength;
        bool preparing = true;

        MicrophoneRecorder recorder;

        void OnEnable() {
            source = GetComponent<AudioSource>();
            source.clip = AudioClip.Create("Loopback", 1, (int)channels, (int)frequency, false);
            source.loop = true;
            source.Play();
            decoder = GetComponent<MicrophoneDecoder>();
            decoder.OnDecoded += OnDecoded;

            int dspBufferLength;
            int bufferCount;
            AudioSettings.GetDSPBufferSize(out dspBufferLength, out bufferCount);
            prepareBufferLength = dspBufferLength * 2;
        }

        void OnDisable() {
            decoder.OnDecoded -= OnDecoded;
            source.Stop();
            while (!pcmBuffer.IsEmpty) {
                float dummy;
                pcmBuffer.TryDequeue(out dummy);
            }
        }

        void OnDecoded(float[] pcm, int pcmLength) {
            if (preparing) {
                if (pcmBuffer.Count + pcmLength > prepareBufferLength) {
                    for (int i = 0; i < pcmBuffer.Count + pcmLength - prepareBufferLength; i++) {
                        float dummy;
                        pcmBuffer.TryDequeue(out dummy);
                    }
                }
            }
            for (int i = 0; i < pcmLength; i++) {
                pcmBuffer.Enqueue(pcm[i]);
            }
            if (pcmBuffer.Count > bufferLengthGoal) {
                for (int i = 0; i < pcmBuffer.Count - bufferLengthGoal / 2; i++) {
                    float dummy;
                    pcmBuffer.TryDequeue(out dummy);
                }
            }
        }

        void OnAudioFilterRead(float[] data, int channels) {
            if (preparing && pcmBuffer.Count > data.Length / channels) {
                preparing = false;
            }
            if (preparing) {
                for (int i = 0; i < data.Length; i++) {
                    data[i] = 0;
                }
                return;
            }
            if (pcmBuffer.Count < data.Length / channels) {
                //Debug.LogWarning("Buffer underrun");
                return;
            }
            for (int i = 0; i < data.Length; i += channels) {
                float sample;
                pcmBuffer.TryDequeue(out sample);
                // mono to stereo upmix
                data[i] = sample * 0.7080f; // -3dB
                data[i + 1] = sample * 0.7080f; // -3dB
            }
        }
    }
}
