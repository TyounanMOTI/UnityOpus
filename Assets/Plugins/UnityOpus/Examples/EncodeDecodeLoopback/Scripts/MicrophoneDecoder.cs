using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityOpus.Example {
    [RequireComponent(typeof(MicrophoneEncoder))]
    public class MicrophoneDecoder : MonoBehaviour {
        public event Action<float[], int> OnDecoded;

        const NumChannels channels = NumChannels.Mono;

        MicrophoneEncoder encoder;
        Decoder decoder;
        readonly float[] pcmBuffer = new float[Decoder.maximumPacketDuration * (int)channels];

        void OnEnable() {
            encoder = GetComponent<MicrophoneEncoder>();
            encoder.OnEncoded += OnEncoded;
            decoder = new Decoder(
                SamplingFrequency.Frequency_48000,
                NumChannels.Mono);
        }

        void OnDisable() {
            encoder.OnEncoded -= OnEncoded;
            decoder.Dispose();
            decoder = null;
        }

        void OnEncoded(byte[] data, int length) {
            var pcmLength = decoder.Decode(data, length, pcmBuffer);
            OnDecoded?.Invoke(pcmBuffer, pcmLength);
        }
    }
}
