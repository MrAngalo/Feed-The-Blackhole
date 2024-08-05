using UnityEngine;
using UnityEngine.InputSystem;

public class MyPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnMove(InputValue value) {
        Debug.Log(value.Get<float>());
    }

    protected void OnJump(InputValue value) {
        
    }
}
