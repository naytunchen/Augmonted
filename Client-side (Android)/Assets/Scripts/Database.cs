using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;

public class Database {
	IDbConnection dbcon;
	IDbCommand dbcmd;

	public Database () {

		DropTables ();	// Drop table to ensure that the table is fresh

		CheckDbExist ("1");

		GetAugmonInfo ();	// retrieve default augmon data
		GetPedometerInfo ();	// retrieve default pedometer data


		Augmon tempmon = new Augmon ();


		/*** Tested Dummy Data ***/
		tempmon.ID = 1;
		tempmon.Total_xp = 50;
		tempmon.Lvl = 2;
		tempmon.Attack = 35;
		tempmon.Defense = 26;
		tempmon.Happiness = 99;
		StoreAugmonInfo (tempmon);

		Pedometer pp = new Pedometer ();
		/*** Tested Dummy Data ***/
		pp.ID = 1;
		pp.Total_step = 500;
		pp.Daily_step = 60;
		StorePedometerInfo (pp);


		GetAugmonInfo ();		// retrieve updated augmon data
		GetPedometerInfo ();	// retrieve updated pedometer data

	}
	
	// Update is called once per frame
	public Database (string id) {
	
		DropTables ();
		CheckDbExist (id);
	}
	/******************************************************************************************************************************/
	/*** 											Drop both Tables															***/
	/******************************************************************************************************************************/
	/*** 									Call this method to drop both tables												***/
	/******************************************************************************************************************************/
	public void DropTables()
	{
		OpenConnection ();

		dbcmd = dbcon.CreateCommand ();
		
		string drop_augmon = "DROP TABLE IF EXISTS augmon";
		string drop_pedo = "DROP TABLE IF EXISTS pedometer";
		dbcmd.CommandText = drop_augmon;
		dbcmd.ExecuteNonQuery ();
		Debug.Log ("augmon table dropped");


		dbcmd.CommandText = drop_pedo;
		dbcmd.ExecuteNonQuery ();
		Debug.Log ("pedometer table dropped");
		CloseConnection ();
	}

	/******************************************************************************************************************************/
	/*** 											Check if tables exist														***/
	/******************************************************************************************************************************/
	/*** 						Call this method everytime before inserting data into the table									***/
	/*** 						this method checks if local database has been created or not									***/
	/*** 						if not, it will create 'augmon' and 'pedometer' table with default values						***/
	/***						augmon(id, total_xp, attack, defense, happiness) = (id, 0, 10, 10, 10)							***/
	/***						pedometer(id, total_steps, daily_steps) = (id, 0, 0, 0)											***/
	/******************************************************************************************************************************/
	public void CheckDbExist(string id)
	{

		OpenConnection ();
		dbcmd = dbcon.CreateCommand ();
		
		string checkTable_sql = "SELECT * FROM sqlite_master WHERE type='table' AND name='augmon'";
		dbcmd.CommandText = checkTable_sql;
		IDataReader reader = dbcmd.ExecuteReader ();
		while (reader.Read ()) {
			Debug.Log (reader.GetString (1) + " table exists");
			return;
		}
		reader.Close ();
		reader = null;

		string create_augmon_sql = "CREATE TABLE augmon(id INT, total_xp INT, lvl INT, attack INT, defense INT, happiness INT)";
		string create_pedometer_sql = "CREATE TABLE pedometer(id INT, total_steps INT, daily_steps INT)";
		string insert_default_augmon = "INSERT INTO augmon VALUES(" + id + ", 0, 1, 10, 10, 10)";
		string insert_default_pedo = "INSERT INTO pedometer VALUES(" + id + ", 0, 0)";
		dbcmd.CommandText = create_augmon_sql;
		dbcmd.ExecuteNonQuery ();
		Debug.Log ("Augmon Table Created!");
		dbcmd.CommandText = create_pedometer_sql;
		dbcmd.ExecuteNonQuery ();

		dbcmd.CommandText = insert_default_augmon;
		dbcmd.ExecuteNonQuery ();

		dbcmd.CommandText = insert_default_pedo;
		dbcmd.ExecuteNonQuery ();

		Debug.Log ("Pedometer Table Created!");

	}


