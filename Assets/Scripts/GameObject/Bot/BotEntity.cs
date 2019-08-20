using UnityEngine;

public class BotEntity : MonoBehaviour
{
    //横に移動する(岩があったら停止してfalse返す)
    public bool MoveX(float x)
    {
        return Move(x, 0);
    }

    //縦に移動する(岩があったら停止してfalse返す)
    public bool MoveY(float y)
    {
        return Move(0, y);
    }

    bool Move(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
        return true;
    }
}