using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.menu
{
    internal class TableSceneManager :MonoBehaviour
    {
        public static TableSceneManager Instance;

        private List<Color> colors = new List<Color>()
        {
            new Color(0.9f, 0.6f, 0.2f), // Màu vàng nhạt
            new Color(0.6f, 0.8f, 0.2f), // Màu xanh lá cây nhạt
            new Color(0.2f, 0.6f, 0.9f), // Màu xanh dương nhạt
            new Color(0.9f, 0.4f, 0.2f), // Màu cam nhạt
            new Color(0.6f, 0.2f, 0.8f), // Màu tím nhạt
            new Color(0.2f, 0.8f, 0.6f), // Màu xanh lục nhạt
            new Color(0.7f, 0.4f, 0.1f), // Màu vàng đất
            new Color(0.4f, 0.6f, 0.1f), // Màu xanh lá cây tối
            new Color(0.1f, 0.4f, 0.7f), // Màu xanh dương tối
            new Color(0.7f, 0.2f, 0.1f), // Màu cam tối
            new Color(0.4f, 0.1f, 0.6f), // Màu tím tối
            new Color(0.1f, 0.6f, 0.4f), // Màu xanh lục tối
            new Color(1.0f, 0.8f, 0.4f), // Màu vàng chanh
            new Color(0.8f, 1.0f, 0.4f), // Màu xanh lá cây sáng
            new Color(0.4f, 0.8f, 1.0f)  // Màu xanh dương sáng
        };
        List<Color> contrastColors = new List<Color>
        {
            new Color(0.1f, 0.4f, 0.8f), // Contrasting with pale yellow
            new Color(0.4f, 0.2f, 0.8f), // Contrasting with pale green
            new Color(0.8f, 0.4f, 0.1f), // Contrasting with pale blue
            new Color(0.1f, 0.6f, 0.8f), // Contrasting with pale orange
            new Color(0.4f, 0.8f, 0.2f), // Contrasting with pale purple
            new Color(0.8f, 0.2f, 0.4f), // Contrasting with pale teal
            new Color(0.3f, 0.6f, 0.9f), // Contrasting with earth yellow
            new Color(0.6f, 0.4f, 0.9f), // Contrasting with dark green
            new Color(0.9f, 0.6f, 0.3f), // Contrasting with dark blue
            new Color(0.3f, 0.8f, 0.9f), // Contrasting with dark orange
            new Color(0.6f, 0.9f, 0.4f), // Contrasting with dark purple
            new Color(0.9f, 0.4f, 0.6f), // Contrasting with dark teal
            new Color(0.0f, 0.2f, 0.6f), // Contrasting with lemon yellow
            new Color(0.2f, 0.0f, 0.6f), // Contrasting with bright green
            new Color(0.6f, 0.2f, 0.0f)  // Contrasting with bright blue
        };
        public List<GameObject> categoryHeader;
        public GameObject element;
        public GameObject tagPrefabs;
        public BangTuanHoan bangTuanHoan;
        public TMP_InputField inputField;
        public TextMeshProUGUI keys;
        public Toggle toggle;
        public Canvas canvas;
        public Vector3 startPos;
        public Vector3 startPos2;
        public Vector3 startPos3;
        public float size;
        public float offset;
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        private void Start()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            CreateTable();
            CreateTag();
            CreateCategoryHeader();
            inputField.onValueChanged.AddListener(OnSearch);
            toggle.onValueChanged.AddListener(OnToggle);
            keys.text = "                      " + string.Join(", ", Helper.headers.Take(27)) +", n_l_ml_ms";
        }
        void OnToggle(bool bol)
        {
            if (string.IsNullOrEmpty(inputField.text)) return;
            OnSearch(inputField.text);
        }
        void OnSearch(string value)
        {
            data = Helper.data.ToList();
            if (string.IsNullOrEmpty(value))
            {
                OnEnableTable();
                return;
            }
            List<string> query = value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var q in query)
            {
                List<string> val = q.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (val.Count >= 3 && (val[1] == "=" || val[1] == ":"))
                {
                    string valueToSearch = string.Join(" ", val.Skip(2).Select(s => s.ToUpper()));
                    val[0] = val[0].ToLower();
                    if (val[0] == "group")
                    {
                        List<string> tags = Helper.elementsGroup;
                        if (toggle.isOn)
                            data = data.Where(dict => dict.Any(kvp =>
                            {
                                if (!kvp.Key.ToLower().Contains(val[0])) return false;
                                int temp = int.Parse(kvp.Value);
                                if (tags[int.Parse(kvp.Value) - 1].ToString().Contains(valueToSearch)) return true;
                                int tmp = int.Parse(kvp.Value) > 10 ? int.Parse(kvp.Value) - 10 : Math.Clamp(int.Parse(kvp.Value), 1, 8);
                                if (valueToSearch[valueToSearch.Length - 1] == 'A' || valueToSearch[valueToSearch.Length - 1] == 'B')
                                {
                                    if (!tmp.ToString().Contains(valueToSearch.Substring(0, valueToSearch.Length - 1))) return false;
                                    if (valueToSearch[valueToSearch.Length - 1] == 'A' && (temp > 12 || temp < 3)) return true;
                                    if (valueToSearch[valueToSearch.Length - 1] == 'B' && (temp < 13 && temp > 2)) return true;
                                }
                                else
                                {
                                    if (tmp.ToString().Contains(valueToSearch)) return true;
                                }
                                return false;
                            }
                            )).ToList();
                        else
                            data = data.Where(dict => dict.Any(kvp =>
                            {
                                if (!kvp.Key.ToLower().Contains(val[0])) return false;
                                int temp = int.Parse(kvp.Value);
                                if (tags[int.Parse(kvp.Value) - 1].ToString().Equals(valueToSearch)) return true;
                                int tmp = int.Parse(kvp.Value) > 10 ? int.Parse(kvp.Value) - 10 : Math.Clamp(int.Parse(kvp.Value), 1, 8);
                                if (valueToSearch[valueToSearch.Length - 1] == 'A' || valueToSearch[valueToSearch.Length - 1] == 'B')
                                {
                                    if (!tmp.ToString().Equals(valueToSearch.Substring(0, valueToSearch.Length - 1))) return false;
                                    if (valueToSearch[valueToSearch.Length - 1] == 'A' && (temp > 12 || temp < 3)) return true;
                                    if (valueToSearch[valueToSearch.Length - 1] == 'B' && (temp < 13 && temp > 2)) return true;
                                }
                                else
                                {
                                    if (tmp.ToString().Equals(valueToSearch)) return true;
                                }
                                return false;
                            }
                             )).ToList();
                    }
                    else if (val[0] == "n_l_ml_ms")
                    {
                        List<string> lNumber = Helper.lNumber;
                        List<string> searchs = valueToSearch.ToLower().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (searchs.Count !=4) continue;
                        string search = searchs[0] + searchs[1];
                        int lNum = lNumber.IndexOf(searchs[1]);
                        if (int.TryParse(searchs[0], out int g) && int.TryParse(searchs[2], out int gg) && lNum != -1)
                        {
                            int ml = 1 + lNum * 2;
                            if (searchs[3][0] == '+') search = search + (lNum + 1 + int.Parse(searchs[2]));
                            else if (searchs[3][0] == '-') search = search + (ml + lNum + 1 + int.Parse(searchs[2]));
                            data = data.Where(dict =>
                            {
                                if (toggle.isOn)
                                {
                                    if (dict["electron_configuration"]
                                        .ToLower()
                                        .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                        .Any(s => s[..2] == search[..2] && int.Parse(s[2..]) >= int.Parse(search[2..]))
                                        ) return true;
                                }
                                else
                                {
                                    if (dict["electron_configuration"]
                                        .ToLower()
                                        .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                        .Any(s => s == search)
                                        ) return true;
                                }
                                return false;
                            }).ToList();
                        }
                    }
                    else
                    {
                        if (toggle.isOn)
                            data = data.Where(dict => dict.Any(kvp => (kvp.Key.ToLower().Contains(val[0]) && kvp.Value.ToUpper().Contains(valueToSearch)))).ToList();
                        else
                            data = data.Where(dict => dict.Any(kvp => (kvp.Key.ToLower().Contains(val[0]) && kvp.Value.ToUpper() == valueToSearch))).ToList();
                    }
                }
            }
            OnEnableTable();
        }
        private void OnEnableTable()
        {
            int tam = bangTuanHoan.elements.Count;
            for (int i = 0; i < tam; i++)
            {
                if (data.Any(dict => dict.Any(kvp => (kvp.Key == "number" && kvp.Value.Equals((i + 1).ToString())))))
                {
                    bangTuanHoan.elements[i].SetActive(true);
                }
                else
                {
                    bangTuanHoan.elements[i].SetActive(false);
                }
            }
        }
        public void CreateTable()
        {
            List<Dictionary<string, string>> data = Helper.data;
            foreach (var row in data)
            {

                int ro = int.Parse(row["period"]) - 1;
                int col = int.Parse(row["group"]) - 1;
                string cat = row["category"];
                Color color;
                string symbol = row["symbol"];
                string name = row["name"];
                string number = row["number"];
                if (bangTuanHoan.category.ContainsKey(cat)) color = bangTuanHoan.category[cat];
                else
                {
                    color = colors[bangTuanHoan.category.Count];
                    bangTuanHoan.category.Add(cat, color);
                }
                if ((ro == 5 || ro == 6) && col == 2)
                    bangTuanHoan.ThemNguyenToVaoBangTuanHoan((ro - 5), int.Parse(row["number"]) - 57 - (ro - 5) * 32, bangTuanHoan.startPos2, color, symbol, name, number);
                else
                    bangTuanHoan.ThemNguyenToVaoBangTuanHoan(ro, col, bangTuanHoan.startPos, color, symbol, name, number);
            }
        }
        public void CreateTag()
        {
            List<string> tags = Helper.elementsGroup;
            for (int i = 0; i < tags.Count; i++)
            {
                if (i == 8 || i == 9) continue;
                GameObject tg = Instantiate(tagPrefabs, Vector3.zero, Quaternion.identity);
                tg.transform.SetParent(canvas.transform, false);
                tg.transform.localScale = new Vector3(1, 1, 1);
                tg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                tg.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(startPos.x + i * size + offset, startPos.y, startPos.z);
                tg.transform.GetComponent<TextMeshProUGUI>().text = tags[i];
            }
            // create tag for downbelow startpos2 for 1 to 7
            for (int i = 0; i < 8; i++)
            {
                GameObject tg = Instantiate(tagPrefabs, Vector3.zero, Quaternion.identity);
                tg.transform.SetParent(canvas.transform, false);
                tg.transform.localScale = new Vector3(1, 1, 1);
                tg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                tg.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(startPos2.x, startPos2.y - i * size - offset, startPos2.z);
                tg.transform.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                tg.transform.GetComponent<TextMeshProUGUI>().fontSize = 20;
            }
            // create tag for downbelow startpos3 for 6 to 7
            for (int i = 0; i < 2; i++)
            {
                GameObject tg = Instantiate(tagPrefabs, Vector3.zero, Quaternion.identity);
                tg.transform.SetParent(canvas.transform, false);
                tg.transform.localScale = new Vector3(1, 1, 1);
                tg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                tg.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(startPos3.x, startPos3.y - i * size - offset, startPos3.z);
                tg.transform.GetComponent<TextMeshProUGUI>().text = (i + 6).ToString();
                tg.transform.GetComponent<TextMeshProUGUI>().fontSize = 20;
            }
        }    
        public void CreateCategoryHeader()
        {
            for (int i = 0;i < categoryHeader.Count; i++)
            {
                categoryHeader[i].GetComponent<RawImage>().color = colors[i];
                categoryHeader[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = contrastColors[i];
                KeyValuePair<string, Color> pair = bangTuanHoan.category.Where(p => p.Value == colors[i]).FirstOrDefault();
                categoryHeader[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pair.Key;
            }
        }
    }
}
