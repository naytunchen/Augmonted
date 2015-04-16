<?php
$app->post('/pedometer/update', function() use ($app) {
	$input = checkJsonInput($app->request->getBody());
	$p_id = $input->p_id;
	$total_steps = $input->total_steps;
	$daily_steps = $input->daily_steps;

	if($p_id === NULL || $total_steps === NULL || $daily_steps === NULL)
	{
		sendMsg(false, 'insufficient info');
	}

	DB::update('pedometers', array(
		'total_steps' => $total_steps,
		'daily_steps' => $daily_steps
		), "id=%s", $p_id);

	$counter = DB::affectedRows();
	if($counter == 0)
	{
		sendMsg(false, "update pedometer failed");
	}
	else
	{
		echo json_encode(['success' => true, 'msg' => "pedometer info updated"]);
	}

});


$app->get('/pedometer/:p_id', function($p_id) use ($app) {
	if(is_numeric($p_id) == false)
	{
		sendMsg(false, "invalid p_id. Numeric expected.");
	}

	$info = DB::queryFirstRow(
		"SELECT * FROM pedometers where id=%s", $p_id);
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