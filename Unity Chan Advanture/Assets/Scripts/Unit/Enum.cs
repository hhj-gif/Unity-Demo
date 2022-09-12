public enum GemType{
	Fire = 0b001,Cold=0b010,Flash=0b100
}

public enum	MagicType
{
	Fire = 0b001, Cold = 0b010, Flash = 0b100, Broken = 0b011, Explode = 0b101,HardIce = 0b110, Hurricane = 0b111
}

public enum MonsterStateTpye
{
	Idle, Patrol,Pursue,Attack,GetHit,Die
}

public enum CellType
{
	ForwardBack,ForwardLeft,NotForward
}