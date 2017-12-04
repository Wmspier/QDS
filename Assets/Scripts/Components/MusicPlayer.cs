using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioMixerGroup guitare;
    public AudioMixerGroup teremin;
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

    }
    
    public void PlayCatch(GameEvents.ElementCollisionEvent e)
    {
        int randClip = Random.Range(0, catches.Length);
        Debug.Log("Rand " + randClip);
        catchesSource.clip = catches[randClip];
        catchesSource.Play();
    }


}