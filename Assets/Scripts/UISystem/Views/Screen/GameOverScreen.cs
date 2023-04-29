using Cysharp.Threading.Tasks;
using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.UI.Views.Screens
{
    public class GameOverScreen : Screen<GameOverViewModel>
    {
        public override void OnEnterScreen()
        {
        }

        public override void OnExitScreen()
        {
        }
        public void OnClickPlayAgain()
        {
            Game.Instance.GoToPlayScene();
        }
        public void OnClickBackToMainMenu()
        {
            Game.Instance.BackToMainMenu().Forget();
        }
    }

}
