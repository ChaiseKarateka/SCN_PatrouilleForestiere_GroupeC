using System.Collections;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Animator animator;
    public UIManager uiManager;

    private string[] dialogues;
    private int index = 0;

    void LoadDialogues()
    {
        // assigner dialogues selon tag
        switch (tag)
        {
            case "Garde forestier":
                dialogues = new string[]
                {
                    "Bienvenue dans votre nouveau poste de garde forestier !",
                    "Votre objectif ici est de prendre soin de cette fôret.",
                    "Nettoyer des aires de pique-nique, prévenir des éventuels incendies ou encore notifier des anomiles, voici les tâches que tu dois accomplir.",
                    "N'hésite pas non plus à parler aux visiteurs si leur comportement est dangereux pour la forêt.",
                    "Maintenant que vous connaissez vos objectifs, il est temps de se mettre au travail !"
                };
                break;

            case "Merchant":
                dialogues = new string[]
                {
                    "Bienvenue dans ma boutique !",
                    "J'ai les meilleurs prix.",
                    "Reviens quand tu veux !"
                };
                break;
        }
    }

    public void Interact(Transform player)
    {
        LoadDialogues();
        StartCoroutine(Talk(player));
    }

    System.Collections.IEnumerator Talk(Transform player)
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);

        while (index < dialogues.Length)
        {
            animator.Play("speak", 0, 0f);
            uiManager.ShowText(dialogues[index], gameObject.tag);

            index++;

            yield return new WaitForSeconds(4f);
        }

        index = 0;
    }
}