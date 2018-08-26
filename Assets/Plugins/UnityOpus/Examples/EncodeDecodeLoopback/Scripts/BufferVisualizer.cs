using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityOpus.Example {
    public class BufferVisualizer : MonoBehaviour {
        public LoopbackPlayer loopbackPlayer;

        void Update() {
            transform.localScale = new Vector3(1.0f, loopbackPlayer.bufferCount / 1000.0f, 1.0f);
        }
    }
}
