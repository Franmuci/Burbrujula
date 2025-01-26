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
        else if (Instance != this) Destroy(gameObject);

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
                PlayOnce(0, TYPEOFAUDIO.MUSIC);
                break;
            case 1:
                PlayOnce(1, TYPEOFAUDIO.SFX);
                break;
        }
    }

    public void PlayOnce(int audioSourceInList, TYPEOFAUDIO tOA)
    {

        if (tOA == TYPEOFAUDIO.MUSIC) audioSourceList[audioSourceInList].volume = musicVolumen / 100;
        else if (tOA == TYPEOFAUDIO.SFX) audioSourceList[audioSourceInList].volume = sfxVolumen / 100;

        audioSourceList[audioSourceInList].Play();
    }
    
    public void PlayLoop(int audioSourceInList, TYPEOFAUDIO tOA)
    {
        audioSourceList[audioSourceInList].loop = true;

        if (tOA == TYPEOFAUDIO.MUSIC) audioSourceList[audioSourceInList].volume = musicVolumen / 100;
        else if (tOA == TYPEOFAUDIO.SFX) audioSourceList[audioSourceInList].volume = sfxVolumen / 100;

        audioSourceList[audioSourceInList].Play();
    }

    public void StopAudio(int audioSourceInList)
    {
        audioSourceList[audioSourceInList].loop = false;
        audioSourceList[audioSourceInList].Stop();
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
