using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
public class HelperTest
{
    [MenuItem("Tools/Rename Scene")]
    public static void RenameScene(string oldPath, string newPath)
    {
        string success = AssetDatabase.MoveAsset(oldPath, newPath);
        if (success == null)
        {
            Debug.Log($"Scene renamed successfully from {oldPath} to {newPath}");
        }
        else
        {
            Debug.LogError("Failed to rename the Scene.");
        }

        AssetDatabase.Refresh();

        UpdateBuildSettingsScenes(newPath, oldPath);
    }

    private static void UpdateBuildSettingsScenes(string newPath, string oldPath)
    {
        // Lấy danh sách Scene trong Build Settings
        var scenes = EditorBuildSettings.scenes;

        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].path == oldPath)
            {
                scenes[i].path = newPath;
                Debug.Log($"Updated Scene path in Build Settings: {newPath}");
            }
        }

        // Cập nhật lại Build Settings
        EditorBuildSettings.scenes = scenes;
    }
    [UnityTest]
    public IEnumerator Test105()
    {
        //rename PeriodicTableCSV in Resources.Load("PeriodicTableCSV") as TextAsset in Helper.cs to PeriodicTableCSV1
        string oldPath = Application.dataPath + "/Resources/PeriodicTableCSV.csv";
        string newPath = Application.dataPath + "/Resources/PeriodicTableCSV1.csv";
        if (File.Exists(oldPath)) File.Move(oldPath, newPath);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
        try
        {
            Helper.ReadCSVFile();
        }
        catch
        {
            Assert.Fail("Failed to read CSV file");
        }
        finally
        {
            if (File.Exists(newPath)) File.Move(newPath, oldPath);
        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test106()
    {
        string oldPath = Application.dataPath + "/Resources/PeriodicTableCSV.csv";
        string newPath = Application.dataPath + "/Resources/PeriodicTableCSV1.csv";
        string oldPath1 = Application.dataPath + "/Resources/PeriodicTableCSV_1.csv";
        string newPath1 = Application.dataPath + "/Resources/PeriodicTableCSV.csv";
        if (File.Exists(oldPath)) File.Move(oldPath, newPath);
        if (File.Exists(oldPath1)) File.Move(oldPath1, newPath1);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
        try
        {
            Helper.ReadCSVFile();
        }
        catch
        {
            Assert.Fail("Failed to read CSV file");
        }
        finally
        {
            if (File.Exists(newPath)) File.Move(newPath, oldPath);
        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test107()
    {
        string oldPath = Application.dataPath + "/Resources/PeriodicTableCSV.csv";
        string newPath = Application.dataPath + "/Resources/PeriodicTableCSV1.csv";
        string oldPath2 = Application.dataPath + "/Resources/PeriodicTableCSV_2.csv";
        string newPath2 = Application.dataPath + "/Resources/PeriodicTableCSV.csv";
        if (File.Exists(oldPath)) File.Move(oldPath, newPath);
        if (File.Exists(oldPath2)) File.Move(oldPath2, newPath2);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
        try
        {
            Helper.ReadCSVFile();
        }
        catch
        {
            Assert.Fail("Failed to read CSV file");
        }
        finally
        {
            if (File.Exists(newPath)) File.Move(newPath, oldPath);
        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test108()
    {
        Assert.DoesNotThrow(() =>
        {
            Helper.ReadCSVFile();
        });
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test109()
    {
        string oldPath = "Assets/Scenes/InfoScene.unity";
        string newPath = "Assets/Scenes/InfoScene1.unity";
        RenameScene(oldPath, newPath);
        Helper.ReadCSVFile();
        try
        {
            Helper.TurnOnSceneAddictiveMode();
        }
        catch
        {
            Assert.Fail("Failed to read CSV file");
        }
        finally
        {
            RenameScene(newPath, oldPath);
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test110()
    {
        string oldPath = "Assets/Scenes/TableScene.unity";
        string newPath = "Assets/Scenes/TableScene1.unity";
        RenameScene(oldPath, newPath);
        Helper.ReadCSVFile();
        try
        {
            Helper.TurnOnSceneAddictiveMode();
        }
        catch
        {
            Assert.Fail("Failed to read CSV file");
        }
        finally
        {
            RenameScene(newPath, oldPath);
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test111()
    {
        Helper.ReadCSVFile();
        Assert.DoesNotThrow(() =>
        {
            Helper.TurnOnSceneAddictiveMode();
        });
        yield return null;
    }
}
#endif
