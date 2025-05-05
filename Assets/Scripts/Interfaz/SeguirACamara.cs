using UnityEngine;
using UnityEngine.InputSystem;

public class SeguirACamara : MonoBehaviour
{
[SerializeField]private Transform centroCamara;
    void Awake()
    {
        this.transform.position = new Vector3(centroCamara.position.x, centroCamara.position.y, this.transform.position.z);
        this.transform.parent = this.centroCamara;
    }
}
