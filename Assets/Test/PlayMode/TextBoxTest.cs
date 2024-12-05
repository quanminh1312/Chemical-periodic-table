using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assembly_CSharp;
using Assets;
using Assets.Code;
using Assets.Code.menu;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TextBoxTest
{
    private TableSceneManager tableSceneManager;
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
        tableSceneManager = TableSceneManager.Instance;
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
    public IEnumerator Test112()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = VIA, phase = solid";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
                {
                    Assert.IsTrue(Helper.data[index]["group"].Contains("16"), "group not match");
                    Assert.IsTrue(Helper.data[index]["phase"].Contains("Solid"), "phase not match");
                }
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test113()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = VIA , phase = solid";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(Helper.data[index]["group"].Contains("16"), "group not match");
                Assert.IsTrue(Helper.data[index]["phase"].Contains("Solid"), "phase not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test114()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = VIA , , phase = solid";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(Helper.data[index]["group"].Contains("16"), "group not match");
                Assert.IsTrue(Helper.data[index]["phase"].Contains("Solid"), "periphaseod not match");
            }
        }
        yield return null;
    }
}