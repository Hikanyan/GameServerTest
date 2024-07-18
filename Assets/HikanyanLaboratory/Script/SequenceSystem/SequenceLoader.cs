using System.Collections;
using System.Collections.Generic;
using HikanyanLaboratory.Script.SequenceSystem;
using UnityEngine;

public class SequenceLoader : MonoBehaviour
{
    [Header("Debug機能")] [SerializeField] private bool _isDebug;
    [SerializeReference, SubclassSelector] private ISequence[] _debugSequenceList;

    public ISequence[] GetSequences()
    {
        return _debugSequenceList;
    }
}