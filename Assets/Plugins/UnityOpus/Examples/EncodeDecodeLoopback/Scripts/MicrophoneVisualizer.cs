using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityOpus.Example {
    public class MicrophoneVisualizer : MonoBehaviour {
        public MicrophoneRecorder recorder;

        void Update() {
            transform.localScale = new Vector3(1, recorder.GetRMS() * 100.0f, 1);
        }
    }
}
