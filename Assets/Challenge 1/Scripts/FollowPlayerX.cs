using Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    [SerializeField]
    private GameObject _plane;

    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
		_offset = transform.position + _plane.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (!GameManager.Instance._isGameActive)
			return;

		transform.position = _plane.transform.position + _offset;
    }
}
