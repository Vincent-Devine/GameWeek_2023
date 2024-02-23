using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] EventReference levelTheme;
    private static PlaySound instance;
    private FMOD.Studio.System studioSystem;
    FMOD.Studio.EventInstance eventInstance;

    private void Awake()
    {
        // Initialize FMOD Studio System
        FMOD.Studio.System.create(out studioSystem);
        studioSystem.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, System.IntPtr.Zero);
    }

    public void PlaySoundEvent(string eventName)
    {
        // Start the FMOD event
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventName);
        eventInstance.start();
        eventInstance.release();
    }

    private void OnDestroy()
    {
        studioSystem.release();
    }

}
