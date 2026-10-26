using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource sfx;

    public AudioClip backgroundMusic;
    public AudioClip eatingSound;
    public AudioClip bumpingSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip firecracker_fire;

    [SerializeField] private AudioClip expSound;
    [SerializeField] private AudioClip doctorExpSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource1.clip = backgroundMusic;
        audioSource1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playEating()
    {
        sfx.PlayOneShot(eatingSound);
    }
    public void playBumping()
    {
        sfx.PlayOneShot(bumpingSound);
    }
    public void playWin()
    {
        sfx.PlayOneShot(winSound);
    }
    public void playLose()
    {
        sfx.PlayOneShot(loseSound);
    }
    public void playDoctorExpSound()
    {
        sfx.PlayOneShot(doctorExpSound);
    }
    public void playFirecracker_fire()
    {
        sfx.PlayOneShot(firecracker_fire, 2);
    }
    public void playExp()
    {
        sfx.PlayOneShot(expSound, 3);
    }
    public void playExp2()
    {
        sfx.PlayOneShot(expSound, 4);
    }

    // ===================

    public void stopBackgroundMusic()
    {
        audioSource1.Stop();
    }
    public void stopSFXSound()
    {
        sfx.Stop();
    }
}
