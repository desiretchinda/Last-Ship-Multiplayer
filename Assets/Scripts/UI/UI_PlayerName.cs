using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PlayerName : MonoBehaviour
{

    public TextMeshProUGUI txtName;

    PlayerManager owner;

    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if(owner == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (owner == null)
            return;

        rectTransform.position = GameManager.Instance.mainCamera.WorldToScreenPoint(owner.spawnBulletPt.position);
    }

    public void SetOwner(PlayerManager player)
    {
        owner = player;
        txtName.text = owner.photonView.Owner.NickName;
        if (owner.photonView.IsMine)
            txtName.color = Color.white;
        rectTransform.position = GameManager.Instance.mainCamera.WorldToScreenPoint(owner.spawnBulletPt.position);
    }
}
