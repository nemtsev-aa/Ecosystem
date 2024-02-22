using UnityEngine;
using System.IO;

public class Logger : MonoBehaviour {
    public static Logger Instance;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Log(string message) {
        Debug.Log(message);

        StreamWriter writer = new StreamWriter(GetLogsPath(), true);
        writer.WriteLine(message);
        writer.Close();
    }

    string GetLogsPath() {
        return Application.persistentDataPath + "/logs.txt";
    }
}

