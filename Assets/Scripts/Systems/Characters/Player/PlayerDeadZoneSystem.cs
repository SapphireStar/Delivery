using Cysharp.Threading.Tasks;
using Isekai.Components;
using Isekai.Managers;
using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadZoneSystem : BaseSystem
{
    private Camera m_mainCamera;
    private CombatComponent m_combatComponent;
    public override void Initialize()
    {
        m_combatComponent = GetSubComponent<CombatComponent>(Isekai.Factories.EComponent.CombatComponent);
        m_mainCamera = Camera.main;
    }
    private void Update()
    {
        checkPlayerDeadZone();
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Debug.Log(Input.mousePosition);
        }
    }

    private void checkPlayerDeadZone()
    {
        if (!m_combatComponent.IsDead)
        {
            
            var screenTop = Screen.height;
            var screenBottom = 0;
            var worldTop = m_mainCamera.ScreenToWorldPoint(new Vector2(0, screenTop));
            var worldBottom = m_mainCamera.ScreenToWorldPoint(new Vector2(0, screenBottom));
            //Debug.Log(worldTop + " " + worldBottom);
            if (transform.position.y > worldTop.y || transform.position.y < worldBottom.y)
            {
                Game.Instance.PauseGame();
                m_combatComponent.IsDead = true;
                Debug.Log("player dead");
                ScreenManager.Instance.TransitionToInstant(Isekai.UI.EScreenType.GameOverScreen, ELayerType.PopupLayer, new GameOverViewModel()).Forget();
            }
        }

    }
    public override void BindComponentProperty()
    {

    }

    public async override UniTaskVoid BindComponentPropertyAsync()
    {
    }



    public override void OnRemove()
    {
    }

    public override void UnbindComponentProperty()
    {
    }

}
