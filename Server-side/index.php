<?php
require 'Slim/Slim.php';	// include the framework in the project
require_once 'config.php';
require_once 'meekrodb.php';
require_once 'lib/util.php';

DB::$host = DB_HOST;
DB::$dbName = DB_NAME;
DB::$user = DB_USERNAME;
DB::$password = DB_PASSWORD;

\Slim\Slim::registerAutoloader();	// register the autoloader

$app = new \Slim\Slim();

$app->hook('slim.before.router', function() use ($app) {
    $path = $app->request()->getPathInfo();
    if(strpos($path, "/user") === 0) {
        require_once('routes/user.php');
    } 
    else if(strpos($path, "/augmon") === 0)
    {
        require_once('routes/augmon.php');
    }
    else if(strpos($path, "/pedometer") === 0)
    {
    	require_once('routes/pedometer.php');
    }
});

$app->run();

