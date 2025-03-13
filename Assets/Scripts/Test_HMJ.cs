using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HMJ : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.Instance.PlaySFX("Footsteps_DirtyGround_Jump_Land_01", transform.position);
        }        
        if(Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.Instance.PlaySFX("Footsteps_DirtyGround_Jump_Land_02", transform.position);
        }        
        if(Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.Instance.PlaySFX("Footsteps_DirtyGround_Jump_Start_03", transform.position);
        }
    }
}
