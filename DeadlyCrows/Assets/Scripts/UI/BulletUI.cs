using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject bulletIcon;
    [SerializeField] HorizontalLayoutGroup bulletLayoutGroup;

    List<GameObject> bulletIcons = new();

    private void OnEnable()
    {
        Player.CurrentBulletsChangedEventHandler += HandleCurrentBulletsChanged;
    }
    private void OnDisable()
    {
        Player.CurrentBulletsChangedEventHandler -= HandleCurrentBulletsChanged;
    }

    private void Start()
    {
        for (int i = 0; i < player.GunData.MaxBullets; i++)
        {
            var icon = Instantiate(bulletIcon, bulletLayoutGroup.transform);
            bulletIcons.Add(icon);
            
            icon.SetActive(false);
        }

        for (int i = 0; i < player.CurrentBullets; i++)
        {
            bulletIcons[i].SetActive(true);
        }
    }

    void HandleCurrentBulletsChanged(object sender, CurrentBulletsChangedEventArgs e)
    {
        Debug.Log($"change amount: {e.changeAmount}");
        for (int i = 0; i < Mathf.Abs(e.changeAmount); i++)
        {
            if (e.changeAmount < 0)
            {
                var lastEnabled = bulletIcons.Where(o => o.activeInHierarchy).LastOrDefault();

                if (lastEnabled != null)
                    lastEnabled.SetActive(false);
            }
            else
            {
                var firstDisabled = bulletIcons.Where(o => !o.activeInHierarchy).FirstOrDefault();
                
                if (firstDisabled != null)
                    firstDisabled.SetActive(true);
            }
        }
    }

}
