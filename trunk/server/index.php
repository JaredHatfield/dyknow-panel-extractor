<?php

require './configs/db.php';
require './libs/Smarty.class.php';
require './common/DyKnow.file.import.php';
require './common/DyKnow.panel.render.php';

$smarty = new Smarty;
$smarty->compile_check = true;
//$smarty->debugging = true;



// This test code is designed to be run from the command line
set_time_limit(0); // Don't let the page time out
ob_implicit_flush(true);
ob_end_flush();
echo "Starting... " . time() .  "\n";
$path = '../sample_panels.dyz';
$content = open_dyknow_file($path);
$parser = new dpxparser();
$parser->loadfile($content);
$parser->parse();
exit();


/*
if(isset($_GET['action'])){
	if($_GET['action'] == "rawimport"){
		echo "Importing raw file...";
		$path = '../sample_panels.dyz';
		$content = open_dyknow_file($path);
		dyknow_raw_db_import($content);
		echo 'File now imported into database';
	}
	else if($_GET['action'] == "importfile"){
		
		//Read the contents of a DyKnow file into the database
		$path = '../sample_panels.dyz';
		$content = open_dyknow_file($path);
		$parser = new dpxparser();
		$parser->loadfile($content);
		$parser->parse();
	}
	else{
		echo "invalid selection";
	}
}
else{
	echo 'Page not set.';
}
$smarty->display('index.tpl');
*/
?>
