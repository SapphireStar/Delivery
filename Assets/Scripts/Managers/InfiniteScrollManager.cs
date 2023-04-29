using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollManager:MonoBehaviour
{
    public GameObject ChunkPrefab;
    private Camera m_mainCamera;
    private List<Transform> m_chunks;
    private void Start()
    {
        m_mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Game.Instance.GameStarted)
        {
            m_mainCamera.transform.position += Vector3.down * Time.deltaTime;
        }
        
    }
}
