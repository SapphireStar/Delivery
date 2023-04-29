using Cysharp.Threading.Tasks;
using Isekai.UI.ViewModels.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Isekai.Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public TransitionScreen TransitionScreen;
        public void Initialize()
        {

        }
        public async UniTask TransitionToScene(string sceneName, Action onTransitionComplete)
        {
            LoadingViewModel viewModel = new LoadingViewModel();
            await ScreenManager.Instance.TransitionTo(UI.EScreenType.LoadingScreen, ELayerType.DefaultLayer, viewModel);
            viewModel.OnLoadingComplete += async () =>
            {
                await TransitionScreen.TransitionEnter();
                ScreenManager.Instance.PopAllScreenInstant();
                await TransitionScreen.TransitionOut();
                onTransitionComplete?.Invoke();
            };

            var handle = SceneManager.LoadSceneAsync(sceneName);

            while (!handle.isDone)
            {
                viewModel.LoadingProgress = handle.progress;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            viewModel.LoadingProgress = 1;

        }
    }

}
