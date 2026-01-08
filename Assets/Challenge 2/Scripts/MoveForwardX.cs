using Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardX : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
		if (!GameManager.Instance._isGameActive)
			return;

		transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
