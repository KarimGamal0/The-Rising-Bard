using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEditor;



public class AudioManager : MonoBehaviour
{





    public static AudioManager instance;



    public Sound[] sounds;




    //public AudioMixerGroup mixerGroup;

    public AudioClip[] clips;



    private void OnEnable()
    {
        ItemPickSoundHandler.PlaySoundEvent += PlayMixed;


    }
    private void OnDisable()
    {
        ItemPickSoundHandler.PlaySoundEvent -= PlayMixed;
    }
    void Awake()
    {


        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        int index = 0;


        foreach (Sound s in sounds)
        {
            if (index < clips.Length)
            {

                s.source = gameObject.AddComponent<AudioSource>();
                s.clip = clips[index];
                s.name = clips[index].name;

                s.source.clip = s.clip;
                Debug.Log(clips[index].name);
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.mixerGroup;
                index++;
            }

        }
    }

    public void Play(string sound)
    {

        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.Log("Sound: " + sound + " not found! Please check " + name + "Object in Scene");
            return;
        }


        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        //
        if (s.source.isPlaying == false)
        {
            if (sound == "walk")
            {
                s.source.time = 23.5f;
            }
            s.source.Play();

        }
        if (sound == "jump")
        {
            s.source.PlayOneShot(s.source.clip);
        }

    }




    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.Log("Sound: " + sound + " not found! Please check " + name + "Object in Scene");
            return;
        }

        s.source.Stop();


    }




    public void PlayMixed(string sound)
    {

        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.Log("Sound: " + sound + " not found! Please check " + name + "Object in Scene");
            return;
        }




 
 
        s.source.PlayOneShot(s.source.clip);


    }


}
