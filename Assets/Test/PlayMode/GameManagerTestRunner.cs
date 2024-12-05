using System.Collections;
using System.Collections.Generic;
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

public class GameManager
{
    private Manager gameManager;

    [UnitySetUp]
    public IEnumerator BeforeEveryTest()
    {
        //clear old logs
        LogAssert.ignoreFailingMessages = true;
        gameManager = new GameObject().AddComponent<Manager>();
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
        Object.Destroy(Manager.instance);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Helper.ClearAllData();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test22()
    {
        Assert.AreSame(gameManager, Manager.instance, "Instance not set");
        Assert.AreEqual(1, Object.FindObjectsOfType<Manager>().Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test23() 
    {
        Manager manager1 = new GameObject().AddComponent<Manager>();
        yield return null;
        Assert.IsNotNull(Manager.instance, "Instance not set");
        Assert.AreSame(gameManager, Manager.instance, "Instance not set");
        Assert.AreEqual(1, Object.FindObjectsOfType<Manager>().Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test24()
    {
        InfosSceneManager.instance.ROOT.SetActive(true);
        InfosSceneManager.instance.displaying = true;
        Debug.Log("Press escape to continue");
        while(!Input.GetKey(KeyCode.Escape))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test25()
    {
        InfosSceneManager.instance.ROOT.SetActive(false);
        InfosSceneManager.instance.displaying = false;
        Debug.Log("Press escape to continue");
        while (!Input.GetKey(KeyCode.Escape))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }

    [UnityTest]
    public IEnumerator Test26()
    {
        InfosSceneManager.instance.ROOT.SetActive(false);
        InfosSceneManager.instance.displaying = false;
        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test27()
    {
        InfosSceneManager.instance.ROOT.SetActive(true);
        InfosSceneManager.instance.displaying = true;
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test28()
    {
        InfosSceneManager.instance.ROOT.SetActive(true);
        InfosSceneManager.instance.displaying = true;
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test29()
    {
        InfosSceneManager.instance.ROOT.SetActive(true);
        InfosSceneManager.instance.displaying = true;
        Debug.Log("Press left mouse button to Hidro to continue");
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(InfosSceneManager.instance.element3Ds[0].activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test30()
    {
        InfosSceneManager.instance.ROOT.SetActive(false);
        InfosSceneManager.instance.displaying = false;
        Debug.Log("Press left mouse button to Hidro to continue");
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test31()
    {
        InfosSceneManager.instance.ROOT.SetActive(false);
        InfosSceneManager.instance.displaying = true;
        Debug.Log("Press left mouse button to Hidro to continue");
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && !InfosSceneManager.instance.displaying)
        {
            if (hit.collider.gameObject.GetComponent<Clickable>() != null)
            {
                //remove the clickable component
                Object.Destroy(hit.collider.gameObject.GetComponent<Clickable>());
            }
        }
        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test32()
    {
        InfosSceneManager.instance.ROOT.SetActive(true);
        InfosSceneManager.instance.displaying = true;
        Debug.Log("left mouse button to somewhere to continue");
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
    [UnityTest]
    public IEnumerator Test33()
    {
        InfosSceneManager.instance.ROOT.SetActive(false);
        InfosSceneManager.instance.displaying = false;
        Debug.Log("left mouse button to somewhere to continue");
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(InfosSceneManager.instance.ROOT.activeSelf, "Root is not active");
    }
}
