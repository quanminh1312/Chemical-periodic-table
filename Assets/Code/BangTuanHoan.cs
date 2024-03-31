using Assets.Code.menu;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets
{
    public class BangTuanHoan : MonoBehaviour
    {
        public Vector3 startPos;
        public Vector3 startPos2;
        public float cellSize = 1.0f;
        public float offset;
        public List<GameObject> elements = new List<GameObject>();

        public Dictionary<string, Color> category = new Dictionary<string, Color>();
        public void ThemNguyenToVaoBangTuanHoan(int ro, int col, Vector3 o, Color color, string symbol, string name, string number)
        {
            GameObject element = Instantiate(TableSceneManager.Instance.element, new Vector3(o.x + col * cellSize + offset, o.y - ro * cellSize + offset, o.z), Quaternion.identity);
            element.transform.SetParent(transform);
            element.GetComponent<SpriteRenderer>().color = color;
            element.GetComponent<Clickable>().number = int.Parse(number);
            element.transform.GetChild(0).GetComponent<TextMeshPro>().text = symbol;
            element.transform.GetChild(1).GetComponent<TextMeshPro>().text = name;
            element.transform.GetChild(2).GetComponent<TextMeshPro>().text = number;
            elements.Add(element);
        }
    }
}
