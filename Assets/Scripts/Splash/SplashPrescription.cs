using System;
using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Prescription Splash", menuName = "Splash/Prescription Splash")]
    public class SplashPrescription : SplashDetails
    {
        [Serializable]
        private class SheetLine
        {
            [SerializeField] private string m_line;
            [SerializeField] private int m_minPillsRemaining = 1;

            public string Line => m_line;
            public int MinPillsRemaining => m_minPillsRemaining;
        }

        [SerializeField] private string[] m_daysOfWeek;
        
        [SerializeField] private string m_multipleSheetsLine = "{0}";
        [SerializeField] private SheetLine[] m_sheetLines;
        
        [SerializeField] private string m_prescriptionLine = "{0}";

        public override string GetTitle(GameState _state)
            => string.Format(base.GetTitle(_state), m_daysOfWeek[_state.CurrentDay]);

        public override string GetSubtitle(GameState _state)
        {
            var sheetLine = m_multipleSheetsLine;
            if (m_sheetLines.Length > 0 && _state.SheetsRemaining < 1)
            {
                var closestIndex = -1;
                var comparison = -1;
                
                var pillsRemaining = _state.PillsRemaining;
                for (var i = 0; i < m_sheetLines.Length; i++)
                {
                    if(m_sheetLines[i].MinPillsRemaining > pillsRemaining) { continue; }
                    
                    if(m_sheetLines[i].MinPillsRemaining < comparison) { continue; }
                    
                    closestIndex = i;
                    comparison = m_sheetLines[i].MinPillsRemaining;
                }

                if(closestIndex > -1) { sheetLine = m_sheetLines[closestIndex].Line; }
            }
            sheetLine = string.Format(sheetLine, _state.SheetsRemaining + 1);
            
            var prescriptionLine = string.Format(m_prescriptionLine, _state.PrescriptionAmount * 2);

            return string.Format(base.GetSubtitle(_state), sheetLine, prescriptionLine, m_daysOfWeek[_state.CurrentDay]);
        }
    }
}
