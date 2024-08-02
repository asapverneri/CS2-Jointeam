using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace CS2Jointeam
{
    public class CS2JointeamConfig : BasePluginConfig
    {
        public override int Version { get; set; } = 1;

		[JsonPropertyName("JOIN_CT_COMMAND")]
		public string Joinctcommand { get; set; } = "css_ct";

        [JsonPropertyName("JOIN_T_COMMAND")]
        public string Jointerroristcommand { get; set; } = "css_t";

        [JsonPropertyName("JOIN_SPEC_COMMAND")]
        public string Joinspectate { get; set; } = "css_spec";

    }
}
