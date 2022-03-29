using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace warehouse.Move
{
    public class movePlayer : MonoBehaviour
    {
        public Joystick Joystick;
        public Transform groundC;
        public float speed;
        public float rotationSmooth;
        public LayerMask ground;

        private CharacterController CharacterController;
        private float turnSmoothVelocity;
        //private Transform cam;

        bool isGrounded;
        float gravity;
        Vector3 velocity;
        void Start()
        {
            CharacterController = GetComponent<CharacterController>();
        }


        void Update()
        {

        }
        private void FixedUpdate()
        {
            MovePlayer();

            //Gravity();
        }

        [HideInInspector] public Vector3 direction;
        public void MovePlayer()
        {
            float x = Joystick.Horizontal;
            float z = Joystick.Vertical;

            direction = new Vector3(x, 0, z).normalized;

            if (direction.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                CharacterController.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
        void Gravity()
        {
            isGrounded = Physics.CheckSphere(groundC.position, 0.2f, ground);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            velocity.y += gravity * Time.deltaTime;
            CharacterController.Move(velocity * Time.deltaTime);
        }
    }
}
