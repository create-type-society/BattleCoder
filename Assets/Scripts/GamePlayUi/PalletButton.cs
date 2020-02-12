using UnityEngine;

namespace BattleCoder.GamePlayUi
{
    public class PalletButton : MonoBehaviour
    {
        [SerializeField] GameObject pallet;

        public void PalletShowOrHide()
        {
            pallet.SetActive(!pallet.activeSelf);
        }
    }
}