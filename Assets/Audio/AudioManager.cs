using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;
  public Sound[] sounds;

  // Start is called before the first frame update
  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
      return;
    }

    foreach (Sound sound in sounds)
    {
      sound.source = gameObject.AddComponent<AudioSource>();
      sound.source.clip = sound.clip;
      sound.source.volume = sound.volume;
      sound.source.pitch = sound.pitch;
      sound.source.loop = sound.loop;
    }
  }

  void Start()
  {
    DontDestroyOnLoad(gameObject);
    Play("Theme");
  }

  public void Play(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s != null)
    {
      s.source.Play();
    }
  }

  public void Stop(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s != null)
    {
      s.source.Stop();
    }
  }
}
