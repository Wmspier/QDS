using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioMixerGroup guitare;
    public AudioMixerGroup theremin;
    public AudioMixerGroup ukulele;
    public AudioClip[] catches;
    public AudioSource catchesSource;
    public float bpm = 128;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;


    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;

        EventSystem.instance.Connect<GameEvents.ElementCollisionEvent>(PlayCatch);
        EventSystem.instance.Connect<GameEvents.PlanetStructureEvent>(UpdateMusicBalance);

    }
    
    public void PlayCatch(GameEvents.ElementCollisionEvent e)
    {
        if (catches.Length == 0)
        {
            Debug.Log("Sound: No Catch sound");
            return;
        }
        int randClip = Random.Range(0, catches.Length);
        catchesSource.clip = catches[randClip];
        catchesSource.Play();
    }

    public void UpdateMusicBalance(GameEvents.PlanetStructureEvent e)
    {
        for(int i = 0; i < e.ElementDistributionList.Count; i++)
        {
            float value = Mathf.Round((float)e.ElementDistributionList[i] / (float) e.Total * 100f);
            switch(i)
            {
                case 0: SetGuitareVolume(value - 80); break;
                case 1: SetThereminVolume(value - 80); break;
                case 2: break;
                case 3: SetUkuleleVolume(value - 80); break;
            }
            
            Debug.Log("Test " + value);
        }
        
    }

    public void SetGuitareVolume (float volume)
    {
        guitare.audioMixer.SetFloat("GuitareVolume", volume);
    }

    public void SetThereminVolume(float volume)
    {
        theremin.audioMixer.SetFloat("ThereminVolume", volume);
    }

    public void SetUkuleleVolume(float volume)
    {
        guitare.audioMixer.SetFloat("UkuleleVolume", volume);
    }
}