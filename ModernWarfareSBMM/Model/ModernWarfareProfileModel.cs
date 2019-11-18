using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace ModernWarfareSBMM.Model
{
    public partial class ModernWarfareProfileResponseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public ModernWarfareProfileModel Data { get; set; }
    }

    public partial class ModernWarfareProfileModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("maxLevel")]
        public long MaxLevel { get; set; }

        [JsonProperty("levelXpRemainder")]
        public long LevelXpRemainder { get; set; }

        [JsonProperty("levelXpGained")]
        public long LevelXpGained { get; set; }

        [JsonProperty("prestige")]
        public long Prestige { get; set; }

        [JsonProperty("prestigeId")]
        public long PrestigeId { get; set; }

        [JsonProperty("maxPrestige")]
        public long MaxPrestige { get; set; }

        [JsonProperty("totalXp")]
        public long TotalXp { get; set; }

        [JsonProperty("paragonRank")]
        public long ParagonRank { get; set; }

        [JsonProperty("paragonId")]
        public long ParagonId { get; set; }

        [JsonProperty("lifetime")]
        public Lifetime Lifetime { get; set; }

        [JsonProperty("weekly")]
        public Weekly Weekly { get; set; }

        [JsonProperty("engagement")]
        public object Engagement { get; set; }
    }

    public partial class Lifetime
    {
        [JsonProperty("all")]
        public All All { get; set; }

        [JsonProperty("mode")]
        public Dictionary<string, All> Mode { get; set; }

        [JsonProperty("map")]
        public Map Map { get; set; }

        [JsonProperty("itemData")]
        public ItemData ItemData { get; set; }

        [JsonProperty("scorestreakData")]
        public ScorestreakData ScorestreakData { get; set; }

        [JsonProperty("accoladeData")]
        public AccoladeData AccoladeData { get; set; }
    }

    public partial class AccoladeData
    {
        [JsonProperty("properties")]
        public Dictionary<string, long> Properties { get; set; }
    }

    public partial class All
    {
        [JsonProperty("properties")]
        public Dictionary<string, double> Properties { get; set; }
    }

    public partial class ItemData
    {
        [JsonProperty("weapon_dmr")]
        public WeaponDmr WeaponDmr { get; set; }

        [JsonProperty("weapon_sniper")]
        public WeaponSniper WeaponSniper { get; set; }

        [JsonProperty("tacticals")]
        public Tacticals Tacticals { get; set; }

        [JsonProperty("lethals")]
        public Lethals Lethals { get; set; }

        [JsonProperty("weapon_lmg")]
        public WeaponLmg WeaponLmg { get; set; }

        [JsonProperty("weapon_launcher")]
        public WeaponLauncher WeaponLauncher { get; set; }

        [JsonProperty("weapon_pistol")]
        public WeaponPistol WeaponPistol { get; set; }

        [JsonProperty("weapon_assault_rifle")]
        public Dictionary<string, PuneHedgehog> WeaponAssaultRifle { get; set; }

        [JsonProperty("weapon_other")]
        public WeaponOther WeaponOther { get; set; }

        [JsonProperty("weapon_shotgun")]
        public WeaponShotgun WeaponShotgun { get; set; }

        [JsonProperty("weapon_smg")]
        public WeaponSmg WeaponSmg { get; set; }

        [JsonProperty("weapon_melee")]
        public WeaponMelee WeaponMelee { get; set; }
    }

    public partial class Lethals
    {
        [JsonProperty("equip_frag")]
        public PuneHedgehog EquipFrag { get; set; }

        [JsonProperty("equip_thermite")]
        public PuneHedgehog EquipThermite { get; set; }

        [JsonProperty("equip_semtex")]
        public PuneHedgehog EquipSemtex { get; set; }

        [JsonProperty("equip_claymore")]
        public PuneHedgehog EquipClaymore { get; set; }

        [JsonProperty("equip_c4")]
        public PuneHedgehog EquipC4 { get; set; }

        [JsonProperty("equip_at_mine")]
        public PuneHedgehog EquipAtMine { get; set; }

        [JsonProperty("equip_throwing_knife")]
        public PuneHedgehog EquipThrowingKnife { get; set; }

        [JsonProperty("equip_molotov")]
        public PuneHedgehog EquipMolotov { get; set; }
    }

    public partial class PuneHedgehog
    {
        [JsonProperty("properties")]
        public PurpleProperties Properties { get; set; }
    }

    public partial class PurpleProperties
    {
        [JsonProperty("hits")]
        public long Hits { get; set; }

        [JsonProperty("kills")]
        public long Kills { get; set; }

        [JsonProperty("shots")]
        public long Shots { get; set; }

        [JsonProperty("deaths")]
        public long Deaths { get; set; }

        [JsonProperty("headShots")]
        public long HeadShots { get; set; }
    }

    public partial class Tacticals
    {
        [JsonProperty("equip_gas_grenade")]
        public PuneHedgehog EquipGasGrenade { get; set; }

        [JsonProperty("equip_snapshot_grenade")]
        public PuneHedgehog EquipSnapshotGrenade { get; set; }

        [JsonProperty("equip_decoy")]
        public PuneHedgehog EquipDecoy { get; set; }

        [JsonProperty("equip_smoke")]
        public PuneHedgehog EquipSmoke { get; set; }

        [JsonProperty("equip_concussion")]
        public PuneHedgehog EquipConcussion { get; set; }

        [JsonProperty("equip_hb_sensor")]
        public PuneHedgehog EquipHbSensor { get; set; }

        [JsonProperty("equip_flash")]
        public PuneHedgehog EquipFlash { get; set; }

        [JsonProperty("equip_adrenaline")]
        public PuneHedgehog EquipAdrenaline { get; set; }
    }

    public partial class WeaponDmr
    {
        [JsonProperty("iw8_sn_kilo98")]
        public PuneHedgehog Iw8SnKilo98 { get; set; }

        [JsonProperty("iw8_sn_mike14")]
        public PuneHedgehog Iw8SnMike14 { get; set; }

        [JsonProperty("iw8_sn_delta")]
        public PuneHedgehog Iw8SnDelta { get; set; }
    }

    public partial class WeaponLauncher
    {
        [JsonProperty("iw8_la_gromeo")]
        public PuneHedgehog Iw8LaGromeo { get; set; }

        [JsonProperty("iw8_la_rpapa7")]
        public PuneHedgehog Iw8LaRpapa7 { get; set; }

        [JsonProperty("iw8_la_juliet")]
        public PuneHedgehog Iw8LaJuliet { get; set; }

        [JsonProperty("iw8_la_kgolf")]
        public PuneHedgehog Iw8LaKgolf { get; set; }

        [JsonProperty("iw8_la_mike32")]
        public PuneHedgehog Iw8LaMike32 { get; set; }
    }

    public partial class WeaponLmg
    {
        [JsonProperty("iw8_lm_kilo121")]
        public PuneHedgehog Iw8LmKilo121 { get; set; }

        [JsonProperty("iw8_lm_mgolf34")]
        public PuneHedgehog Iw8LmMgolf34 { get; set; }

        [JsonProperty("iw8_lm_lima86")]
        public PuneHedgehog Iw8LmLima86 { get; set; }

        [JsonProperty("iw8_lm_pkilo")]
        public PuneHedgehog Iw8LmPkilo { get; set; }

        [JsonProperty("iw8_lm_dblmg")]
        public PuneHedgehog Iw8LmDblmg { get; set; }
    }

    public partial class WeaponMelee
    {
        [JsonProperty("iw8_knife")]
        public PuneHedgehog Iw8Knife { get; set; }
    }

    public partial class WeaponOther
    {
        [JsonProperty("iw8_me_riotshield")]
        public PuneHedgehog Iw8MeRiotshield { get; set; }
    }

    public partial class WeaponPistol
    {
        [JsonProperty("iw8_pi_cpapa")]
        public PuneHedgehog Iw8PiCpapa { get; set; }

        [JsonProperty("iw8_pi_mike1911")]
        public PuneHedgehog Iw8PiMike1911 { get; set; }

        [JsonProperty("iw8_pi_golf21")]
        public PuneHedgehog Iw8PiGolf21 { get; set; }

        [JsonProperty("iw8_pi_decho")]
        public PuneHedgehog Iw8PiDecho { get; set; }

        [JsonProperty("iw8_pi_papa320")]
        public PuneHedgehog Iw8PiPapa320 { get; set; }
    }

    public partial class WeaponShotgun
    {
        [JsonProperty("iw8_sh_charlie725")]
        public PuneHedgehog Iw8ShCharlie725 { get; set; }

        [JsonProperty("iw8_sh_oscar12")]
        public PuneHedgehog Iw8ShOscar12 { get; set; }

        [JsonProperty("iw8_sh_romeo870")]
        public PuneHedgehog Iw8ShRomeo870 { get; set; }

        [JsonProperty("iw8_sh_dpapa12")]
        public PuneHedgehog Iw8ShDpapa12 { get; set; }
    }

    public partial class WeaponSmg
    {
        [JsonProperty("iw8_sm_mpapa7")]
        public PuneHedgehog Iw8SmMpapa7 { get; set; }

        [JsonProperty("iw8_sm_augolf")]
        public PuneHedgehog Iw8SmAugolf { get; set; }

        [JsonProperty("iw8_sm_papa90")]
        public PuneHedgehog Iw8SmPapa90 { get; set; }

        [JsonProperty("iw8_sm_mpapa5")]
        public PuneHedgehog Iw8SmMpapa5 { get; set; }

        [JsonProperty("iw8_sm_beta")]
        public PuneHedgehog Iw8SmBeta { get; set; }

        [JsonProperty("iw8_sm_uzulu")]
        public PuneHedgehog Iw8SmUzulu { get; set; }
    }

    public partial class WeaponSniper
    {
        [JsonProperty("iw8_sn_sbeta")]
        public PuneHedgehog Iw8SnSbeta { get; set; }

        [JsonProperty("iw8_sn_alpha50")]
        public PuneHedgehog Iw8SnAlpha50 { get; set; }

        [JsonProperty("iw8_sn_hdromeo")]
        public PuneHedgehog Iw8SnHdromeo { get; set; }
    }

    public partial class Map
    {
    }

    public partial class ScorestreakData
    {
        [JsonProperty("lethalScorestreakData")]
        public Dictionary<string, LethalScorestreakDatum> LethalScorestreakData { get; set; }

        [JsonProperty("supportScorestreakData")]
        public SupportScorestreakData SupportScorestreakData { get; set; }
    }

    public partial class LethalScorestreakDatum
    {
        [JsonProperty("properties")]
        public LethalScorestreakDatumProperties Properties { get; set; }
    }

    public partial class LethalScorestreakDatumProperties
    {
        [JsonProperty("extraStat1")]
        public long ExtraStat1 { get; set; }

        [JsonProperty("uses")]
        public long Uses { get; set; }

        [JsonProperty("awardedCount")]
        public long AwardedCount { get; set; }
    }

    public partial class SupportScorestreakData
    {
        [JsonProperty("airdrop")]
        public LethalScorestreakDatum Airdrop { get; set; }

        [JsonProperty("radar_drone_overwatch")]
        public LethalScorestreakDatum RadarDroneOverwatch { get; set; }

        [JsonProperty("scrambler_drone_guard")]
        public LethalScorestreakDatum ScramblerDroneGuard { get; set; }

        [JsonProperty("uav")]
        public LethalScorestreakDatum Uav { get; set; }

        [JsonProperty("airdrop_multiple")]
        public LethalScorestreakDatum AirdropMultiple { get; set; }

        [JsonProperty("directional_uav")]
        public LethalScorestreakDatum DirectionalUav { get; set; }
    }

    public partial class Weekly
    {
        [JsonProperty("all")]
        public All All { get; set; }

        [JsonProperty("mode")]
        public Mode Mode { get; set; }

        [JsonProperty("map")]
        public Map Map { get; set; }
    }

    public partial class Mode
    {
        [JsonProperty("dom")]
        public All Dom { get; set; }

        [JsonProperty("war")]
        public All War { get; set; }

        [JsonProperty("conf")]
        public All Conf { get; set; }

        [JsonProperty("arm")]
        public All Arm { get; set; }
    }
}
