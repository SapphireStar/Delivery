using Cysharp.Threading.Tasks;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Managers
{
    public class PauseGameEvent : IEventHandler
    {

    }
    public class Game : MonoSingleton<Game>
    {
        public bool GameStarted;
        async void Start()
        {
            await InitializeManagers();

            MainMenuViewModel viewmodel = new MainMenuViewModel();
            await ScreenManager.Instance.TransitionToInstant(UI.EScreenType.MainMenuScreen,ELayerType.DefaultLayer, viewmodel);

            SoundManager.Instance.PlayMusic(SoundDefine.Music_Login);
        }

        public async UniTask InitializeManagers()
        {
            await LayerManager.Instance.Initialize();
            ScreenManager.Instance.Initialize();
            ResourceManager.Instance.Initialize();
            await SettingsManager.Instance.Initialize();
            SoundManager.Instance.Initialize();
            LevelManager.Instance.Initialize();
        }
        public async UniTaskVoid BackToMainMenu()
        {
            await LevelManager.Instance.TransitionToScene("MainMenu",null);
            ScreenManager.Instance.TransitionToInstant(UI.EScreenType.MainMenuScreen, ELayerType.DefaultLayer, new MainMenuViewModel()).Forget();
        }
        public void GoToPlayScene()
        {
            LevelManager.Instance.TransitionToScene("PlayScene",
                () => GameStarted = true).Forget();
        }
        public void PauseGame()
        {
            GameStarted = false;
        }
        public void ResumeGame()
        {
            GameStarted = true;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SettingsManager.Instance.SaveSettings();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SettingsManager.Instance.SetSettingByKey<bool>("MusicOn", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SettingsManager.Instance.SetSettingByKey<bool>("MusicOn", false);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log(SettingsManager.Instance.GetSettingByKey<bool>("MusicOn"));
            }
        }

    }

}
