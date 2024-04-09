using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Work Selector", menuName = "Microgame Selectors/Work Selector")]
    public class WorkSelector : MicrogameSelector
    {
        [SerializeField] private string m_hungryGame = "lunch";
        [SerializeField] private string m_nextGame;

        public override string Select(GameState _state)
        {
            if (_state.Hungry) { return m_hungryGame; }

            _state.PaperStackSize = 0;
            return m_nextGame;
        }
    }
}
