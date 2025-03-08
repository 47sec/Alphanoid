using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float cameraRotationSpeed;
    [SerializeField] private float bodyRotationSpeed;
    [SerializeField] private float moveSpeed;
    private CharacterController character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _camera.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraRotationSpeed * Time.deltaTime);
        _camera.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * cameraRotationSpeed * Time.deltaTime);


    }
}
