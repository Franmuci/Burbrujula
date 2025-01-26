using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TYPEOFAUDIO
{
    SFX, MUSIC
}

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public AudioManager Instance;

    [SerializeField] List<AudioSource> audioSourceList = new();

    [Range(0, 100)]
    [SerializeField] float musicVolumen;
    [Range(0, 100)]
    [SerializeField] float sfxVolumen;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"La escena {scene.name} ha terminado de cargar.");

        //Añade todos los audio sources
        AddAllAudioSource();

        switch (scene.buildIndex)
        {
            case 0:
                audioSourceList.ForEach(aS =>
                {
                    if (aS == transform.GetChild(0)) aS.Play();
                });
                break;
            case 1:
                audioSourceList.ForEach(aS =>
                {
                    if (aS == transform.GetChild(1)) aS.Play();
                });
                break;
        }
    }

    public void PlayOnce(AudioSource audioSource, TYPEOFAUDIO tOA)
    {
        audioSourceList.ForEach(element =>
        {
            if (audioSource == element && tOA == TYPEOFAUDIO.MUSIC)
            {
                element.volume = musicVolumen;
                element.Play();
            }
            else if (audioSource == element && tOA == TYPEOFAUDIO.SFX)
            {
                element.volume = sfxVolumen;
                element.Play();
            }
        });
    }
    
    public void PlayLoop(AudioSource audioSource, TYPEOFAUDIO tOA)
    {
        audioSourceList.ForEach(element =>
        {
            if (audioSource == element && tOA == TYPEOFAUDIO.MUSIC)
            {
                element.volume = musicVolumen;
                element.loop = true;
                element.Play();
            }
            else if (audioSource == element && tOA == TYPEOFAUDIO.SFX)
            {
                element.volume = sfxVolumen;
                element.loop = true;
                element.Play();
            }
        });
    }

    public void StopAudio(AudioSource audioSource)
    {
        audioSourceList.ForEach(element =>
        {
            if (audioSource == element)
            {
                element.loop = false;
                element.Stop();
            }
        });
    }

    public void SetMusicVolumen(float value)
    {
        musicVolumen = value;
        if (musicVolumen > 100) musicVolumen = 100;
        if (musicVolumen < 0) musicVolumen = 0;
    }
    
    public void SetSfxVolumen(float value)
    {
        sfxVolumen = value;
        if (sfxVolumen > 100) sfxVolumen = 100;
        if (sfxVolumen < 0) sfxVolumen = 0;
    }

    void AddAllAudioSource()
    {
        foreach (AudioSource aS in FindObjectsByType<AudioSource>(0))
        {
            if (!audioSourceList.Contains(aS)) audioSourceList.Add(aS);
        }
    }
}
