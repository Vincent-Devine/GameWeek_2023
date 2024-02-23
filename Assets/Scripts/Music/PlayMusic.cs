using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    bool themeIsPlaying;
    [SerializeField] EventReference levelTheme;
    private static PlayMusic instance;
    // FMOD Studio System
    private FMOD.Studio.System studioSystem;
    FMOD.Studio.EventInstance eventInstance;
    private FMOD.Studio.PARAMETER_ID parameterId;

    private void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize FMOD Studio System
        FMOD.Studio.System.create(out studioSystem);
        studioSystem.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, System.IntPtr.Zero);
    }

    private void Start()
    {
        themeIsPlaying = false;

        FMOD.Studio.EventDescription eventDescription;
        eventInstance.getDescription(out eventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
        eventDescription.getParameterDescriptionByName("PHASES", out parameterDescription);
        parameterId = parameterDescription.id;

    }

    public void PlayMusicEvent(string eventName)
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

    private void Update()
    {
        if (!themeIsPlaying)
        {
            eventInstance = FMODUnity.RuntimeManager.CreateInstance(levelTheme);
            eventInstance.start();
            eventInstance.release();
            themeIsPlaying = true;
        }
        if (Time.timeScale == 0)
        {
            GetComponent<PlayMusic>().SwitchAudioPhase("PAUSE", 1);
        }
        else
        {
            GetComponent<PlayMusic>().SwitchAudioPhase("PAUSE", 0);
        }

    }

    public void SwitchAudioPhase(string name, float value)
    {
        // Set the parameter value to control the audio phase
        eventInstance.setParameterByName(name, value);
    }

}
