using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelWidget : MonoBehaviour
{
    [SerializeField]
    private Image m_leftAvatar;
    [SerializeField]
    private Image m_rightAvatar;
    [SerializeField]
    private TextMeshProUGUI m_talkText;
    [SerializeField]
    private TextMeshProUGUI m_speakerName;

    private CancellationTokenSource m_tokenSource;
    private string curText;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Initialize(true, "hello world hello world hello world hello world hello world", "You:", null);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_tokenSource.Cancel();
            m_talkText.text = curText;
        }
    }
    public void Initialize(bool isSelf, string talkText, string speakerName, Sprite avatar)
    {
        if (isSelf)
        {
            m_rightAvatar.gameObject.SetActive(false);
            m_leftAvatar.gameObject.SetActive(true);
            m_leftAvatar.sprite = avatar;
        }
        else
        {
            m_leftAvatar.gameObject.SetActive(false);
            m_rightAvatar.gameObject.SetActive(true);
            m_rightAvatar.sprite = avatar;
        }
        curText = talkText;
        m_talkText.text = curText;

        //StartShowText
        if (m_tokenSource != null)
        {
            m_tokenSource.Cancel();
            m_tokenSource.Dispose();
        }
        m_tokenSource = new CancellationTokenSource();
        SetTalkText().Forget();

    }

    public async UniTaskVoid SetTalkText()
    {
        for (int i = 0; i < curText.Length; i++)
        {
            m_talkText.text = curText.Substring(0, i + 1);
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f),false,PlayerLoopTiming.Update, m_tokenSource.Token);
        }
    }
    
}
