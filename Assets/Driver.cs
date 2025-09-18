using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 200f;
    [SerializeField] float moveSpeed = 12f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*  void Start()
     {
     }
  */
    // Update is called once per frame
    void Update()
    {

        float steer = 0f;
        float move = 0f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            /*   Debug.Log("we are pushing forward");
              transform.Translate(0, moveSpeed, 0); */

            move = 1f;
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            /*   Debug.Log("we are pushing backwards");
              transform.Translate(0, -moveSpeed, 0); */

            move = -1f;
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            /* Debug.Log("we are pushing left");
            transform.Rotate(0, 0, steerSpeed); */

            steer = 1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            /* Debug.Log("we are pushing right");
            transform.Rotate(0, 0, -steerSpeed); */

            steer = -1f;
        }

        float steerAmount = steer * steerSpeed * Time.deltaTime;
        float moveAmount = move * moveSpeed * Time.deltaTime;

        if (move < 0)
        {
            steerAmount *= -1;
        }

        transform.Rotate(0, 0, steerAmount);
        transform.Translate(0, moveAmount, 0);



        

    }
}