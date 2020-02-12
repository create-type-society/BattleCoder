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
                new Command("上に移動", "Move(Dir.Up,1)"),
                new Command("下に移動", "Move(Dir.Down,1)"),
                new Command("右に移動", "Move(Dir.Right,1)"),
                new Command("左に移動", "Move(Dir.Left,1)"),
                new Command("射撃", "Shot()"),
                new Command("上に方向転換", "MoveDir(Dir.Up)"),
                new Command("下に方向転換", "MoveDir(Dir.Down)"),
                new Command("右に方向転換", "MoveDir(Dir.Right)"),
                new Command("左に方向転換", "MoveDir(Dir.Left)"),
                new Command("近接攻撃", "Attack()"),
                new Command("キーが押された", "GetKeyDown(KeyCode.A)"),
                new Command("キーが離された", "GetKeyUp(KeyCode.A)"),
                new Command("キーが押され続けている", "GetKey(KeyCode.A)"),
                new Command("座標生成", "new Pos(0,0)"),
                new Command("射撃角度設定", "ShotDir(180)"),
                new Command("指定した場所を調べる", "GetTileType()"),
                new Command("戦車の場所", "TileType.Tank"),
                new Command("岩の場所", "TileType.Rock"),
                new Command("穴の場所", "TileType.Hole"),
                new Command("何もない場所", "TileType.Empty"),
                new Command("指定座標への角度", "GetPosRad()"),
                new Command("自分の座標取得", "GetMyPos()"),
                new Command("1秒待機", "Wait(1000)"),
                new Command("無限ループ", "while(true){}"),
                new Command("IF", "if(){}"),
                new Command("FOR", "for(){}"),
                new Command("関数", "function (){}"),
                new Command("移動プリセット",
                    @"while(1){
if(GetKey(KeyCode.UpArrow))
  Move(Dir.Up,1)
if(GetKey(KeyCode.LeftArrow))
  Move(Dir.Left,1)
if(GetKey(KeyCode.DownArrow))
  Move(Dir.Down,1)
if(GetKey(KeyCode.RightArrow))
  Move(Dir.Right,1)
}"
                ),
                new Command("ランダム", "Math.random()"),
                new Command("プリント", "Print()"),
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