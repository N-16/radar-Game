using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour{

    [SerializeField] public List<Sound> soundFx;
    [SerializeField] public List<Sound> ambience;
    [SerializeField] public List<Sound> soundTrack;

    private static SoundManager _instance;

    public static SoundManager Instance{
        get{
            if (_instance == null){
                Debug.LogError("Null Instance!");
            }
            return _instance;
        }
    }
    private void Awake(){
        _instance = this;

        foreach (Sound sound in soundFx)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.looping;
            sound.soundType = soundType.soundFx;
        }

        foreach (Sound sound in ambience)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.looping;
            sound.soundType = soundType.ambience;
        }

        foreach (Sound sound in soundTrack)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.looping;
            sound.soundType = soundType.soundTrack;
        }       
    }

    public void PlaySound(soundType soundType, string soundName, Action afterSoundPlayed = null)
    {
        var soundArray = soundType == soundType.soundFx ? soundFx : soundType == soundType.ambience ? ambience : soundTrack;
        foreach (Sound sound in soundArray)
        {
            if (soundName == sound.soundName)
            {
                sound.source.volume = sound.volume;
                sound.source.Play();
                if (afterSoundPlayed != null)
                    StartCoroutine(DoAfterSoundPlayedRoutine(sound, afterSoundPlayed));
                return;
            }
            
        }
        Debug.LogError("sound not found");
    }

    public void PauseSound(soundType soundType, string soundName)
    {
        var soundArray = soundType == soundType.soundFx ? soundFx : soundType == soundType.ambience ? ambience : soundTrack;
        foreach (Sound sound in soundArray)
        {
            if (soundName == sound.soundName)
            {
                sound.source.Pause();
                return;
            }
            
        }
        Debug.LogError("sound not found");
    }
    public void StopSound(soundType soundType, string soundName)
    {
        var soundArray = soundType == soundType.soundFx ? soundFx : soundType == soundType.ambience ? ambience : soundTrack;
        foreach (Sound sound in soundArray)
        {
            if (soundName == sound.soundName)
            {
                sound.source.Pause();
                return;
            }
            
        }
        Debug.LogError("sound not found");
    }

    public void SmoothStop(soundType soundType, string soundName, float timeToStop, Action toDoAfterStop = null){
        var soundArray = soundType == soundType.soundFx ? soundFx : soundType == soundType.ambience ? ambience : soundTrack;
        foreach (Sound sound in soundArray)
        {
            if (soundName == sound.soundName){
                StartCoroutine(TweenVolumeRoutine(sound, timeToStop, 0f, toDoAfterStop));
                return;
            }
            
        }
        Debug.LogError("sound not found");
    }

    public void SmoothStart(soundType soundType, string soundName, float timeToFullVolume, Action toDoAfterStop = null){
        var soundArray = soundType == soundType.soundFx ? soundFx : soundType == soundType.ambience ? ambience : soundTrack;
        foreach (Sound sound in soundArray)
        {
            if (soundName == sound.soundName){
                sound.source.volume = 0f;
                sound.source.Play();
                StartCoroutine(TweenVolumeRoutine(sound, timeToFullVolume, sound.volume, toDoAfterStop));
                return;
            }
            
        }
        Debug.LogError("sound not found");
    }

    public void Crossfade(string from, soundType fromSoundType ,string to, soundType toSoundType, float time, Action toDoAfterCrossfade = null){
        SmoothStart(toSoundType, to, time, toDoAfterCrossfade);
        SmoothStop(fromSoundType, from, time);
    }
    IEnumerator DoAfterSoundPlayedRoutine(Sound sound, Action toDo){
        while(sound.source.isPlaying){
            yield return null;
        }
        toDo?.Invoke();
    }

    IEnumerator TweenVolumeRoutine(Sound sound, float time, float volumeTo, Action toDoAfterDone){
        float startTime = Time.time;
        float startVolume = sound.source.volume; Debug.Log("Start Volume : " + sound.source.volume);
        while(Time.time - startTime < time){
            sound.source.volume = Mathf.Lerp(startVolume, volumeTo, Mathf.InverseLerp(startTime, startTime + time, Time.time));
            //Debug.Log("Volume tweened : " + sound.source.volume);
            yield return null;
        }
        toDoAfterDone?.Invoke();
        if (volumeTo == 0f) 
            sound.source.Stop();
    }    
}

[System.Serializable]
public class Sound{
    [Range(0f, 1f)] public float volume;
    public AudioClip clip;
    public bool looping;
    public string soundName;
    [HideInInspector] public soundType soundType;
    [HideInInspector] public AudioSource source;
}

public enum soundType{
    soundFx, 
    ambience, 
    soundTrack
}