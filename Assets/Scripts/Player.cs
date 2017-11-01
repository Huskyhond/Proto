using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerKeys
{
    public KeyCode DropItemKey;
    public KeyCode TeleportItemKey;
    public KeyCode MoveUpKey, MoveDownKey, MoveRightKey, MoveLeftKey;
}

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public int playerId = 1;
    public PlayerKeys Inputs { get { return _inputs; } }
    public bool HasPickable { get { return _pickable != null; } }
    public Pickable Pickable { get { return _pickable; } }
    [SerializeField] private PlayerKeys _inputs = new PlayerKeys();
    [Range(1, 100)] [SerializeField] private float _movementSpeed = 10f;
    private bool _onFloor = false;
    private Pickable _pickable;
    private Rigidbody _rb;
    private Vector3 _startLocation;

    void Start()
    {
        _startLocation = transform.position;
        _rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (_onFloor)
        {
            _rb.velocity = Vector3.zero;
            if (Input.GetKey(_inputs.MoveUpKey) || Input.GetAxis("AxisY" + playerId) < -0.1f)
            {
                if (playerId == 1)
                    Move(Vector3.right);
                else
                    Move(Vector3.left);
            }
            if (Input.GetKey(_inputs.MoveDownKey) || Input.GetAxis("AxisY" + playerId) > 0.1f)
            {
                if (playerId == 1)
                    Move(Vector3.left);
                else
                    Move(Vector3.right);
            }
            if (Input.GetKey(_inputs.MoveLeftKey) || Input.GetAxis("AxisX" + playerId) < -0.1f)
            {
                if (playerId == 1)
                    Move(Vector3.forward);
                else
                    Move(Vector3.back);
            }
            if (Input.GetKey(_inputs.MoveRightKey) || Input.GetAxis("AxisX" + playerId) > 0.1f)
            {
                if (playerId == 1)
                    Move(Vector3.back);
                else
                    Move(Vector3.forward);
            }
            if (Input.GetKeyDown(_inputs.DropItemKey) || Input.GetButtonDown("Release" + playerId))
            {
                if (HasPickable) _pickable.Drop();
                RemovePickable();
            }
            if (Input.GetKeyDown(_inputs.TeleportItemKey) || Input.GetButtonDown("Throw" + playerId))
            {
                if (HasPickable) _pickable.Teleport(this);
                RemovePickable();
            }
        }
        if (transform.position.y < -3f)
        {
            Respawn();
        }
    }

    public void Move(Vector3 direction)
    {
        _rb.velocity += direction * _movementSpeed;
    }

    public void Pickup(Pickable pickable)
    {
        _pickable = pickable;
        _pickable.GetComponent<Rigidbody>().isKinematic = true;
        _pickable.gameObject.transform.parent = transform;
        _pickable.transform.position = transform.position + (Vector3.up * 2f);
    }

    public void Respawn()
    {
        transform.position = _startLocation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Island"))
        {
            _onFloor = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Island"))
        {
            _onFloor = false;
        }
    }

    public void RemovePickable()
    {
        _pickable = null;
    }

}
