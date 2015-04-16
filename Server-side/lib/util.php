<?php
function checkJsonInput($input) {
    $input  = str_replace('&quot;','"', $input);
    $input = json_decode($input);
    if ($input === NULL) {
        echo json_encode(['success'=>false, 'message'=>'unparseable input']);
        json_last_error();
        exit();
    }
    return $input;
}

function sendMsg($is_success, $data=[]) {
    echo json_encode(['success'=>$is_success, 'data'=>$data]);
    exit();
}

function getGeoBoundary($longitude, $latitude, $raidus) {
    $radus = $raidus * 2.5;
    
    $degree = 24901/360.0;
    $dpmLat = 1/$degree;

    $radiusLat = $dpmLat*$raidus;
    $minLat = $latitude - $radiusLat;
    $maxLat = $latitude + $radiusLat;

    $mpdLng = $degree * cos($latitude * (pi()/180));
    $dpmLng = 1 / $mpdLng;
    $radiusLng = $dpmLng * $raidus;
    $minLng = $longitude - $radiusLng;
    $maxLng = $longitude + $radiusLng;
    
    return ['min_lng'=>$minLng, 'max_lng'=>$maxLng, 'min_lat'=>$minLat, 'max_lat'=>$maxLat];
}

function checkLogin($api_key) {
    $data = DB::queryFirstRow("select users.* from sessions join users on sessions.user_id = users.user_id where api_key = %s and adddate(last_timestamp, interval 1 day) > now()", $api_key);
    if ($data == NULL) {
        return false;
    }
    DB::update('sessions', ['last_timestamp'=>DB::sqleval("now()")], "api_key=%s", $api_key);
    unset($data['password_hash']);
    return $data;
}
?>

