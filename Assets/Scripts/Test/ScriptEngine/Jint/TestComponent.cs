using System;
using Jint;
using Jint.Runtime.Interop;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace BattleCoder.Test.ScriptEngine.Jint
{
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

            public static Parson Create(string name, int age)
            {
                return new Parson(name, age);
            }

            public string Name { get; private set; }
            public int Age { get; private set; }
        }

        public enum Hoge
        {
            Fuga = 88,
            Poge = 23,
            Go = 55
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
                engine.Execute("gameObjectName.Name = 'ScriptEngine'")
                    .Execute("gameObjectName.Age = 25");

                // Parsonクラスに自動でバインドしてくれる。
                Debug.Assert(data.Name == "ScriptEngine");
                Debug.Assert(data.Age == 25);

                // エンジンにSystemを登録
                engine = new Engine(cfg => cfg.AllowClr());

                // 関数を変数に格納
                engine.Execute("var concat = System.String.Concat;");
                // 実行
                var val = engine.GetValue("concat").Invoke("test", "abc");
                Debug.Assert(val.AsString() == "testabc");

                engine = new Engine();
                // 型を登録
                engine.SetValue("Parson", TypeReference.CreateTypeReference(engine, typeof(Parson)));
                // new でインスタンス作成
                engine.Execute("var p1 = new Parson('Test', 10)");
                // 変数を取得
                var val2 = engine.GetValue("p1").AsObject();
                Debug.Assert(val2.Get("Name").AsString() == "Test");
                Debug.Assert((int) val2.Get("Age").AsNumber() == 10);

                // static関数にアクセス
                engine.Execute("var p2 = Parson.Create('Bob', 30)");
                // 変数を取得
                var val3 = engine.GetValue("p2").AsObject();
                Debug.Assert(val3.Get("Name").AsString() == "Bob");
                Debug.Assert((int) val3.Get("Age").AsNumber() == 30);

                // 型を登録
                engine.SetValue("Hoge", TypeReference.CreateTypeReference(engine, typeof(Hoge)));
                // new でインスタンス作成
                engine.Execute("var go = Hoge.Go");
                // 変数を取得
                var val4 = engine.GetValue("go").AsNumber();
                Debug.Assert((Hoge) val4 == Hoge.Go);

                try
                {
                    engine = new Engine(new Action<Options>(options => options.TimeoutInterval(new TimeSpan(0, 0, 5))));
                    engine.Execute("while(true);");

                    Debug.LogError("Test Error!");
                }
                catch (TimeoutException e)
                {
                    Debug.Log("Test Clear!");
                }

#if UNITY_EDITOR
                // テストを終了
                EditorApplication.isPlaying = false;
                EditorUtility.DisplayDialog("", "テストが完了しました。", "OK");
#endif
            }
            catch (Exception e)
            {
                // エラー表示
                Debug.LogError(e);
            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}