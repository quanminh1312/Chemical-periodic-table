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

public class TableSceneManagerTest
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
    public IEnumerator Test34()
    {
        Assert.AreSame(tableSceneManager, TableSceneManager.Instance, "Instance not set");
        Assert.AreEqual(1, Object.FindObjectsOfType<TableSceneManager>().Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test35()
    {
        TableSceneManager manager1 = new GameObject().AddComponent<TableSceneManager>();
        yield return null;
        Assert.IsNotNull(TableSceneManager.Instance, "Instance not set");
        Assert.AreSame(tableSceneManager, TableSceneManager.Instance, "Instance not set");
        Assert.AreEqual(1, Object.FindObjectsOfType<TableSceneManager>().Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test36()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = null;
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        Assert.IsTrue(checkAllElement(),"nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test37()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = a";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        Assert.IsFalse(checkAllElement(),"have Input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test38()
    {
        tableSceneManager.inputField = null;
        var toggle = tableSceneManager.toggle;
        MethodInfo toggleMethod = typeof(TableSceneManager).GetMethod("OnToggle", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(toggleMethod, "Start method not found");
        Assert.DoesNotThrow(() => toggleMethod.Invoke(tableSceneManager, new object[] { null }), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test39()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = a";
        MethodInfo toggleMethod = typeof(TableSceneManager).GetMethod("OnToggle", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(toggleMethod, "toggle method not found");
        Assert.DoesNotThrow(() => toggleMethod.Invoke(tableSceneManager,null), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator test40()
    {
        //var inputfield = tableSceneManager.inputField;
        //inputfield.text = "group = a";
        MethodInfo onSearchMethod = typeof(TableSceneManager).GetMethod("OnSearch", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(onSearchMethod, "search method not found");
        Assert.DoesNotThrow(() => onSearchMethod.Invoke(tableSceneManager, null), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test41()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "ầiu312";
        yield return null;
        //MethodInfo onSearchMethod = typeof(TableSceneManager).GetMethod("OnSearch", BindingFlags.NonPublic | BindingFlags.Instance);
        //Assert.IsNotNull(onSearchMethod, "search method not found");
        //Assert.DoesNotThrow(() => onSearchMethod.Invoke(tableSceneManager, new object[] { "ầiu312" }), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test42()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "name=Hidro";
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test43()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "electron_Configuration=1s2,name=H";
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test44()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "name =";
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test45()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "= Hidro";
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test46()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "name = dadjials!!@$";
        yield return null;
        Assert.IsTrue(checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test47()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "đâljálda = Hidro";
        yield return null;
        Assert.IsTrue(checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test48()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "appearance = , period = 2";
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(Helper.data[index]["period"].Equals("2"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test49()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "appearance = silvery white";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(Helper.data[index]["appearance"].Equals("silvery white"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test50()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "appearance = silvery white";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(Helper.data[index]["appearance"].Contains("silvery white"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test51()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "apperance = silvery white , phase = solid";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(Helper.data[index]["appearance"].Equals("silvery white"), "period not match");
                Assert.IsTrue(Helper.data[index]["phase"].Equals("solid"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test52()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "apperance = silvery white , phase = solid";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(Helper.data[index]["appearance"].Contains("silvery white"), "period not match");
                Assert.IsTrue(Helper.data[index]["phase"].Contains("solid"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test53()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "appear = silvery white";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test54()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group : VIA";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("VIA"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test55()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group & VIA";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test56()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group VIA";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test57()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = VIA";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("VIA"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test58()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = VI";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("VI"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test59()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = A";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("A"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test60()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”6A";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("VIA"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test61()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”6";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("VI"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test62()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”a";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("A"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test63()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”VIA";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Equals("VIA"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test64()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”VI";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test65()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”A";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test66()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”6A";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Equals("VIA"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test67()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”6";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                //var group = Helper.elementsGroup.Where(x => x.Contains("VIA")).FirstOrDefault();
                var indexInGroup = int.Parse(Helper.data[index]["group"]) - 1;
                Assert.IsTrue(Helper.elementsGroup[indexInGroup].Contains("VI"), "period not match");
                Assert.IsFalse(Helper.elementsGroup[indexInGroup].Contains("VII"), "period not match");
                Assert.IsFalse(Helper.elementsGroup[indexInGroup].Contains("VIII"), "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test68()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "group = ”a";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test69()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test70()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test71()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p -1";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test72()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = a p -1 +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test73()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p a +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test74()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 u -1 +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test75()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p -1 +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(index >= 4, "period not match");
            }
            else
            {
                Assert.IsTrue(index < 4, "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test76()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p +1 +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(index >= 6, "period not match");
            }
            else
            {
                Assert.IsTrue(index < 6, "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test77()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p +1 1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test78()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p +1 +";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test79()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 2 +1 +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test80()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p +1 -1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(index >= 9, "period not match");
            }
            else
            {
                Assert.IsTrue(index < 9, "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test81()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 2 p +1 -1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(!checkAllElement(), "nothing happen");
        for (int index = 0; index < tableSceneManager.bangTuanHoan.elements.Count; index++)
        {
            if (tableSceneManager.bangTuanHoan.elements[index].activeSelf)
            {
                Assert.IsTrue(index == 9, "period not match");
            }
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test82()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 3 d -2 +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = true;
        yield return null;
        Assert.IsTrue(checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test83()
    {
        var inputfield = tableSceneManager.inputField;
        inputfield.text = "n_l_ml_ms = 3 d +1 +1/2";
        var toggle = tableSceneManager.toggle;
        toggle.isOn = false;
        yield return null;
        Assert.IsTrue(checkAllElementFalse(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test84()
    {
        TableSceneManager.Instance.bangTuanHoan = null;
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("OnEnableTable", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test85()
    {
        TableSceneManager.Instance.bangTuanHoan.elements = null;
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("OnEnableTable", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test86()
    {
        TableSceneManager.Instance.ResetData();
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("OnEnableTable", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test87()
    {
        TableSceneManager.Instance.DeleteNumberKey();
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("OnEnableTable", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test88()
    {
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("OnEnableTable", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test89()
    {
        foreach (var item in TableSceneManager.Instance.bangTuanHoan.elements)
        {
            Object.Destroy(item);
        }
        Helper.data = null;
        TableSceneManager.Instance.bangTuanHoan.elements = new List<GameObject>();
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
        Helper.data = new List<Dictionary<string, string>>();
    }
    [UnityTest]
    public IEnumerator Test90()
    {
        foreach (var item in TableSceneManager.Instance.bangTuanHoan.elements)
        {
            Object.Destroy(item);
        }
        Helper.data = new List<Dictionary<string, string>>();
        TableSceneManager.Instance.bangTuanHoan.elements = new List<GameObject>();
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.IsTrue(TableSceneManager.Instance.bangTuanHoan.elements.Count == 0, "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test91()
    {
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test92()
    {
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test93()
    {
        TableSceneManager.Instance.ResetData();
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test94()
    {
        TableSceneManager.Instance.ResetData();
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.IsTrue(checkAllElement(), "nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test95()
    {
        TableSceneManager.Instance.bangTuanHoan.startPos = new Vector3(0, 0, 0);
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.Fail("nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test96()
    {
        TableSceneManager.Instance.bangTuanHoan.startPos2 = new Vector3(0, 0, 0);
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        Assert.Fail("nothing happen");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test97()
    {
        Helper.elementsGroup = null;
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTag", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test98()
    {
        Helper.elementsGroup = new List<string>() { "IA", "IIA", "IIIB", "IVB", "VB", "VIB", "VIIB", "VIIIB", "VIIIB", "VIIIB", "IB", "IIB", "IIIA", "IVA", "VA", "VIA", "VIIA", "VIIIA", };
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateTag", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test99()
    {
        TableSceneManager.Instance.categoryHeader = null;
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateCategoryHeader", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test100()
    {
        MethodInfo methodInfo = typeof(TableSceneManager).GetMethod("CreateCategoryHeader", BindingFlags.Public | BindingFlags.Instance);
        Assert.DoesNotThrow(() => methodInfo.Invoke(TableSceneManager.Instance, null), "null input");
        yield return null;
    }
}
