using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField]
    private DialoguePanelWidget m_dialoguePanel;
    async void Start()
    {
        var prefab = await ResourceManager.Instance.LoadResourceAsync<GameObject>("DialoguePanelWidget");
        m_dialoguePanel = Instantiate(prefab, LayerManager.Instance.GetLayer(ELayerType.PopupLayer)).GetComponent<DialoguePanelWidget>();
        m_dialoguePanel.gameObject.SetActive(false);
    }
    public void Initialize()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
