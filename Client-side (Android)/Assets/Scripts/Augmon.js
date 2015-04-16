#pragma strict

class Augmon
{
	var name        : String;
	var model       : GameObject;
	var rarity      : Rarity;
    var baseHP      : float;
    var currHP      : float;
    var baseAtk     : float;
    var currAtk     : float;
    var baseDef     : float;
    var currDef     : float;
    var baseSpeed   : float;
    var currSpeed   : float;
}

enum Rarity
{
	common,
	rare
}