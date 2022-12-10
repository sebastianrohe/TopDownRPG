using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class PlayerExperience : MonoBehaviour {

    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private HammerHitbox HammerHitbox;
    [SerializeField] private Image experienceBar;
    [SerializeField] private int experienceNeeded;
    [SerializeField] private int currentExperience;
    // private float experienceValue = 0;
    public int CurrentExperience {
        set {
            if (value >= experienceNeeded) {
                OnLevelUp();
                currentExperience = 0;
                experienceBar.fillAmount = (float)currentExperience / experienceNeeded;
            } else {
                currentExperience = value;
                experienceBar.fillAmount = (float)currentExperience / experienceNeeded;
            }

        }
        get {
            return currentExperience;
        }
    }

    private void Start() {
        experienceBar.fillAmount = currentExperience / experienceNeeded;
        OnLevelUp();
    }

    private void OnLevelUp() {
        experienceNeeded = 2 * experienceNeeded;
        HammerHitbox.hammerDamage += 1;
        PlayerController.health += 1;
        PlayerController.numOfHearts += 1;
        print("LEVEL UP!");
        // TODO: HP + DMG erhöhen
    }
}
