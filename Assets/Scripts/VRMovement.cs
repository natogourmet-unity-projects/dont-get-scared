using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovement : MonoBehaviour
{
    public Camera mc;
    public float speed;

    public AudioSource audio;
    public AudioSource extra;
    public AudioClip walkSFX;
    public AudioClip runSFX;

    private bool tired = false;
    private bool running = false;
    private float runningTime = 0;

    void Start()
    {
        mc = Camera.main;
    }

    void Update()
    {
        float xRot = mc.transform.rotation.eulerAngles.x;
        if (xRot > 15f && xRot < 90f)
        {
            Vector3 dir = mc.transform.rotation * Vector3.forward;
            dir.y = 0;
            dir.Normalize();
            if (!tired && xRot > 30f)
            {
                StartRunning();
                dir *= 2;
                runningTime += Time.deltaTime;
                if (runningTime >= 10)
                {
                    StartCoroutine(TimeResting());
                    tired = true;
                    extra.Play();
                    running = false;
                    StopRunning();
                }
            }
            else if (tired) dir /= 2;
            else
            {
                if (audio.clip == runSFX)
                {
                    audio.clip = walkSFX;
                }

            }


            if (!audio.isPlaying)
            {
                audio.Play();
            }

            this.transform.Translate(dir * speed * Time.deltaTime);
            Debug.Log(dir);
        }
        else if (audio.isPlaying)
        {
            audio.Stop();
        }
    }

    IEnumerator TimeResting()
    {
        yield return new WaitForSeconds(5);
        tired = false;
    }

    public void StartRunning()
    {
        if (audio.clip == walkSFX)
        {
            audio.clip = runSFX;
        }
    }

    public void StopRunning()
    {
        if (audio.clip == runSFX)
        {
            audio.clip = walkSFX;
        }
        runningTime = 0;
    }
}
