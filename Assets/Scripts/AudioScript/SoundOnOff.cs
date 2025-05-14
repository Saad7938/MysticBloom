using UnityEngine;


public class SoundOnOff : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    public void MusicOn(){
        music.loop = true;
        music.Play();
    }
public void MusicOff(){
    Debug.Log("MusicOff() called");
    music.Stop();
    music.loop = false;
    music.enabled = false; // Temporarily disable to prevent auto-play
}

    public void CloseOptions(){
        gameObject.SetActive(false);
    }
}
