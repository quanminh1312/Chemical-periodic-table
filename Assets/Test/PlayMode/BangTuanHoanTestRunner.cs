using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Assembly_CSharp;
using Assets;
using Assets.Code.menu;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BangTuanHoanTestRunner
{
    private bool checkAllElement()
    {
        foreach (var gameObject in TableSceneManager.Instance.bangTuanHoan.elements)
        {
            if (!gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
    private bool checkAllElementFalse()
    {
        foreach (var gameObject in TableSceneManager.Instance.bangTuanHoan.elements)
        {
            if (gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
    [UnitySetUp]
    public IEnumerator BeforeEveryTest()
    {
        //clear old logs
        LogAssert.ignoreFailingMessages = true;
        new GameObject().AddComponent<Manager>();
        yield return null;
    }
    [UnityTearDown]
    public IEnumerator AfterEveryTest()
    {
        //destroy all objects in edit mode
        foreach (var gameObject in Object.FindObjectsOfType<GameObject>())
        {
            Object.Destroy(gameObject);
        }
        Object.Destroy(TableSceneManager.Instance);
        Object.Destroy(Manager.instance);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Helper.ClearAllData();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test101()
    {
        TableSceneManager.Instance = null;
        BangTuanHoan bangTuanHoan = new GameObject().AddComponent<BangTuanHoan>();
        MethodInfo methodInfo = typeof(BangTuanHoan).GetMethod("ThemNguyenToVaoBangTuanHoan", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(bangTuanHoan, new object[] { 1, 1, new Vector3(0,0,0), Color.black, "H", "Hydro", "1" }), "no TableSceneManager.instanece");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test102()
    {
        TableSceneManager.Instance.element = null;
        BangTuanHoan bangTuanHoan = new GameObject().AddComponent<BangTuanHoan>();
        MethodInfo methodInfo = typeof(BangTuanHoan).GetMethod("ThemNguyenToVaoBangTuanHoan", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(bangTuanHoan, new object[] { 1, 1, new Vector3(0, 0, 0), Color.black, "H", "Hydro", "1" }), "no TableSceneManager.instanece");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test103()
    {
        TableSceneManager.Instance.bangTuanHoan.offset = -1;
        BangTuanHoan bangTuanHoan = new GameObject().AddComponent<BangTuanHoan>();
        MethodInfo methodInfo = typeof(BangTuanHoan).GetMethod("ThemNguyenToVaoBangTuanHoan", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(bangTuanHoan, new object[] { 1, 1, new Vector3(0, 0, 0), Color.black, "H", "Hydro", "1" }), "no TableSceneManager.instanece");
        Assert.Fail("offset is nill");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test104()
    {
        BangTuanHoan bangTuanHoan = new GameObject().AddComponent<BangTuanHoan>();
        MethodInfo methodInfo = typeof(BangTuanHoan).GetMethod("ThemNguyenToVaoBangTuanHoan", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(bangTuanHoan, new object[] { 1, 1, new Vector3(0, 0, 0), Color.black, "H", "Hydro", "1" }), "no TableSceneManager.instanece");
        yield return null;
    }
}
