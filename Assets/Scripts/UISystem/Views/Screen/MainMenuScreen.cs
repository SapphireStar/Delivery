using Cysharp.Threading.Tasks;
using Isekai.Managers;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.UI.Views.Screens
{
    public class GameStartEvent : IEventHandler
    {

    }
    public class MainMenuScreen : Screen<MainMenuViewModel>
    {
        public override void OnEnterScreen()
        {
            
        }

        public override void OnExitScreen()
        {
            
        }
        public void OnNewGameClicked()
        {
            Game.Instance.GoToPlayScene();
        }
        public void OnSettingsClicked()
        {
            SettingsViewModel viewmodel = new SettingsViewModel();
            ScreenManager.Instance.TransitionTo(EScreenType.SettingsScreen, ELayerType.DefaultLayer, viewmodel).Forget();
        }

    }

}
