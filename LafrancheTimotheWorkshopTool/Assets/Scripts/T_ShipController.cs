using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_ShipController : MonoBehaviour
{
    //On the Scriptable
    private float acceleration = 0.02f, maxSpeed = .1f;

    //On the GO
    [HideInInspector]
    public Vector2 shotDirection;
    private Vector3 moveDirection, inertieDirection, nextPosition;
    [SerializeField]
    private Camera cam;

    private float currentInertieX, currentInertieY;


    public T_ShipPlaytimeStatue currentStatue;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = currentStatue.baseShip.shipSprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;

        currentInertieX = Mathf.Lerp(currentInertieX, moveDirection.x* maxSpeed *2, acceleration);

        if(currentInertieX < acceleration / 50 && currentInertieX > -acceleration / 50)
        {
            currentInertieX = 0;
        }

        currentInertieY = Mathf.Lerp(currentInertieY, moveDirection.y* maxSpeed *2, acceleration);
        if (currentInertieY < acceleration / 50 && currentInertieY > -acceleration/50)
        {
            currentInertieY = 0;
        }

        inertieDirection = new Vector3(currentInertieX, currentInertieY, 0);


        nextPosition = transform.position + inertieDirection;
        nextPosition = new Vector3(Mathf.Clamp(nextPosition.x, -9, 9), Mathf.Clamp(nextPosition.y, -5, 5), 0);

        transform.position = nextPosition;
    }

    private void Update()
    {
        shotDirection = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.up = shotDirection;

        if(Input.GetMouseButtonDown(0))
        {
            currentStatue.TryToShoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inertieDirection /= 10;
    }
}
