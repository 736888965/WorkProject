using Lm;
using UnityEngine;
using Google.Protobuf;
using System.IO;
using Game;

public class TestPro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            helloworld h1 = new helloworld();
            h1.Id = 100;
            h1.Str = "kl";
            h1.Opt = 10000;
            byte[] databytes = h1.ToByteArray();
            File.WriteAllBytes(Application.dataPath + "/1.md", databytes);
        }
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            //byte[] byts = File.ReadAllBytes(Application.dataPath + "/1.md");
            //Serializer.Deserialize<helloworld>(byts);
            //Load();
            Fun_1();
        }
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            Fun_2();
        }
    }

    void Save()
    {
        helloworld h1 = new helloworld();
        h1.Id = 100;
        h1.Str = "kl";
        h1.Opt = 10000;
        using (Stream output = File.OpenWrite(Application.dataPath + "/1.txt"))
        {
            //CodedOutputStream st = new CodedOutputStream(output);
            h1.WriteTo(output);
        }
    }
    void Load()
    {

        using (Stream file = File.OpenRead(Application.dataPath + "/1.txt"))
        {
            helloworld hellow = helloworld.Parser.ParseFrom(file);
            print(hellow.Id + "    " + hellow.Str + "%%%%%%%%%%" + hellow.Opt);
        }
    }


    void  Fun_1()
    {
        Person person = new Person();
        person.Id = 10;
        ProtocolHelp.Serializer(person, "1.txt");
    }

    void Fun_2()
    {
        Person person = ProtocolHelp.Deserializer<Person>("1.txt");
        print(person.Name);
        print(person.Id);

    }


}
