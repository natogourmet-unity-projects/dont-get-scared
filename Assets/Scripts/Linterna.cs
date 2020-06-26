using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linterna : MonoBehaviour
{
    public AudioSource lanternAudio;
    public AudioClip[] lanternSounds;
    public Light linterna;
    public Slider battery;
    public float timeOn;
    public float rechargeTime;
    private bool discharge = false;

    public Camera mc;
    public bool justTriggered = false;

    private void Start()
    {
        mc = Camera.main;
    }

    void Update()
    {
        if (linterna.isActiveAndEnabled)
        {
            battery.value -= (1f / timeOn) * Time.deltaTime;
            if (battery.value <= 0)
            {
                TurnLantern();
                discharge = true;
            }
        }
        else
        {
            battery.value += (1f / rechargeTime) * Time.deltaTime;
            if (discharge && battery.value >= 1) discharge = false;
        }

        float xRot = mc.transform.rotation.eulerAngles.x;

        if (!discharge && !justTriggered && (xRot < 345f && xRot > 270f))
        {
            justTriggered = true;
            TurnLantern();
        }

        if (justTriggered && xRot > 345f)
        {
            justTriggered = false;
        }
    }

    public void TurnLantern()
    {
        lanternAudio.clip = lanternSounds[(linterna.gameObject.activeSelf) ? 0 : 1];
        lanternAudio.Play();
        linterna.enabled = !linterna.enabled;
    }
}
