using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneeze : MonoBehaviour {
    public int minSneezeTimerValue;
    public int maxSneezeTimerValue;
    public int sneezeForce = 100;
    private float sneezeTimer;
    public float sneezeTimerStartingValue;
    private float reactionTimer;
    public float reactionTimerValue;
    public GameObject pointer;
    private Rigidbody2D pointerRigid;
    private bool reacting;
    public static bool sneezing;
    private Animator anim;
    public GameObject Sprite;

    void Start() {
        sneezeTimer = sneezeTimerStartingValue;
        reactionTimer = reactionTimerValue;
        reacting = false;
        anim = Sprite.GetComponent<Animator>();
        Random.InitState(12345);
    }

    void FixedUpdate() {
        if (PlayerMovement.dead)
            return;

        Indicator();
        SneezeTimer();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Spike") {
            Sprite.GetComponent<Renderer>().material.color = Color.white;
            pointer.SetActive(false);
        }
    }

    //SNEEZZINNGGG
    void Sneeze() {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        Vector2 trajectory = pz - this.transform.position;

        if (trajectory.y > 0)
            trajectory = new Vector2(trajectory.x, -0.001f);

        trajectory.Normalize();

        pointerRigid = GetComponent<Rigidbody2D>();
        pointerRigid.AddForce(-trajectory * sneezeForce);
        transform.position = transform.position + new Vector3(0, 0.001f, 0);
    }

    //This methode decatates the motion/rotation of the sneeze indicator
    void Indicator() {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz = pz - this.transform.position;
        pz.z = 0;
        float Angle = Vector3.Angle(pointer.transform.position - this.transform.position, pz);
        Vector3 cross = Vector3.Cross(pointer.transform.position - this.transform.position, pz);
        if (cross.z < 0) {
            Angle = -Angle;
        }

        pointer.transform.RotateAround(this.transform.position, Vector3.forward, Angle);
    }

    //Handeling the sneezing timer
    void SneezeTimer() {
        if (reactionTimer > 0 && reactionTimer <= (reactionTimerValue * 0.5f)) {
            anim.SetBool("BuildingUp", false);
            anim.SetBool("Sneezing", true);
        }

        if (reactionTimer <= 0) {
            if (reacting) {
                reacting = false;
                sneezing = true;
                Sneeze();
            }
            if (sneezeTimer <= 0) {
                anim.SetBool("BuildingUp", true);
                reactionTimer = reactionTimerValue;
                Sprite.GetComponent<Renderer>().material.color = Color.red;
                sneezeTimer = Random.Range(minSneezeTimerValue, maxSneezeTimerValue);
                reacting = true;
                pointer.SetActive(true);
            } else {
                sneezeTimer -= Time.deltaTime * 100;
                Sprite.GetComponent<Renderer>().material.color = Color.white;
                pointer.SetActive(false);
            }
        } else {
            reactionTimer -= Time.deltaTime * 100;
        }
    }
}