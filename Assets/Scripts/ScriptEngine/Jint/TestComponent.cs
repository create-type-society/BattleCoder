using System;
using Jint;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    public interface IParson
    {
        string Name { get; }
        int Age { get; }
    }

    public class Parson : IParson
    {
        public Parson(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        IParson data = new Parson("", 0);

        try
        {
            // Parsonクラスを自動でいい感じにjs環境に変数として取り込んでくれる
            var engine = new Engine().SetValue("gameObjectName", data);
            // Jsのコードを実行
            engine.Execute("gameObjectName.Name = 'Name: ScriptEngine, Age: '")
                .Execute("gameObjectName.Age = 25");

            // Parsonクラスに自動でバインドしてくれる。
            gameObject.name = data.Name;
            gameObject.name += data.Age.ToString();
        }
        catch (Exception e)
        {
            // エラー表示
            print(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}