<?php
$app->post('/user/register', function() use ($app) {
    $input = checkJsonInput($app->request->getBody());
    
    $email = $input->email;
    $first_name = $input->first_name;
    $last_name = $input->last_name;
    $password = $input->password;
    
    if ($email === NULL || $password === NULL || $first_name === NULL || $last_name === NULL) {
        sendMsg(false,'input insuffcient');
    }

    $account = DB::query("SELECT * FROM users WHERE email=%s", $email);
    $counter = DB::count();
    if($counter > 0)
    {
        sendMsg(false, "email already exists");
    }
    else
    {
        $password_hash = password_hash($password, PASSWORD_BCRYPT);
        DB::query("select * from users");
        $counter = DB::count();

        DB::insert('users', array(
            'first_name' => $first_name,
            'last_name' => $last_name,
            'email' => $email,
            'password_hash' => $password_hash,
            ));
        $counter2 = DB::count();

        if($counter != $counter2)
        {
            echo json_encode(['success' => true, 'msg' => 'registeration']);
            return;
        }
        else
        {
            sendMsg(false, "registeration failed");
        }

    }
});


$app->post('/user/facebookRegister', function() use ($app) {
    $input = checkJsonInput($app->request->getBody());
    
    $facebook_id = $input->facebook_id;

    if ($facebook_id === NULL) {
        sendMsg(false,'input insuffcient');
    }

    $account = DB::query("SELECT * FROM users WHERE facebook_id=%s", $facebook_id);
    $counter = DB::count();
    if($counter > 0)
    {
        sendMsg(false, "facebook_id already exists");
    }
    else
    {
        DB::query("select * from users");
        $counter = DB::count();

        DB::insert('users', array(
            'facebook_id' => $facebook_id
            ));
        $counter2 = DB::count();

        if($counter != $counter2)
        {
            echo json_encode(['success' => true, 'msg' => 'facebook_id registeration']);
            return;
        }
        else
        {
            sendMsg(false, "facebook_id registeration failed");
        }

    }
});



$app->post('/user/login', function() use ($app) {
    $input = checkJsonInput($app->request->getBody());

    $email = $input->email;
    $password = $input->password;
    if($email === NULL || $password === NULL)
    {
        sendMsg(false, 'input insufficient');
    }
    $checked = DB::queryFirstRow("select id, password_hash from users where email = %s", $email);
    if($checked === NULL || password_verify($password, $checked['password_hash']) === false)
    {
        sendMsg(false, "email and password don't match");
    }
    else
    {
        echo json_encode(['success' => true, 'msg' => "login successful", 'user_id' => $checked['id']]); 
        return;
    }
});


$app->post('/user/facebookLogin', function() use ($app) {
    $input = checkJsonInput($app->request->getBody());

    $facebook_id = $input->facebook_id;
    if($facebook_id === NULL)
    {
        sendMsg(false, 'input insufficient');
    }
    $checked = DB::queryFirstRow("select id from users where facebook_id = %s", $facebook_id);
    if($checked === NULL)
    {
        sendMsg(false, "facebook_id doesn't exist");
    }
    else
    {
        echo json_encode(['success' => true, 'msg' => "facebook login successful", 'user_id' => $checked['id']]); 
        return;
    }
});

$app->get('/user/:user_id', function($user_id) use ($app) {
    if(is_numeric($user_id) == false)
    {
        sendMsg(false, "invalid user_id. Numeric expected.");
    }

    $info = DB::queryFirstRow(
        "SELECT u.id, facebook_id, first_name, last_name, email, total_xp, lvl, attack, defense, happiness, total_steps, daily_steps FROM users as u, augmons as a, pedometers as p where u.id=%s and p.id=u.id and a.id=u.id", $user_id);
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

$app->post('/user/update', function() use ($app) {
    $input = checkJsonInput($app->request->getBody());
    $user_id = $input->user_id;
    $email = $input->email;
    $first_name = $input->first_name;
    $last_name = $input->last_name;
    $password = $input->password;

    if($user_id === NULL || $password === NULL)
    {
        sendMsg(false, "insufficient info");
    }

    $password_hash = password_hash($password, PASSWORD_BCRYPT);

    DB::update('users', array('email' => $email, 'first_name' => $first_name, 'last_name' => $last_name, 'password_hash' => $password_hash), "id=%s", $user_id);

    $counter = DB::affectedRows();

    if($counter == 0)
    {
        sendMsg(false, "update user info failed");
    }
    else
    {
        echo json_encode(['success' => true, 'msg' => "user info updated"]);
    }


});

?>
