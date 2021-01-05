using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO
{
    private static string enemyCSVPath = "/Project/Scripts/Editor/CSV/Enemy.csv";
    private static string powerCSVPath = "/Project/Scripts/Editor/CSV/Power.csv";

    [MenuItem("Utilities/Generate Enemies")]
    public static void GenerateEnemies()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + enemyCSVPath);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            if (splitData.Length != 7)
            {
                Debug.LogError(s + " does not have correct number of columns");
                continue;
            }

            EnemyStats enemy = ScriptableObject.CreateInstance<EnemyStats>();
            enemy.enemyName = splitData[0];
            enemy.health = float.Parse(splitData[1]);
            enemy.damage = float.Parse(splitData[2]);
            enemy.speed = float.Parse(splitData[3]);
            enemy.score = float.Parse(splitData[4]);
            enemy.waypointDistance = float.Parse(splitData[5]);
            enemy.isLongRanged = bool.Parse(splitData[6]);

            AssetDatabase.CreateAsset(enemy, $"Assets/Project/Scripts/Scriptable Objects/Enemy/{enemy.enemyName}.asset");
        }

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Utilities/Generate Power")]
    public static void GeneratePowers()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + powerCSVPath);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            if (splitData.Length != 4)
            {
                Debug.LogError(s + " does not have correct number of columns");
                continue;
            }

            Power power = ScriptableObject.CreateInstance<Power>();
            power.powerName = splitData[0];
            power.projectileSpeed = float.Parse(splitData[1]);
            power.damage = float.Parse(splitData[2]);
            power.damageRadius = float.Parse(splitData[3]);

            AssetDatabase.CreateAsset(power, $"Assets/Project/Scripts/Scriptable Objects/Powers/{power.powerName}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
