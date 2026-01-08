using Menu;
using UnityEngine;

public class PropellerRotating : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 1024.0f;
    void Start()
    {
        
    }

    void Update()
    {
		if (!GameManager.Instance._isGameActive)
			return;

		transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
