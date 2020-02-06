using System;
using UnityEngine;

namespace BattleCoder.GamePlayUi
{
    public class ProcessScrollViewPresenter : MonoBehaviour
    {
        [SerializeField] GameObject content;

        [SerializeField] ProcessPanelPresenter processPanelPrefab;


        public ProcessPanelPresenter AddProcessPanel(Action processClosedCallback)
        {
            var obj = Instantiate(processPanelPrefab, content.transform);
            obj.closed += () =>
            {
                processClosedCallback();
                Destroy(obj.gameObject);
            };
            return obj;
        }
    }
}