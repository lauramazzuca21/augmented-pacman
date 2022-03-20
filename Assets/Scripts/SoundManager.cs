using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Sound += CollectPoints;
        EventManager.ArrestedPlayer += StartSoundArrested;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CollectPoints(string tag)
    {        
        foreach(AudioClip audio in sounds)
        {
            if(audio.name == tag)
            {
                audioSource.clip = audio;
                audioSource.Play();
            }
        }      
    }

    private void StartSoundArrested()
    {
        foreach (AudioClip audio in sounds)
        {
            if (audio.name == "Police")
            {
                audioSource.clip = audio;
                audioSource.Play();
            }
        }
    }
}
