using UnityEngine;

namespace BattleCoder.Result
{
    public class ReturnTitle: MonoBehaviour
        {
            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneChangeManager.ChangeTitleScene();
                }
            }
        }
    }
