using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletCellType
{
    NON_SPECIFIC_IMMUNITY_CELL,
    SPECIFIC_IMMUNITY_CELL
}

public class BulletCell : MonoBehaviour
{
    // use string to represent it
    public string feature;
    public BulletCellType type;

    private Vector3 direction;
    public float bullet_speed = 1.1f;

    void Start()
    {
        Destroy(gameObject,1f);
    }

    void Update()
    {
        Move();
        CheckBoundary();
    }

    void Move()
    {
        //transform.Translate(bullet_speed*Time.deltaTime * direction);
        transform.position += direction * bullet_speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 vec)
    {
        direction = Vector3.Normalize(vec);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpecialVirus")
        {
            SpecialVirus property = other.gameObject.GetComponent<SpecialVirus>();
            if( type == BulletCellType.NON_SPECIFIC_IMMUNITY_CELL || (type == BulletCellType.SPECIFIC_IMMUNITY_CELL && property.GetVirusFeature() == feature))
                Destroy(other.gameObject);


            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "GeneralPathogens")
        {
            Debug.Log(other);
            if (type == BulletCellType.NON_SPECIFIC_IMMUNITY_CELL)
                Destroy(other.gameObject);

            Destroy(gameObject);
        }
    }

    void CheckBoundary()
    {
        int x = Camera.main.pixelWidth;
        int y = Camera.main.pixelHeight;
        if(transform.position.x < -x || transform.position.x >x || transform.position.y < -y || transform.position.y > y)
            Destroy(gameObject);
    }

    public void SetFeature(string str)
    {
        feature = str;
    }

    public void SetType(BulletCellType btype)
    {
        type = btype;
    }
}
