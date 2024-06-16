using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Button back, instruction;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private GameObject instructionPanel;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Start()
    {
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        back.onClick.AddListener(Back);
        musicSlider.onValueChanged.AddListener(MusicVolume);
        sfxSlider.onValueChanged.AddListener(SFXVolume);
        instruction.onClick.AddListener(() => { instructionPanel.SetActive(true); });

    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality+1);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Not found any sound");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Not found any sound");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ButtonSoundClick()
    {
        PlaySfx(ConstantSound.BUTTON_CLICK);
    }

    private void Back()
    {
        settingPanel.SetActive(false);
        ButtonSoundClick();
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
