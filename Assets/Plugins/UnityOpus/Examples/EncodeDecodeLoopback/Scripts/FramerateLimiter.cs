using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityOpus.Example {
    public class FramerateLimiter : MonoBehaviour {
        public int targetFramerate = 90;

        void Awake() {
            Application.targetFrameRate = targetFramerate;
        }
    }
}