	/******************************************************************************************************************************/
	/*** 										Establish Connection to Database												***/
	/******************************************************************************************************************************/
	public void OpenConnection()
	{
		// this method open connection to local database
		string connectionString = "URI=file:augmon.db";
		// Use this for initialization
		dbcon = (IDbConnection)new SqliteConnection (connectionString);
		dbcon.Open ();

	}

	/******************************************************************************************************************************/
	/*** 										Close Connection to Database													***/
	/******************************************************************************************************************************/
	void CloseConnection()
	{
		// this method closes connection to local database
		dbcmd.Dispose ();
		dbcmd = null;
		dbcon.Close ();
		dbcon = null;
	}


	/******************************************************************************************************************************/
	/*** 										Get All function : Augmon														***/
	/******************************************************************************************************************************/
	public Augmon GetAugmonInfo()
	{
		OpenConnection ();
		Debug.Log ("GetAugmonInfo being called");
		dbcmd = dbcon.CreateCommand ();
		string sql = "SELECT * FROM augmon";
		dbcmd.CommandText = sql;
		IDataReader reader = dbcmd.ExecuteReader();
		Augmon augmon = new Augmon();
		while(reader.Read ())
		{
			augmon.ID = reader.GetInt32(0);
			augmon.Total_xp = reader.GetInt32(1);
			augmon.Lvl = reader.GetInt32 (2);
			augmon.Attack = reader.GetInt32(3);
			augmon.Defense = reader.GetInt32(4);
			augmon.Happiness = reader.GetInt32(5);
		}

		Debug.Log ("Augmon:\tid = " + augmon.ID + "\ttotal_xp = " + augmon.Total_xp + "\tlvl = " + augmon.Lvl + "\tattack = " + augmon.Attack + "\tdefense = " + augmon.Defense + "\thappiness = " + augmon.Happiness);
		reader.Close ();
		reader = null;
		CloseConnection ();

		return augmon;
	}

	/******************************************************************************************************************************/
	/*** 										Update All function : Augmon													***/
	/******************************************************************************************************************************/
	public void StoreAugmonInfo(Augmon augmon)
	{
		OpenConnection ();
		Debug.Log ("StoreAugmonInfo being called");
		dbcmd = dbcon.CreateCommand ();
		dbcmd.CommandType = CommandType.Text;

		string sql = "UPDATE augmon SET total_xp = " + augmon.Total_xp + ", lvl = " + augmon.Lvl + ", attack = " + augmon.Attack + ", defense = " + augmon.Defense + ", happiness = " + augmon.Happiness + " WHERE id = " + augmon.ID;
		dbcmd.CommandText = sql;
		dbcmd.ExecuteNonQuery();
		Debug.Log ("all columns on augmon table updated");
		CloseConnection ();
	}


	/******************************************************************************************************************************/
	/*** 										Get All function : Pedometer													***/
	/******************************************************************************************************************************/
	public Pedometer GetPedometerInfo()
	{
		OpenConnection ();
		Debug.Log ("GetPedometerInfo being called");
		dbcmd = dbcon.CreateCommand ();
		string sql = "SELECT * FROM pedometer";
		dbcmd.CommandText = sql;
		IDataReader reader = dbcmd.ExecuteReader();
		Pedometer pedometer = new Pedometer ();
		while(reader.Read ())
		{
			pedometer.ID = reader.GetInt32(0);
			pedometer.Total_step = reader.GetInt32(1);
			pedometer.Daily_step = reader.GetInt32(2);
		}
		Debug.Log ("Pedometer\tid = " + pedometer.ID + "\ttotal_steps = " + pedometer.Total_step + "\tdaily_steps = " + pedometer.Daily_step);
		reader.Close ();
		reader = null;
		CloseConnection ();

		return pedometer;
	}

	/******************************************************************************************************************************/
	/*** 										Update All function : Pedometer													***/
	/******************************************************************************************************************************/
	public void StorePedometerInfo(Pedometer pedometer)
	{
		OpenConnection ();
		Debug.Log ("StorePedometerInfo being called");
		dbcmd = dbcon.CreateCommand ();
		dbcmd.CommandType = CommandType.Text;

		string sql = "UPDATE pedometer SET total_steps = " + pedometer.Total_step + ", daily_steps = " + pedometer.Daily_step + " WHERE id = " + pedometer.ID;
		dbcmd.CommandText = sql;
		dbcmd.ExecuteNonQuery();
		Debug.Log ("all columns on pedometer table updated");
		CloseConnection ();
	}
		
}
