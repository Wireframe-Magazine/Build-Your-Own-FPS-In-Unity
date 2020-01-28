using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UseAttacks : MonoBehaviour
{
    public int ammoAmount = 10;
    public float meleeRepeatDelay = 0.25f;
    public GameObject projectile;
    public GameObject punchMesh;
    public Text ammoPanel;
    private bool punchActive;

    private void Start()
    {
        //Update text to display the player ammo.
        UpdateText();
        //Hide the hand when we start the game and have ammo.
        punchMesh.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammoAmount > 0)
            {
                ammoAmount--;
                UpdateText();
                var clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
                //Destroy after 2 seconds to stop clutter.
                Destroy(clone, 5.0f);
            }
            else
            {
                if (!punchActive)
                {
                    punchActive = true;
                    StartCoroutine(MeleeAttack());
                }
            }
        }
    }

    void ApplyAmmo(int ammo)
    {
        ammoAmount += ammo;
        UpdateText();
    }

    void UpdateText()
    {
        //Check the ammo panel exists.
        if (ammoPanel != null)
        {
            //Sets the text on our panel.
            ammoPanel.text = ammoAmount.ToString();
        }
    }

    IEnumerator MeleeAttack()
    {
        punchMesh.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        punchMesh.SetActive(false);
        yield return new WaitForSeconds(meleeRepeatDelay);
        punchActive = false;
        yield return null;
    }
}
