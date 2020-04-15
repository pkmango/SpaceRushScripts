using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer soundsMixer;
    public AudioMixer musicMixer;
    public Toggle toggleMusic;
    public Toggle toggleSound;

    private int soundsKey;
    private int musicKey;

    void Start()
    {
        soundsKey = PlayerPrefs.GetInt("soundsKey", 1);
        SetSoundsAfterLoad();
        musicKey = PlayerPrefs.GetInt("musicKey", 1);
        SetMusicAfterLoad();
    }

    public void SetSoundsAfterLoad()
    {
        if (soundsKey == 1)
        {
            soundsMixer.SetFloat("soundsVolume", 0f); // Включаем все звуки
            toggleSound.isOn = true;
        }
        else
        {
            soundsMixer.SetFloat("soundsVolume", -80f);  // Выключаем все звуки
            toggleSound.isOn = false;
        }
    }

    public void SetMusicAfterLoad()
    {
        if (musicKey == 1)
        {
            musicMixer.SetFloat("musicVolume", 0f); // Включаем всю музыку
            toggleMusic.isOn = true;
        }
        else
        {
            musicMixer.SetFloat("musicVolume", -80f);  // Выключаем всю музыку
            toggleMusic.isOn = false;
        }
    }

    public void ToggleSounds()
    {

        if (soundsKey == 1)
        {
            soundsMixer.SetFloat("soundsVolume", -80f);  // Выключаем все звуки
            toggleSound.isOn = false;
            soundsKey = 0;
        }
        else
        {
            soundsMixer.SetFloat("soundsVolume", 0f); // Включаем все звуки
            toggleSound.isOn = true;
            soundsKey = 1;
        }
        PlayerPrefs.SetInt("soundsKey", soundsKey);
    }

    public void ToggleMusic()
    {
        if (musicKey == 1)
        {
            musicMixer.SetFloat("musicVolume", -80f);  // Выключаем всю музыку
            toggleMusic.isOn = false;
            musicKey = 0;
        }
        else
        {
            musicMixer.SetFloat("musicVolume", 0f); // Включаем всю музыку
            toggleMusic.isOn = true;
            musicKey = 1;
        }
        PlayerPrefs.SetInt("musicKey", musicKey);
    }
}