using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public AudioClip[] songs = new AudioClip[5];

    private AudioSource audioSourceStatic;
    private AudioSource audioSourceLevel;
    private float musicChangeSpeed = 2f;
    private int fading;

    // Use this for initialization
    void Start()
    {

        DontDestroyOnLoad(this);
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSourceStatic = audioSources[0];
        audioSourceLevel = audioSources[1];

        //play the menu music
        audioSourceStatic.clip = songs[0];
        fading = 1;
        audioSourceStatic.Play();



    }

    // Update is called once per frame
    void Update()
    {
        FadeMusic();
        
    }


    void FadeMusic()
    {
        
        //Check if currently fading music
        if (fading > 0)
        {

            //If the game is just starting, both volumes are 0
            if (fading == 1)
            {

                audioSourceStatic.volume = Mathf.Lerp(audioSourceStatic.volume, .8f, musicChangeSpeed * Time.deltaTime);

                if (audioSourceStatic.volume > .7f)
                {
                    fading = 0;
                }
            }

            //If transitioning to a level, static > 0
            if (fading == 2)
            {

                audioSourceLevel.volume = Mathf.Lerp(audioSourceLevel.volume, .8f, musicChangeSpeed * Time.deltaTime);
                audioSourceStatic.volume = Mathf.Lerp(audioSourceStatic.volume, 0f, musicChangeSpeed * Time.deltaTime);

                if (audioSourceLevel.volume > .7f && audioSourceStatic.volume < .1f)
                {
                    fading = 0;
                    audioSourceLevel.volume = .8f;
                    audioSourceStatic.volume = 0f;

                }
            }

            //If transitioning to a menu, level > 0
            if (fading == 3)
            {
                audioSourceStatic.volume = Mathf.Lerp(audioSourceStatic.volume, .8f, musicChangeSpeed * Time.deltaTime);
                audioSourceLevel.volume = Mathf.Lerp(audioSourceLevel.volume, 0f, musicChangeSpeed * Time.deltaTime);

                if (audioSourceStatic.volume > .7f && audioSourceLevel.volume < .1f)
                {
                    fading = 0;
                    audioSourceLevel.volume = .8f;
                    audioSourceStatic.volume = 0f;
                }
            }

        }
    }

    //levels call to change music to a level music song
    public void LevelMusic(int song)
    {
        audioSourceLevel.clip = songs[song];
        audioSourceLevel.Play();
        fading = 2;
    }

    public void MenuMusic()
    {
        fading = 3;
    }
    
}
