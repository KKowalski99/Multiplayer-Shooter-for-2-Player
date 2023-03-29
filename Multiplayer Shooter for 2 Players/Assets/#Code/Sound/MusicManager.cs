using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class MusicManager : MonoBehaviour
{
    Sound _sound = new Sound();
    MusicTracksPathData _trackData = new MusicTracksPathData();
    Queue<string> tracks = new Queue<string>(); 

    void Start()
    {
        if (TryGetComponent(out AudioSource audioSorce)) { _sound.source = audioSorce; }
        else _sound.source = gameObject.AddComponent<AudioSource>();

        foreach (string address in _trackData.addressData)
        {
            tracks.Enqueue(address);
        }
       
    }
   void AsyncOperationHandleCompleted(AsyncOperationHandle<AudioClip> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            _sound.source.clip = asyncOperationHandle.Result;
            PlayTrack();
        }
        else Logger.LogError("Object could not be loaded", this);

        ChangeMusicOrder();
    }

    bool _trackSwitchingRequested;
    private void Update()
    {
        if (_sound.source.isPlaying) return;

        if (_trackSwitchingRequested == false)
        {
            SwitchTrack();
            _trackSwitchingRequested = true;
        }
    }
    void SwitchTrack()
    {
        AsyncOperationHandle<AudioClip> async = Addressables.LoadAssetAsync<AudioClip>(tracks.Peek()); ;
        async.Completed += AsyncOperationHandleCompleted;
     
    }

    void ChangeMusicOrder()
    {
        string temp = tracks.Peek();
        tracks.Dequeue();
        tracks.Enqueue(temp);
    }
    void PlayTrack()
    {
        _sound.source.Play();
        _trackSwitchingRequested = false;
    }
    

}

internal record MusicTracksPathData
{
    internal string[] addressData =
    {
        "Assets/Sound/Music/fare_twilight.mp3",
        "Assets/Sound/Music/not_alone.mp3",
        "Assets/Sound/Music/psyaddict.mp3"
    };

}