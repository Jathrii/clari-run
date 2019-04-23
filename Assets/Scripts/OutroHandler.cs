using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroHandler : MonoBehaviour
{
    public Text text;
    public Text text2;
    private bool next;
    public Animator finalAnimator;
    // Start is called before the first frame update
    void Start()
    {
        next = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            if (!next)
            {
                text.text = "...And its JUST 399 EGP";
                next = true;
                finalAnimator.SetBool("Next", true);
                text2.text="press A to BUY NOW";
            }
            else
            {
                SceneManager.LoadScene("Credits");
            }
            
        }
        
    }
}
