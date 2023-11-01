﻿using Scripts.BaseGameScripts.UiManagement;
using Scripts.GameScripts;

namespace Scripts.BaseGameScripts.GameStateManagement.States
{
    public class FailState : IGameState
    {
        public void OnEnter()
        {
            UiActionManager.showUiItem?.Invoke(Defs.UI_KEY_FAIL_SCREEN, null);
        }

        public void OnExit()
        {
            UiActionManager.hideUiItem?.Invoke(Defs.UI_KEY_FAIL_SCREEN, null);
        }
    }
}