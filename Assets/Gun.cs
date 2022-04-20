using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Gun : MonoBehaviour
{
    public enum elements {Fire, Earth, Ice, Wind};

    public elements elType = elements.Fire;        // the default elemental type.

    public string name = "Gun";
    public int damage = 2;
    public float rateOfFire = 0.5f;

    [Header("Randomization")]
    public List<string> names;
    public Vector2 damageRange;
    public Vector2 rateOfFireRange;

    public TextMeshPro nameText;

    void Start() {
        // nameText = transform.GetChild(1).GetComponent<TextMeshPro>();
        Debug.Log("nameText: " + nameText);
    }

    public void Randomize() {
        name = names[(int)elType];
        nameText.text = name;

        damage = (int)Random.Range(damageRange.x, damageRange.y);
        rateOfFire = Random.Range(rateOfFireRange.x, rateOfFireRange.y);
    }


    public Rigidbody bulletPrefab;  // the bullet to be spawned
    public Transform bulSpawn;      // the bullet spawn location

    bool onCooldown = false;

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        if(mouse == null) return;

        // isPressed
        if(mouse.leftButton.isPressed) {
            Fire();
        }
    }

    void Fire() {
        if(!onCooldown) {
            Rigidbody bullet = Instantiate(bulletPrefab, bulSpawn.position, bulSpawn.rotation);
            bullet.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
            Bullet bulletStuff = bullet.GetComponent<Bullet>();
            bulletStuff.elType = this.elType;
            bulletStuff.damage = this.damage;
            
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown() {
        onCooldown = true;
        yield return new WaitForSeconds(.5f);
        onCooldown = false;
    }
}
