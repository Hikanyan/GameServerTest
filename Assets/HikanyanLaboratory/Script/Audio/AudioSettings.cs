using System.Collections.Generic;

namespace HikanyanLaboratory
{
    public class AudioSettings
    {
        public string StreamingAssetsPathAcf { get; set; }
        public Dictionary<CueSheet, string> CueSheetPaths { get; } = new Dictionary<CueSheet, string>
        {
            { CueSheet.BGM, "CueSheet_BGM" },
            { CueSheet.SE, "CueSheet_SE" },
            { CueSheet.ME, "CueSheet_ME" }
        };
    }
}