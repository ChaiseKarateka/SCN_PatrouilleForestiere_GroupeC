using System.Collections;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Animator animator;
    public UIManager uiManager;
    public NPCPatrol npcPatrol;

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

            case "Randonneur":
                dialogues = new string[]
                {
                    "Bonjour monsieur, y-a-t-il un problème ?",
                    "J'ai fait tombé mes déchets ? Vraiment désolé, je ne voulais pas salir la forêt.",
                    "Merci de m'avoir prévenu, je vais faire plus attention à l'avenir."
                };
                break;
        }
    }

    public void Interact(Transform player)
    {
        if (npcPatrol != null)
        {
            npcPatrol.StopPatrol();
        }
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

        if (npcPatrol != null)
        {
            npcPatrol.ResumePatrol();
            npcPatrol.StopDropping();
        }
    }
}