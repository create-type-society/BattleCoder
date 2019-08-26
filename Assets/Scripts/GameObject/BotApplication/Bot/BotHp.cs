//BotのHpを表す

public struct BotHp
{
    public readonly int hp;

    public BotHp(int hp)
    {
        this.hp = hp;
    }

    //Hpをダメージ分だけ減らしたBotHpを新たに生成する
    public BotHp DamageHp(int damage)
    {
        return new BotHp(hp - damage);
    }

    public bool IsDeath()
    {
        return hp <= 0;
    }
}