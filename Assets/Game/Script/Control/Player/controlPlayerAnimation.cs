using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wearhouse.Control
{
    public class controlPlayerAnimation : MonoBehaviour
    {
        public Animator Animation;

        private Move.movePlayer movePlayer;
        private controlPlayerInventory controlPlayerInventory;
        void Start()
        {
            movePlayer = GetComponent<Move.movePlayer>();
            controlPlayerInventory = GetComponent<controlPlayerInventory>();
        }


        void Update()
        {
            characterAnimation();
        }

        void characterAnimation()
        {
            Animation.SetFloat("speed", movePlayer.direction.magnitude);
            if (controlPlayerInventory.Cart.Count > 0)
                Animation.SetBool("hold", true);
            if (controlPlayerInventory.Cart.Count <= 0)
                Animation.SetBool("hold", false);
        }
    }
}
