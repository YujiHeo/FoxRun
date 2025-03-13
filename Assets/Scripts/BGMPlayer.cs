using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.bgmSource = this.GetComponent<AudioSource>();
        SoundManager.Instance.PlayBGM(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
