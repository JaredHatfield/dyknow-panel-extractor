<?php

require '../libs/Smarty.class.php';

$smarty = new Smarty;

$smarty->compile_check = true;
$smarty->debugging = true;



$smarty->display('index.tpl');

?>
