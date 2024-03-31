using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assembly_CSharp
{
    public class Manager : MonoBehaviour
    {
        public static Manager instance;
        private void Start()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            Helper.ReadCSVFile();
            Helper.TurnOnSceneAddictiveMode();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && InfosSceneManager.instance.displaying)
            {
                Helper.ToggleInfoScene(0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && !InfosSceneManager.instance.displaying)
                {
                    if (hit.collider.gameObject.GetComponent<Clickable>() != null)
                    {
                        hit.collider.gameObject.GetComponent<Clickable>().ToggleInfo();
                    }
                }
            }
        }
    }
}
