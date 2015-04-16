<?php
$app->post('/augmon/update', function() use ($app) {
	$input = checkJsonInput($app->request->getBody());
	$augmon_id = $input->augmon_id;
	$name = $input->name;
	$total_xp = $input->total_xp;
	$attack = $input->attack;
	$defense = $input->defense;
	$happiness = $input->happiness;

	if($augmon_id === NULL || $total_xp === NULL || $attack === NULL || $defense === NULL || $happiness === NULL)
	{
		sendMsg(false, 'insufficient info');
	}

	DB::update('augmons', array(
		'name' => $name,
		'total_xp' => $total_xp,
		'attack' => $attack,
		'defense' => $defense,
		'happiness' => $happiness
		), "id=%s", $augmon_id);

	$counter = DB::affectedRows();
	if($counter == 0)
	{
		sendMsg(false, "update augmon failed");
	}
	else
	{
		echo json_encode(['success' => true, 'msg' => "augmon info updated"]);
	}

});

$app->post('/augmon/updateName', function() use ($app) {
	$input = checkJsonInput($app->request->getBody());
	$augmon_id = $input->augmon_id;
	$name = $input->name;

	if($augmon_id === NULL || $name === NULL)
	{
		sendMsg(false, 'insufficient info');
	}

	DB::update('augmons', array(
		'name' => $name
		), "id=%s", $augmon_id);

	$counter = DB::affectedRows();
	if($counter == 0)
	{
		sendMsg(false, "update augmon failed");
	}
	else
	{
		echo json_encode(['success' => true, 'msg' => "augmon name updated"]);
	}

});


$app->get('/augmon/:augmon_id', function($augmon_id) use ($app) {
	if(is_numeric($augmon_id) == false)
	{
		sendMsg(false, "invalid augmon_id. Numeric expected.");
	}

	$info = DB::queryFirstRow(
		"SELECT * FROM augmons where id=%s", $augmon_id);
	$counter = DB::count();
	if($counter == 0)
	{
		sendMsg(false, "data doesn't exists");
	}
	else if($counter > 0)
	{
		sendMsg(true, $info);
	}
});
?>