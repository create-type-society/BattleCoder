public interface IBotCommands
{
    //移動コマンド
    void Move(Direction direction, float speed, uint gridDistance);
}