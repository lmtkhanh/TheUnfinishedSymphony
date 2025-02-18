using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip beat;
    public AudioClip hitSoundA;
    public AudioClip enemyNotePop;
    public AudioClip musicBlock;
    public AudioClip endEnemyNoteSpawn;

    public void playBeatSound(double playTime)
    {
        audioSource.clip = beat;
        audioSource.PlayScheduled(playTime);
    }

    public void playHitSoundA()
    {
        audioSource.clip = hitSoundA;
        audioSource.PlayOneShot(hitSoundA);
    }

    public void playEnemyNotePopSound()
    {
        audioSource.clip = enemyNotePop;
        audioSource.PlayOneShot(enemyNotePop);
    }

    public void playMusicBlockSound()
    {
        audioSource.clip = musicBlock;
        audioSource.PlayOneShot(musicBlock);
    }

    public void playEndEnemyNoteSpawnSound()
    {
        audioSource.clip = endEnemyNoteSpawn;
        audioSource.PlayOneShot(endEnemyNoteSpawn);
        Debug.Log("enemy note stop spawning!");
    }

}
