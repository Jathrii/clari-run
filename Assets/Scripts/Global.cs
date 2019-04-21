using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
    public AudioClip Dead;
    public AudioClip Jumping;
    public AudioClip SneezeBuildUp;
    public AudioClip Sneezing;
    public AudioClip Walking;

    public static Global instance;

    // Start is called before the first frame update
    void Start() {
        instance = this;
    }
}