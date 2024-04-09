using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Day of Week Splash", menuName = "Splash/Day of Week Splash")]
    public class SplashDay : SplashDetails
    {
        [SerializeField] private string[] m_daysOfWeek;

        public override string GetTitle(GameState _state)
            => string.Format(base.GetTitle(_state), m_daysOfWeek[_state.CurrentDay]);

        public override string GetSubtitle(GameState _state)
            => string.Format(base.GetSubtitle(_state), m_daysOfWeek[_state.CurrentDay]);
    }
}
