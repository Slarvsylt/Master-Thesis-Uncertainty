using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MoveDatabase : MonoBehaviour
{
    public static MoveDatabase Instance { get; set; }
    public List<Move> Moves { get; set; }
    // Use this for initialization
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        Moves = JsonConvert.DeserializeObject<List<Move>>(Resources.Load<TextAsset>("JSON/Moves").ToString());
        //Debug.Log(Moves[0]);
    }

    public Move GetMove(string moveSlug)
    {
        foreach (Move move in Moves)
        {
            if (move.ObjectSlug == moveSlug)
                return new Move(move);
        }
        Debug.LogWarning("Couldn't find item: " + moveSlug);
        return null;
    }
}
