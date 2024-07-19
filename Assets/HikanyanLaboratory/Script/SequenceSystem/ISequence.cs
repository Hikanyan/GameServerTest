using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace HikanyanLaboratory.Script.SequenceSystem
{
    public interface ISequence
    {
        public void SetData(SequenceData data);
        
        public UniTask PlayAsync(CancellationToken ct, Action<Exception> exceptionHandler = null);

        public void Skip();
    }
}