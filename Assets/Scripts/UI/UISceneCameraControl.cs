using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneCameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator cameraAnimator = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraAnimator != null)
        {
            cameraAnimator.SetBool("play", UIManager.instance.uISceneCameraPlay);
        }
    }
}
