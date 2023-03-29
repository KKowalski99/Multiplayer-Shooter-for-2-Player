using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TextureScrolling : MonoBehaviour
{
    [SerializeField] float _scrollX = 0.5f;
    [SerializeField] float _scrollY = 0.5f;

    MeshRenderer _meshRenderer;
    private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();
    void Update() => _meshRenderer.material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * _scrollX, Time.realtimeSinceStartup * _scrollY);


}
