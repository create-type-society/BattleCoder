using UnityEngine;

namespace BattleCoder.GamePlayUi
{
    public class ConsoleWindow : MonoBehaviour
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}