using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MusicManager : MonoBehaviour
{
    private AudioSource source;
    private int index;
    private bool focus = true;

    public bool playing = true;
    public List<AudioClip> clips;

    void Awake()
    {
        source = this.GetComponents<AudioSource>()[0];
        index = UnityEngine.Random.Range(0, clips.Count);
    }

    void Update()
    {
        if (focus)
        {
            if (!playing && source.isPlaying)
            {
                source.Stop();
            }
            else if (playing && !source.isPlaying && clips.Count > 0)
            {
                index = (index + 1) % clips.Count;
                source.clip = clips[index];
                source.Play();
            }
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        focus = hasFocus;
    }
}