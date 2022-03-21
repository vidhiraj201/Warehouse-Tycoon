using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Control
{
    public class controlUI : MonoBehaviour
    {
        public GameObject UI;

        public void hideUI()
        {
            if (UI.activeSelf)
                UI.SetActive(false);
        }
        public void CloseButton()
        {
            UI.GetComponent<Animator>().Play("Exit");
        }

    }
}

