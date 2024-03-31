using Assets.Code.menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public class InfosSceneManager: MonoBehaviour
    {
        public static InfosSceneManager instance;
        public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
        public GameObject ROOT;
        public bool displaying = false;
        public List<GameObject> element3Ds = new List<GameObject>();
        private int num;
        public GameObject categoryImage;
        private void Start()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            num = -1;
        }
        public void ToggleInfoSceneManager(int number)
        {
            if (!ROOT)
            {
                Debug.LogError("No root object set for DebugManager!");
                return;
            }
            if (number == 0 && !displaying) return;
            if (!displaying) //turn on
            {
                DisplayText(number);
                ROOT.SetActive(true);
                displaying = true;
                if (number != 0) num = number - 1;
                element3Ds[num].SetActive(true);
            }
            else //turn off
            {
                ROOT.SetActive(false);
                displaying = false;
                if (num != -1)
                {
                    element3Ds[num].SetActive(false);
                    num = -1;
                }
            }
        }
        private void DisplayText(int number)
        {
            // unit for downbelow
            Dictionary<string, string> data = Helper.data[number-1];
            texts[0].text = "<color=#BC8F8F>Name:  </color>" + data["name"];
            texts[1].text = "<color=#BC8F8F>Number:  </color>" + data["number"];
            texts[13].text = "<color=#BC8F8F>Symbol:  </color>" + data["symbol"];
            int group = int.Parse(data["group"]);
            texts[2].text = "<color=#BC8F8F>Group:  </color>" + Helper.elementsGroup[ group- 1].ToString();
            texts[3].text = "<color=#BC8F8F>Period:  </color>" + data["period"];
            texts[4].text = "<color=#BC8F8F>Phase:  </color>" + data["phase"];
            texts[5].text = "<color=#BC8F8F>Melt:  </color>" + data["melt"] + "°C";
            texts[6].text = "<color=#BC8F8F>Boil:  </color>" + data["boil"] + "°C";
            texts[7].text = "<color=#BC8F8F>Atomic Mass:  </color>" + data["atomic_mass"]+"u";
            texts[8].text = "<color=#BC8F8F>Density:  </color>" + data["density"] + "kg/m³";
            texts[9].text = "<color=#BC8F8F>Molar_heat: </color>" + data["molar_heat"] + "cal/mol";
            texts[10].text = "<color=#BC8F8F>Category:  </color>" + data["category"];
            texts[11].text = "<color=#BC8F8F>Discovered by:  </color>" + data["discovered_by"];
            texts[12].text = "<color=#BC8F8F>Appearance:  </color>" + data["appearance"];
            texts[14].text = "<color=#BC8F8F>Electron  Configuration:  </color>" + data["electron_configuration"];
            categoryImage.GetComponent<Image>().color = TableSceneManager.Instance.bangTuanHoan.category[data["category"]];
        }
    }
}
