using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Door Selector", menuName = "Microgame Selectors/Door Selector")]
    public class DoorSelector : MicrogameSelector
    {
        [SerializeField] private string m_workGame;
        [SerializeField] private string m_prescriptionGame;

        public override string Select(GameState _state)
            => _state.NeedsRefill ? m_prescriptionGame : m_workGame;
    }
}
