public enum E_Audio
{
    // BGM
    #region BGM
    None = -1,
    Stage_1_BGM,
    Stage_2_BGM,

    #endregion

    // SFX

    #region 캐릭터

    Char_Dash = 10,
    Char_RangeAttack,
    Char_Walk,
    Char_AfterJump,
    Char_Jump,
    Char_Damaged,
    Char_Dead,
    Char_DashMeleeAttack,
    Char_ArmUnit,
    Char_MeleeSkill_1,
    Char_MeleeSkill_2,
    Char_Drain,
    Char_LongRangeSkill_3,
    Char_LongRangeSkill_4,
    Char_LongRangeSkill_5_Rotate,
    Char_LongRangeSkill_5_Shoot,

    #endregion

    #region 몬스터

    Base_Attack = 40,
    Base_Damage,

    Base_Jump,

    #endregion

    #region UI

    UI_ButtonClick = 100,
    UI_ButtonClick2,
    UI_ButtonMove,
    UI_GetChip,
    UI_ShopOnOff,
    UI_ShopConfirm,
    UI_Backpack,
    UI_PopUp,
    UI_GameOver

    #endregion
}