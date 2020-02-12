using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class CommandButtonCreator : MonoBehaviour
    {
        struct Command
        {
            public readonly string nameText;
            public readonly string scriptText;

            public Command(string nameText, string scriptText)
            {
                this.nameText = nameText;
                this.scriptText = scriptText;
            }
        }

        [SerializeField] GameObject buttonBase;

        // Start is called before the first frame update
        void Start()
        {
            int offsetX = 0;
            int offsetY = 20;
            int cnt = 0;
            foreach (var command in new Command[]
            {
                new Command("上に移動", "unko"),
                new Command("下に移動", "unko"),
                new Command("右に移動", "unko"),
                new Command("左に移動", "unko"),
                new Command("射撃", "unko"),
                new Command("上に方向転換", "unko"),
                new Command("下に方向転換", "unko"),
                new Command("右に方向転換", "unko"),
                new Command("左に方向転換", "unko"),
                new Command("近接攻撃", "unko"),
                new Command("キーが押された", "unko"),
                new Command("キーが離された", "unko"),
                new Command("キーが押され続けている", "unko"),
                new Command("座標生成", "unko"),
                new Command("射撃角度設定", "unko"),
                new Command("指定した場所を調べる", "unko"),
                new Command("敵戦車の場所", "unko"),
                new Command("岩の場所", "unko"),
                new Command("穴の場所", "unko"),
                new Command("何もない場所", "unko"),
                new Command("指定座標までの角度", "unko"),
                new Command("自分の座標取得", "unko"),
                new Command("待機", "unko"),
                new Command("無限ループ", "unko"),
                new Command("IF", "unko"),
                new Command("FOR", "unko"),
                new Command("関数", "unko"),
                new Command("移動プリセット", "unko"),
                new Command("ランダム", "unko"),
                new Command("プリント", "unko"),
            })
            {
                if (cnt % 5 == 0)
                {
                    offsetX = 0;
                    offsetY += -20;
                }
                var button = Instantiate(buttonBase, transform);
                button.transform.position = button.transform.position + new Vector3(offsetX, offsetY, 0);
                button.GetComponent<CommandButton>().text = command.scriptText;
                button.GetComponentInChildren<Text>().text = command.nameText;
                offsetX += 80;
                cnt++;
            }
        }

    }
}