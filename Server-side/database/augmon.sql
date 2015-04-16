drop table if exists pedometers;
drop table if exists augmons;
drop table if exists users;



CREATE TABLE IF NOT EXISTS users
(
	id INTEGER(100) unsigned AUTO_INCREMENT ,
	facebook_id VARCHAR(255) UNIQUE KEY,
	first_name VARCHAR(255),
    last_name VARCHAR(255),
  	email VARCHAR(255) UNIQUE KEY,
	password_hash VARCHAR(1000),
	logged_in VARCHAR(6),
    PRIMARY KEY (id)
/*   FOREIGN KEY (pid) REFERENCES augmons(id) ON UPDATE CASCADE, */
/*    FOREIGN KEY (aid) REFERENCES pedometers(id) ON UPDATE CASCADE*/
) ENGINE=InnoDB;




CREATE TABLE IF NOT EXISTS augmons
(
	id INTEGER(100) UNSIGNED NOT NULL,
	name VARCHAR(255),
	total_xp INTEGER(255) UNSIGNED NOT NULL,
	lvl INTEGER(255) UNSIGNED NOT NULL,
	attack INTEGER(255) UNSIGNED NOT NULL,
	defense INTEGER(255) UNSIGNED NOT NULL,
	happiness INTEGER(255) UNSIGNED NOT NULL,
	PRIMARY KEY (id),
	FOREIGN KEY (id) REFERENCES users(id)
	) ENGINE=InnoDB;



CREATE TABLE IF NOT EXISTS pedometers
(
	id INTEGER(100) UNSIGNED NOT NULL,
	total_steps INTEGER(255) UNSIGNED NOT NULL,
	daily_steps INTEGER(255) UNSIGNED NOT NULL,
	PRIMARY KEY (id),
	FOREIGN KEY (id) REFERENCES users(id)
)ENGINE=InnoDB;



CREATE TRIGGER users_insert_augmon
AFTER INSERT ON users
FOR EACH ROW
	INSERT INTO augmons(id, name, total_xp, lvl, attack, defense, happiness) values (NEW.id, "default", 0, 1, 10, 10, 10);




CREATE TRIGGER users_insert_pedometers
AFTER INSERT ON augmons
FOR EACH ROW
	INSERT INTO pedometers(id, total_steps, daily_steps) values (NEW.id, 0, 0);