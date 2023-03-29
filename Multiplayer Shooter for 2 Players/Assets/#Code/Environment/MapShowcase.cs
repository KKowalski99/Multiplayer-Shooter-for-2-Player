using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowcase : MonoBehaviour, IEventListenable
{
   List<Camera> _cameras = new List<Camera>();
    int _cameraIndex;

    private void Start()
    {
        _cameras.AddRange(transform.GetComponentsInChildren<Camera>());

        InvokeRepeating(nameof(SwitchCameras), 0, 3);
    }

   void SwitchCameras()
    {
        foreach (var camera in _cameras)
        {
            if (camera != _cameras[_cameraIndex]) camera.gameObject.SetActive(false);
            else camera.gameObject.SetActive(true);
        }

        if (_cameraIndex < _cameras.Count - 1) _cameraIndex++;
        else _cameraIndex = 0;
    }

    public void OnEnable() => GameEvents.onPlayerEnabled.AddListener(DisableAfterPlayerSpawn);

    public void OnDisable() => GameEvents.onPlayerEnabled.RemoveListener(DisableAfterPlayerSpawn);

    void DisableAfterPlayerSpawn() => gameObject.SetActive(false);
    
}
