using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] WaveManager wave_manager;
    [SerializeField] CastleStatus castle_status;
    [SerializeField] CardPanelUI card_panel;

    [Header("Cartas")]
    [SerializeField] List<CardData> all_cards;
    [SerializeField] int cards_per_offer = 3;
    [SerializeField] int enchanted_every = 5;
    [SerializeField] float rare_chance = 0.02f;

    private void OnEnable()
    {
        wave_manager.OnWaveEnded += HandleWaveEnded;
    }

    private void OnDisable()
    {
        wave_manager.OnWaveEnded -= HandleWaveEnded;
    }

    void HandleWaveEnded(int wave)
    {
        bool enchanted_wave = wave % enchanted_every == 0;
        List<CardData> offer = BuildOffer(enchanted_wave);

        // Sem cartas disponíveis: segue direto para a próxima onda
        if (offer.Count == 0)
        {
            wave_manager.BeginNextWave();
            return;
        }

        Time.timeScale = 0f;
        card_panel.Show(offer, OnCardChosen);
    }

    List<CardData> BuildOffer(bool enchanted_wave)
    {
        List<CardData> offer = new List<CardData>();

        for (int i = 0; i < cards_per_offer; i++)
        {
            CardRarity rarity;

            if (enchanted_wave)
            {
                rarity = CardRarity.Enchanted;
            }
            else
            {
                rarity = Random.value < rare_chance ? CardRarity.Rare : CardRarity.Common;
            }

            CardData card = PickCard(rarity, offer);

            // Se não sobrou da raridade sorteada, tenta a outra comum/rara
            if (card == null && !enchanted_wave)
            {
                CardRarity other = rarity == CardRarity.Rare ? CardRarity.Common : CardRarity.Rare;
                card = PickCard(other, offer);
            }

            if (card != null)
            {
                offer.Add(card);
            }
        }

        return offer;
    }

    CardData PickCard(CardRarity rarity, List<CardData> already_offered)
    {
        List<CardData> candidates = new List<CardData>();

        foreach (CardData card in all_cards)
        {
            if (card.rarity == rarity && !already_offered.Contains(card))
            {
                candidates.Add(card);
            }
        }

        if (candidates.Count == 0) return null;

        return candidates[Random.Range(0, candidates.Count)];
    }

    void OnCardChosen(CardData card)
    {
        card.Apply(castle_status);

        card_panel.Hide();
        Time.timeScale = 1f;

        wave_manager.BeginNextWave();
    }
}