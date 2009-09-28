<?php

function get_page($id){
	$query  = "SELECT p.uid, p.oner, p.onern ";
	$query .= "FROM dpx.pages p ";
	$query .= "WHERE p.page = " . $id . ";";
	$result = mysql_query($query);
	$row = mysql_fetch_assoc($result);
	return $row;
}

function get_pen_strokes($id){
	$query  = " ";
	$query  = "SELECT p.ut, p.pw, p.ph, p.uid, p.data ";
	$query .= "FROM dpx.pens p ";
	$query .= "WHERE p.page = " . $id . " ";
	$query .= "  AND p.uid NOT IN ( ";
	$query .= "    SELECT e.objid FROM dpx.deob d ";
	$query .= "    JOIN dpx.edde e ON d.deob = e.deob ";
	$query .= "    WHERE page = " . $id . ");";
	$result = mysql_query($query);
	$val = array();
	while($row = mysql_fetch_assoc($result)){
		$val[] = $row;
	}
	return $val;
}


function render_page($id){
	$pageinfo = get_page($id);
	$inkinfo = get_pen_strokes($id);
	$file = "";

	$file .= '<DYKNOW_NB50 VRSN="5.1.41.0">';
	$file .= '<DATA>';
	$file .= '<PAGE UID="' . $pageinfo['uid'] . '" ONER="' . $pageinfo['oner'] . '" ONERN="' . $pageinfo['onern'] . '">';
	$file .= '<TXTMODECONTENTSMOD />';
	$file .= '<TXTMODECONTENTSPART />';
	$file .= '<OLST>';

	for($i = 0; $i < sizeof($inkinfo); $i++){
		$file .= '<PEN UT="' . $inkinfo[$i]['ut'] . '" PW="' . $inkinfo[$i]['pw'] . '" PH="' . $inkinfo[$i]['ph'] . '" UID="' . $inkinfo[$i]['uid'] . '" DATA="' . $inkinfo[$i]['data'] . '" />';
	}

	$file .= '</OLST>';
	$file .= '</PAGE>';
	$file .= '</DATA>';
	$file .= '<CHATS />';
	$file .= '</DYKNOW_NB50>';

	//Render the file in the browser
	$compressed = gzcompress($file);
	//header('Content-type: application/dyknow');
	//header('Content-Disposition: attachment; filename="panel.dyz"');
	echo $compressed;

	/*
	$string = $file;
	$gz = gzopen('somefile.dyz','w9');
	gzwrite($gz, $string);
	gzclose($gz);
	echo 'done';
	*/

	/*
	header('Content-type: application/dyknow');
	header('Content-Disposition: attachment; filename="panel.dyz"');
	//echo gzencode($file, 9);
	echo gzcompress($file);
	*/
}

?>