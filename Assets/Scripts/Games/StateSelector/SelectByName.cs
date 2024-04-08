using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Select by Name", menuName = "Microgame Selectors/Select by Name")]
    public class SelectByName : MicrogameSelector
    {
        [SerializeField] private string m_nextGame;

        public override string Select(GameState _state)
            => m_nextGame;
    }
}
