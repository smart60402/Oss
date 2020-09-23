﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroPlayer : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public Image Fade;
    public Image BeforeImageReadyBlack;
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer.prepareCompleted += (VideoPlayer source) =>
        {
            AudioUtilities.PlayAudio(Audios.Instance.Triangles);
        };
        VideoPlayer.loopPointReached += (VideoPlayer source) =>
        {
            VideoPlayer.targetCameraAlpha = 0;
            Fade.GetComponent<Animator>().SetTrigger("Fading");
            BeforeImageReadyBlack.color = new Color(0, 0, 0, 0);
            Destroy(this.gameObject);
        };
        VideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
