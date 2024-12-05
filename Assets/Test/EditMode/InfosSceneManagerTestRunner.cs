using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Assets;
using Assets.Code;
using Assets.Code.menu;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class Dad
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        Helper.ReadCSVFile();
    }
    [TearDown]
    public void AfterEveryTest()
    {
        //destroy all objects in edit mode
        foreach (var gameObject in Object.FindObjectsOfType<GameObject>())
        {
            Object.DestroyImmediate(gameObject);
        }
        Object.DestroyImmediate(InfosSceneManager.instance);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
    [OneTimeTearDown]
    public void GlobalCleanup()
    {
        Helper.ClearAllData();
    }
}
public class InfoSceneManager : Dad
{
    private InfosSceneManager infosSceneManager;
    private MethodInfo toggleInfoSceneManagerMethod;
    private MethodInfo displayTextMethod;
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        toggleInfoSceneManagerMethod = typeof(InfosSceneManager).GetMethod("ToggleInfoSceneManager", BindingFlags.Public | BindingFlags.Instance);
        Assert.IsNotNull(toggleInfoSceneManagerMethod, "ToggleInfoSceneManager method not found");
        displayTextMethod = typeof(InfosSceneManager).GetMethod("DisplayText", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(displayTextMethod, "DisplayText method not found");
    }
    [SetUp]
    public void BeforeEveryTest()
    {
        //clear old logs
        LogAssert.ignoreFailingMessages = true;
        infosSceneManager = new GameObject().AddComponent<InfosSceneManager>();
        infosSceneManager.element3Ds = new List<GameObject>();
        infosSceneManager.texts = new List<TMPro.TextMeshProUGUI>();
        for (int i = 0; i < 119; i++)
        {
            GameObject gameObject = new GameObject();
            infosSceneManager.element3Ds.Add(gameObject);
            infosSceneManager.texts.Add(gameObject.AddComponent<TMPro.TextMeshProUGUI>());
        }
        infosSceneManager.categoryImage = new GameObject();
        infosSceneManager.categoryImage.AddComponent<Image>();
        TableSceneManager tableSceneManager = new GameObject().AddComponent<TableSceneManager>();
        tableSceneManager.element = new GameObject();
        tableSceneManager.element.AddComponent<SpriteRenderer>();
        tableSceneManager.element.AddComponent<Clickable>();
        //add 3 textmeshpro to element
        for (int i = 0; i < 3; i++)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<TMPro.TextMeshPro>();
            gameObject.transform.SetParent(tableSceneManager.element.transform);
        }
        tableSceneManager.canvas = new GameObject().AddComponent<Canvas>();
        tableSceneManager.tagPrefabs = new GameObject();
        tableSceneManager.tagPrefabs.AddComponent<RectTransform>();
        tableSceneManager.tagPrefabs.AddComponent<TextMeshProUGUI>();
        tableSceneManager.bangTuanHoan = new GameObject().AddComponent<BangTuanHoan>();
        tableSceneManager.bangTuanHoan.startPos = new Vector3(0, 0, 0);
        tableSceneManager.bangTuanHoan.startPos2 = new Vector3(0, 0, 0);
        tableSceneManager.bangTuanHoan.offset = 1.0f;
        tableSceneManager.categoryHeader = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<RawImage>();
            GameObject text = new GameObject();
            text.AddComponent<TextMeshProUGUI>();
            text.transform.SetParent(gameObject.transform);
            tableSceneManager.categoryHeader.Add(gameObject);
        }
        tableSceneManager.inputField = new GameObject().AddComponent<TMP_InputField>();
        tableSceneManager.toggle = new GameObject().AddComponent<Toggle>();
        tableSceneManager.keys = new GameObject().AddComponent<TextMeshProUGUI>();
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo.Invoke(tableSceneManager, null);

        MethodInfo startMethod = typeof(InfosSceneManager).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(startMethod, "Start method not found");
        startMethod.Invoke(infosSceneManager, null);
    }
    [Test]
    public void Test1()
    {
        Object.DestroyImmediate(InfosSceneManager.instance);
        InfosSceneManager infosSceneManager = new GameObject().AddComponent<InfosSceneManager>();
        MethodInfo startMethod = typeof(InfosSceneManager).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(startMethod, "Start method not found");
        startMethod.Invoke(infosSceneManager, null);
        Assert.AreSame(infosSceneManager, InfosSceneManager.instance, "Instance not set");
        Assert.AreEqual(1, Object.FindObjectsOfType<InfosSceneManager>().Length);
    }
    [Test]
    public void Test2()
    {
        Object.DestroyImmediate(InfosSceneManager.instance);
        InfosSceneManager infosSceneManager = new GameObject().AddComponent<InfosSceneManager>();
        MethodInfo startMethod = typeof(InfosSceneManager).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(startMethod, "Start method not found");
        startMethod.Invoke(infosSceneManager, null);
        InfosSceneManager infosSceneManager1 = new GameObject().AddComponent<InfosSceneManager>();
        startMethod.Invoke(infosSceneManager1, null);
        Assert.IsNotNull(InfosSceneManager.instance, "Instance not set");
        Assert.AreSame(infosSceneManager, InfosSceneManager.instance, "Instance not set");
        Assert.AreEqual(1, Object.FindObjectsOfType<InfosSceneManager>().Length);
    }
    [Test]
    public void Test3()
    {
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 0 });
        LogAssert.Expect(LogType.Error, "No root object set for DebugManager!");
    }
    [Test]
    public void Test4()
    {
        infosSceneManager.ROOT = new GameObject();
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 0 });
        LogAssert.NoUnexpectedReceived();
    }

    [Test]
    public void Test5()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 0 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test6()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(true);
        infosSceneManager.displaying = true;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 0 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test7()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { -1 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test8()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(true);
        infosSceneManager.displaying = true;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { -1 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test9()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 1 });
        //assert if root is unactive
        Assert.IsTrue(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test10()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(true);
        infosSceneManager.displaying = true;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 1 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test11()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 119 });
        //assert if root is unactive
        Assert.IsTrue(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test12()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(true);
        infosSceneManager.displaying = true;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 119 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test13()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 120 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test14()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(true);
        infosSceneManager.displaying = true;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, new object[] { 120 });
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test15()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, null);
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test16()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(true);
        infosSceneManager.displaying = true;
        toggleInfoSceneManagerMethod.Invoke(infosSceneManager, null);
        //assert if root is unactive
        Assert.IsFalse(infosSceneManager.ROOT.activeSelf, "Root is not active");
    }
    [Test]
    public void Test17()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        Assert.Throws<TargetInvocationException>(() => displayTextMethod.Invoke(infosSceneManager, new object[] { 0 }), "No root object set for DebugManager!");
    }
    [Test]
    public void Test18()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        Assert.Throws<TargetInvocationException>(() => displayTextMethod.Invoke(infosSceneManager, new object[] { -1 }), "No root object set for DebugManager!");
    }
    [Test]
    public void Test19()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        Assert.DoesNotThrow(() => displayTextMethod.Invoke(infosSceneManager, new object[] { 1 }), "No root object set for DebugManager!");
    }
    [Test]
    public void Test20()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        Assert.DoesNotThrow(() => displayTextMethod.Invoke(infosSceneManager, new object[] { 119 }), "No root object set for DebugManager!");
    }
    [Test]
    public void Test21()
    {
        infosSceneManager.ROOT = new GameObject();
        infosSceneManager.ROOT.SetActive(false);
        infosSceneManager.displaying = false;
        Assert.Throws<TargetInvocationException>(() => displayTextMethod.Invoke(infosSceneManager, new object[] { 120 }), "No root object set for DebugManager!");
    }
}
